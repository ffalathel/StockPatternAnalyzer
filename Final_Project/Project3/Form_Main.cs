using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Project1; // using aCandlestick and StockReader from Project1

namespace Project3
{
    /// <summary>
    /// This is the main form where users select stock files and date ranges
    /// Chart windows get created from here
    /// </summary>
    public partial class Form_Main : Form
    {
        // keeps track of all the chart windows I've opened
        private readonly List<Form_Chart> openChartForms = new List<Form_Chart>();

        // using the stock reader from Project1 to load CSV files
        private readonly StockReader stockReader = new StockReader();

        /// <summary>
        /// Constructor - sets up the form with default dates
        /// </summary>
        public Form_Main()
        {
            InitializeComponent();

            // set default date range to last year
            dateTimePicker_start.Value = DateTime.Today.AddYears(-1);
            dateTimePicker_end.Value = DateTime.Today;
        }

        /// <summary>
        /// File dialog OK event - doesn't do much, actual work is in button click
        /// </summary>
        private void openFileDialog_files_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // actual processing happens in button_loadFiles_Click
        }

        /// <summary>
        /// Load button click - opens file dialog and loads selected stocks
        /// </summary>
        private void button_loadFiles_Click(object sender, EventArgs e)
        {
            // try to find the Stock Data folder
            string stockFolder = Path.Combine(Application.StartupPath, "Stock Data");
            if (!Directory.Exists(stockFolder))
            {
                // try parent folder (for when running from Visual Studio)
                string parentStockFolder = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Stock Data");
                if (Directory.Exists(parentStockFolder))
                {
                    stockFolder = parentStockFolder;
                }
                else
                {
                    Directory.CreateDirectory(stockFolder);
                }
            }

            // set starting folder
            openFileDialog_files.InitialDirectory = stockFolder;

            // show the dialog
            if (openFileDialog_files.ShowDialog() != DialogResult.OK)
            {
                return; // user cancelled
            }

            // get selected files
            string[] files = openFileDialog_files.FileNames;
            if (files == null || files.Length == 0)
            {
                return;
            }

            // close any existing charts
            CloseAllChartForms();

            // load each file
            for (int idx = 0; idx < files.Length; idx++)
            {
                string file = files[idx];

                // read the candlesticks from the file
                List<aCandlestick> list = stockReader.ReadCandlesticksFromCsv(file);

                // check if we got data
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show(
                        $"No data found in {Path.GetFileName(file)}",
                        "Load Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    continue;
                }

                // create chart form for this stock
                Form_Chart chartForm = new Form_Chart(
                    file,
                    list,
                    dateTimePicker_start.Value.Date,
                    dateTimePicker_end.Value.Date);

                if (idx == 0)
                {
                    // first chart goes in the main form's panel
                    chartForm.TopLevel = false;
                    chartForm.FormBorderStyle = FormBorderStyle.None;
                    chartForm.Dock = DockStyle.Fill;
                    panel_hostChart.Controls.Clear();
                    panel_hostChart.Controls.Add(chartForm);
                    chartForm.Show();
                }
                else
                {
                    // additional charts open as separate windows
                    chartForm.Show();
                }

                // keep track of this form
                openChartForms.Add(chartForm);
            }
        }

        /// <summary>
        /// Refresh button click - refreshes all open charts with current date range
        /// </summary>
        private void button_refreshAll_Click(object sender, EventArgs e)
        {
            // go through all open chart forms
            foreach (var cf in openChartForms.ToList())
            {
                // make sure form is still valid
                if (cf != null && !cf.IsDisposed)
                {
                    // update date range
                    cf.UpdateDateRange(dateTimePicker_start.Value.Date, dateTimePicker_end.Value.Date);
                    // refresh the chart
                    cf.RefreshChart();
                }
            }
        }

        /// <summary>
        /// Closes all the chart forms I've opened
        /// </summary>
        private void CloseAllChartForms()
        {
            foreach (var cf in openChartForms.ToList())
            {
                if (cf != null && !cf.IsDisposed)
                {
                    cf.Close();
                }
            }

            openChartForms.Clear();
            panel_hostChart.Controls.Clear();
        }

        /// <summary>
        /// Helper to convert regular candlesticks to smart candlesticks
        /// </summary>
        /// <param name="candlesticks">List of regular candlesticks to convert</param>
        /// <returns>List of smart candlesticks</returns>
        public static List<aSmartCandlestick> ConvertToSmartCandlesticks(List<aCandlestick> candlesticks)
        {
            List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>();

            foreach (aCandlestick cs in candlesticks)
            {
                smartCandlesticks.Add(new aSmartCandlestick(cs));
            }

            return smartCandlesticks;
        }

        /// <summary>
        /// Alternative method to read smart candlesticks directly from a file
        /// </summary>
        /// <param name="filename">Path to the CSV file</param>
        /// <returns>List of smart candlesticks from the file</returns>
        public static List<aSmartCandlestick> ReadSmartCandlesticksFromFile(string filename)
        {
            List<aSmartCandlestick> candlesticks = new List<aSmartCandlestick>();

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    // skip header
                    string header = sr.ReadLine();

                    // read each line
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            aSmartCandlestick candle = new aSmartCandlestick(line);
                            candlesticks.Add(candle);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return candlesticks;
        }
    }
}
