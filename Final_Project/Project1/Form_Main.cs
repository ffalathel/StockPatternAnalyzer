using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project1
{
    /// <summary>
    /// This is my main form for the stock analysis app that shows candlestick charts and volume data
    /// </summary>
    public partial class Form_Main : Form
    {
        // This helps me read stock data from CSV files
        private StockReader stockReader;

        // This holds all the candlesticks I loaded from the CSV file (before filtering)
        private List<aCandlestick> allCandlesticks;

        // This holds the candlesticks after I filter them by date range, bound to the DataGridView
        private BindingList<aCandlestick> filteredCandlesticks;

        // This stores which stock symbol I'm currently looking at
        private string currentStockSymbol = "";

        /// <summary>
        /// Constructor for my main form - sets up everything and gives it default values
        /// </summary>
        public Form_Main()
        {
            // Set up all the form components (controls, layouts, etc.)
            InitializeComponent();

            // Create my stock reader object for reading CSV files
            stockReader = new StockReader();

            // Initialize the lists so I don't get null reference exceptions
            allCandlesticks = new List<aCandlestick>();
            filteredCandlesticks = new BindingList<aCandlestick>();

            // Set default start date to 2 years ago so I have a reasonable range
            dateTimePicker_Start.Value = DateTime.Now.AddYears(-2);
            // Set default end date to today
            dateTimePicker_End.Value = DateTime.Now;

            // Set default period to Daily (first item in the combobox)
            comboBox_Period.SelectedIndex = 0;

            // Set up the chart areas so OHLC and Volume display properly
            configureChartAreas();
        }

        /// <summary>
        /// This sets up the chart areas so OHLC and Volume display properly with good alignment
        /// </summary>
        private void configureChartAreas()
        {
            // Get reference to the OHLC chart area (candlestick display)
            ChartArea chartArea_OHLC = chart_Candlesticks.ChartAreas["ChartArea_OHLC"];
            // Get reference to the Volume chart area (column display)
            ChartArea chartArea_Volume = chart_Candlesticks.ChartAreas["ChartArea_Volume"];

            // Set the OHLC chart area to take up 70% of the vertical space
            chartArea_OHLC.Position.Height = 70;
            // Set the width to 100% of the chart control
            chartArea_OHLC.Position.Width = 100;
            // Position at the left edge
            chartArea_OHLC.Position.X = 0;
            // Position at the top
            chartArea_OHLC.Position.Y = 0;

            // Set the Volume chart area to take up 30% of the vertical space below OHLC
            chartArea_Volume.Position.Height = 30;
            // Set the width to 100% of the chart control
            chartArea_Volume.Position.Width = 100;
            // Position at the left edge
            chartArea_Volume.Position.X = 0;
            // Position below the OHLC chart at 70% down
            chartArea_Volume.Position.Y = 70;

            // Align the chart areas so they share the same X axis alignment
            chartArea_Volume.AlignWithChartArea = "ChartArea_OHLC";
        }

        /// <summary>
        /// Event handler for the Load Stock button click, opens file dialog
        /// </summary>
        /// <param name="sender">The button that triggered the event</param>
        /// <param name="e">Event arguments</param>
        private void button_LoadStock_Click(object sender, EventArgs e)
        {
            // Set the initial directory to the Stock Data folder relative to executable
            string stockDataPath = Path.Combine(Application.StartupPath, "Stock Data");

            // Create the directory if it doesn't exist to prevent errors
            if (!Directory.Exists(stockDataPath))
            {
                // Create the Stock Data folder
                Directory.CreateDirectory(stockDataPath);
            }

            // Set the initial directory for the file dialog
            openFileDialog_Stock.InitialDirectory = stockDataPath;

            // Show the open file dialog to let user select a stock CSV file
            openFileDialog_Stock.ShowDialog();
        }

        /// <summary>
        /// Event handler triggered when user selects a file and clicks OK in the file dialog
        /// This is where the main loading and display logic happens
        /// </summary>
        /// <param name="sender">The OpenFileDialog control</param>
        /// <param name="e">Cancel event arguments</param>
        private void openFileDialog_Stock_FileOk(object sender, CancelEventArgs e)
        {
            // Extract the stock symbol from the filename (everything before the hyphen)
            string filename = Path.GetFileNameWithoutExtension(openFileDialog_Stock.FileName);
            // Check if the filename contains a hyphen separator
            if (filename.Contains("-"))
            {
                // Split by hyphen and take the first part as the stock symbol
                currentStockSymbol = filename.Split('-')[0];
            }

            // Read the candlestick data from the selected CSV file
            readCandlesticksFromFile();

            // Filter the candlesticks based on the selected date range
            filterCandlesticks();

            // Normalize the chart Y-axis to use full dynamic range
            normalizeChart();

            // Display the candlesticks in both the DataGridView and Chart
            displayCandlesticks();
        }

        /// <summary>
        /// Event handler for the end date picker value changed (auto-generated, not used)
        /// </summary>
        /// <param name="sender">The DateTimePicker control</param>
        /// <param name="e">Event arguments</param>
        private void dateTimePicker_End_ValueChanged(object sender, EventArgs e)
        {
            // This event handler is auto-generated but not currently used
            // Leave empty unless you want to add automatic refresh on date change
        }

        /// <summary>
        /// Event handler for the period combobox selection changed (auto-generated, not used)
        /// </summary>
        /// <param name="sender">The ComboBox control</param>
        /// <param name="e">Event arguments</param>
        private void comboBox_Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This event handler is auto-generated but not currently used
            // Leave empty unless you want to add automatic file reloading on period change
        }

        /// <summary>
        /// Reads candlestick data from a CSV file and populates allCandlesticks
        /// Version that uses the currently selected file from the OpenFileDialog
        /// </summary>
        private void readCandlesticksFromFile()
        {
            // Get the filename from the dialog
            string filename = openFileDialog_Stock.FileName;

            // Call the overloaded version that does the actual reading
            allCandlesticks = loadTicker(filename);
        }

        /// <summary>
        /// Loads ticker data from a CSV file, ensures proper chronological order
        /// </summary>
        /// <param name="filename">Full path to the CSV file to read</param>
        /// <returns>List of aCandlestick objects in chronological order (oldest first)</returns>
        private List<aCandlestick> loadTicker(string filename)
        {
            // Get the stock reader to read the candlesticks from the CSV file
            List<aCandlestick> listOfCandlesticks = stockReader.ReadCandlesticksFromCsv(filename);

            // Check if we have at least 2 candlesticks to compare dates
            if (listOfCandlesticks != null && listOfCandlesticks.Count >= 2)
            {
                // Get the first candlestick from the list
                aCandlestick firstCandlestick = listOfCandlesticks[0];
                // Get the second candlestick from the list
                aCandlestick secondCandlestick = listOfCandlesticks[1];

                // If the first date is greater than the second, the list is in reverse order
                if (firstCandlestick.date > secondCandlestick.date)
                {
                    // Don't forget to reverse the list so oldest dates come first
                    listOfCandlesticks.Reverse();
                }
            }

            // Return the list in proper chronological order (oldest first)
            return listOfCandlesticks;
        }

        /// <summary>
        /// Filters the form's allCandlesticks based on date range from the DateTimePickers
        /// and updates the filteredCandlesticks with the filtered results
        /// </summary>
        private void filterCandlesticks()
        {
            // Get the start date from the DateTimePicker control
            DateTime startDate = dateTimePicker_Start.Value;
            // Get the end date from the DateTimePicker control
            DateTime endDate = dateTimePicker_End.Value;

            // Call the overloaded version that does the actual filtering
            List<aCandlestick> tempFilteredList = FilterCandlesticksByDate(allCandlesticks, startDate, endDate);

            // Create a BindingList from the filtered list for data binding to DataGridView
            filteredCandlesticks = new BindingList<aCandlestick>(tempFilteredList);
        }

        /// <summary>
        /// Filters a list of candlesticks to only include those within the specified date range
        /// </summary>
        /// <param name="listOfCandlesticks">The complete list of candlesticks to filter</param>
        /// <param name="startDate">The earliest date to include (inclusive)</param>
        /// <param name="endDate">The latest date to include (inclusive)</param>
        /// <returns>A new list containing only candlesticks within the date range</returns>
        private List<aCandlestick> FilterCandlesticksByDate(List<aCandlestick> listOfCandlesticks, DateTime startDate, DateTime endDate)
        {
            // Use LINQ Where clause to filter candlesticks by date range
            // Only include candlesticks where the date is >= startDate AND <= endDate
            List<aCandlestick> filteredList = listOfCandlesticks
                .Where(c => c.date >= startDate && c.date <= endDate)
                .ToList();

            // Return the filtered list
            return filteredList;
        }

        /// <summary>
        /// Normalizes the chart's Y-axis to use the full dynamic range of the filtered data
        /// Adds 5% padding above and below for better visualization
        /// </summary>
        private void normalizeChart()
        {
            // Only normalize if we have data to display
            if (filteredCandlesticks == null || filteredCandlesticks.Count == 0)
                return;

            // Call the overloaded version that accepts the list as parameter
            normalizeChart(filteredCandlesticks.ToList());
        }

        /// <summary>
        /// Normalizes the chart's Y-axis based on the provided list of candlesticks
        /// </summary>
        /// <param name="listOfCandlesticks">List of candlesticks to use for finding min/max values</param>
        private void normalizeChart(List<aCandlestick> listOfCandlesticks)
        {
            // Get the minimum low value of the stock data we will display
            decimal minValue = listOfCandlesticks.Min(c => c.low);
            // Get the maximum high value of the stock data we will display
            decimal maxValue = listOfCandlesticks.Max(c => c.high);

            // Calculate 5% padding based on the range of values
            decimal padding = (maxValue - minValue) * 0.05m;
            // Subtract padding from minimum to add space below
            decimal minY = minValue - padding;
            // Add padding to maximum to add space above
            decimal maxY = maxValue + padding;

            // Get reference to the Y-axis of the OHLC chart area
            Axis yAxis = chart_Candlesticks.ChartAreas["ChartArea_OHLC"].AxisY;
            // Set the minimum value of the Y-axis, rounding down to nearest integer
            yAxis.Minimum = Math.Floor((double)minY);
            // Set the maximum value of the Y-axis, rounding up to nearest integer
            yAxis.Maximum = Math.Ceiling((double)maxY);
        }

        /// <summary>
        /// Displays the filtered candlesticks in both the DataGridView and the Chart control
        /// </summary>
        private void displayCandlesticks()
        {
            // Redraw the DataGridView by setting its DataSource to the filtered candlesticks
            dataGridView_Candlesticks.DataSource = filteredCandlesticks;

            // Reset the DataSource of the chart to the filtered candlesticks
            chart_Candlesticks.DataSource = filteredCandlesticks;

            // Get reference to the OHLC series in the chart
            Series ohlcSeries = chart_Candlesticks.Series["Series_OHLC"];
            // Set which property to use for X values (dates)
            ohlcSeries.XValueMember = "date";
            // Set which properties to use for Y values (High, Low, Open, Close in that order)
            ohlcSeries.YValueMembers = "high,low,open,close";

            // Get reference to the Volume series in the chart
            Series volumeSeries = chart_Candlesticks.Series["Series_Volume"];
            // Set which property to use for X values (dates)
            volumeSeries.XValueMember = "date";
            // Set which property to use for Y values (volume)
            volumeSeries.YValueMembers = "volume";

            // Bind the data to the chart to update the display
            chart_Candlesticks.DataBind();

            // Update the form title to show the current stock symbol
            this.Text = $"Stock Analysis - {currentStockSymbol}";
        }

      
        /// <summary>
        /// Refreshes the display by re-filtering, re-normalizing, and re-displaying the data
        /// Called when user changes date range or other display parameters
        /// </summary>
        private void refreshDisplay()
        {
            // First we need to filter or refilter the list of allCandlesticks
            filterCandlesticks();

            // Normalize the chart with the newly filtered data
            normalizeChart();

            // Redisplay the candlesticks in both DataGridView and Chart
            displayCandlesticks();
        }

        /// <summary>
        /// Event handler for the Update button click, refreshes the display with current date range
        /// </summary>
        /// <param name="sender">The button that triggered the event</param>
        /// <param name="e">Event arguments</param>
        private void button_Update_Click(object sender, EventArgs e)
        {
            // Only update if we have data loaded
            if (allCandlesticks == null || allCandlesticks.Count == 0)
            {
                // Show message box informing user to load a file first
                MessageBox.Show("Please load a stock file first.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if period selection changed and reload appropriate file if needed
            updatePeriodIfChanged();

            // Refresh the entire display with current settings
            refreshDisplay();
        }

        /// <summary>
        /// Checks if the period selection has changed and reloads the appropriate file if needed
        /// Constructs filename based on stock symbol and selected period (Day/Week/Month)
        /// </summary>
        private void updatePeriodIfChanged()
        {
            // Only proceed if we have a current stock symbol loaded
            if (string.IsNullOrEmpty(currentStockSymbol))
                return;

            // Get the selected period from the combo box (Day, Week, or Month)
            string selectedPeriod = comboBox_Period.SelectedItem?.ToString();

            // If no period is selected, default to Day
            if (string.IsNullOrEmpty(selectedPeriod))
                selectedPeriod = "Day";

            // Construct the filename based on symbol and period
            // Format: SYMBOL-Period.csv (e.g., AAPL-Day.csv, AAPL-Week.csv)
            string filename = $"{currentStockSymbol}-{selectedPeriod}.csv";
            // Get the full path to the file in the Stock Data folder
            string fullPath = Path.Combine(Application.StartupPath, "Stock Data", filename);

            // Check if the file exists before trying to load it
            if (File.Exists(fullPath))
            {
                // Read the candlesticks from the period-specific file
                allCandlesticks = loadTicker(fullPath);
            }
            else
            {
                // Inform user that the file for this period doesn't exist
                MessageBox.Show($"File not found: {filename}\nPlease ensure you have downloaded the {selectedPeriod} data.",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker_Start_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label_StartDate_Click(object sender, EventArgs e)
        {

        }

        private void label_EndDate_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_Candlesticks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart_Candlesticks_Click(object sender, EventArgs e)
        {

        }

        private void label_Period_Click(object sender, EventArgs e)
        {

        }
    }
}