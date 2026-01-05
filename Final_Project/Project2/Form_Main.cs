using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Project1; // reuse classes from Project1 (aCandlestick, StockReader)

namespace Project2
{
    /// <summary>
    /// Main input form to select stock files, pick date range and manage chart windows.
    /// </summary>
    public partial class Form_Main : Form
    {
        // list of open chart forms so we can refresh them
        private readonly List<Form_Chart> openChartForms = new List<Form_Chart>(); // store references to open charts

        // stock reader instance from Project1 to load CSV files
        private readonly StockReader stockReader = new StockReader(); // reuse Project1's StockReader

        /// <summary>
        /// Construct the main form and set sensible defaults.
        /// </summary>
        public Form_Main()
        {
            InitializeComponent(); // initialize designer components
            // preset start date so user doesn't need to re-enter it on each run
            dateTimePicker_start.Value = DateTime.Today.AddMonths(-6); // default to last 6 months
            dateTimePicker_end.Value = DateTime.Today; // default to today
        }

        /// <summary>
        /// OpenFileDialog FileOk event - no action here (we handle in FileOk handler below).
        /// </summary>
        private void openFileDialog_files_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // This event gets triggered after file(s) selected, actual processing is in button_loadFiles_Click flow.
        }

        /// <summary>
        /// Click handler for Load Stock File(s). Opens file dialog for user to select files.
        /// </summary>
        private void button_loadFiles_Click(object sender, EventArgs e)
        {
            // set initial directory to a "Stock Data" folder beside the exe
            string stockFolder = Path.Combine(Application.StartupPath, "Stock Data");
            if (!Directory.Exists(stockFolder))
            {
                Directory.CreateDirectory(stockFolder); // create if missing
            }
            // set starting folder for dialog
            openFileDialog_files.InitialDirectory = stockFolder;
            // show dialog to user
            if (openFileDialog_files.ShowDialog() != DialogResult.OK)
            {
                return; // user cancelled - do nothing
            }

            // get selected file paths
            string[] files = openFileDialog_files.FileNames;
            if (files == null || files.Length == 0)
            {
                return; // nothing selected
            }

            // iterate selected files - first will be hosted in main form, others as separate windows
            for (int idx = 0; idx < files.Length; idx++)
            {
                string file = files[idx]; // current file path

                // read candlestick list using Project1 StockReader
                List<aCandlestick> list = stockReader.ReadCandlesticksFromCsv(file); // load file into list

                // if no data, show an error and skip
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show($"No data found in {Path.GetFileName(file)}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue; // skip this file
                }

                // create new chart form instance with loaded list and selected date range
                Form_Chart chartForm = new Form_Chart(file, list, dateTimePicker_start.Value.Date, dateTimePicker_end.Value.Date); // instantiate chart form

                if (idx == 0)
                {
                    // host the first chart inside the main form's panel
                    chartForm.TopLevel = false; // make it a child control
                    chartForm.FormBorderStyle = FormBorderStyle.None; // remove border to blend
                    chartForm.Dock = DockStyle.Fill; // occupy the entire panel
                    panel_hostChart.Controls.Clear(); // clear any previous controls in panel
                    panel_hostChart.Controls.Add(chartForm); // add to panel
                    chartForm.Show(); // show the form inside panel
                }
                else
                {
                    // show additional charts as independent windows
                    chartForm.Show(); // show as separate window
                }

                // keep track of open chart forms so we can refresh them later
                openChartForms.Add(chartForm);
            }
        }

        /// <summary>
        /// Click handler for Refresh All - calls RefreshChart on every open Form_Chart.
        /// </summary>
        private void button_refreshAll_Click(object sender, EventArgs e)
        {
            // iterate the list of open chart forms
            foreach (var cf in openChartForms.ToList()) // duplicate list to avoid modification while iterating
            {
                // update each chart form's date range to current values in main form
                cf.UpdateDateRange(dateTimePicker_start.Value.Date, dateTimePicker_end.Value.Date); // set new date range
                // refresh chart data in the chart form
                cf.RefreshChart(); // call refresh on chart form
            }
        }
    }
}
