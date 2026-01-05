using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Project1; // use aCandlestick class from Project1

namespace Project2
{
    /// <summary>
    /// Form that displays a single stock candlestick + volume chart.
    /// </summary>
    public partial class Form_Chart : Form
    {
        // preserved file path for reloads (if needed)
        private readonly string filePath; // path to CSV file

        // original full list read from CSV (unfiltered)
        private readonly List<aCandlestick> originalList; // raw data as loaded

        // currently filtered list used as DataSource
        private List<aCandlestick> currentList = new List<aCandlestick>(); // filtered subset

        // current date range for this chart
        private DateTime startDate;
        private DateTime endDate;

        // stock symbol and period parsed from filename
        private readonly string stockSymbol;
        private readonly string periodLabel;

        /// <summary>
        /// Construct chart form with file and preloaded data.
        /// </summary>
        /// <param name="filePath">CSV file path</param>
        /// <param name="list">Loaded list of aCandlestick</param>
        /// <param name="start">Start date to display</param>
        /// <param name="end">End date to display</param>
        public Form_Chart(string filePath, List<aCandlestick> list, DateTime start, DateTime end)
        {
            InitializeComponent(); // initialize designer components

            // save fields
            this.filePath = filePath; // store file path
            this.originalList = list.ToList(); // copy list
            this.startDate = start; // store start date
            this.endDate = end; // store end date

            // parse filename to extract symbol and period (filename like SYMBOL-Period.csv)
            string fn = System.IO.Path.GetFileNameWithoutExtension(filePath); // filename without extension
            var parts = fn.Split('-'); // split by hyphen
            this.stockSymbol = parts.Length > 0 ? parts[0] : fn; // first part is symbol
            this.periodLabel = parts.Length > 1 ? parts[1] : "Day"; // second part is period or default

            // configure chart areas, series and appearance
            SetupChartControl(); // prepare chart control

            // initial bind & display
            RefreshChart(); // render chart
        }

        /// <summary>
        /// Configure chart control: create areas, series, remove legend, setup volume area alignment.
        /// </summary>
        private void SetupChartControl()
        {
            // clear any existing configuration
            chart_candlestick.Series.Clear(); // clear series
            chart_candlestick.ChartAreas.Clear(); // clear areas
            chart_candlestick.Titles.Clear(); // clear titles
            chart_candlestick.Legends.Clear(); // remove legend to save right space

            // create OHLC chart area
            ChartArea caOHLC = new ChartArea("ChartArea_OHLC"); // main price area
            caOHLC.Position = new ElementPosition(0, 0, 100, 70); // occupy top 70%
            caOHLC.AxisX.LabelStyle.Format = "yyyy-MM-dd"; // date format
            caOHLC.AxisX.MajorGrid.Enabled = false; // improve readability
            caOHLC.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash; // dashed grid for price
            caOHLC.AxisX.IsMarginVisible = false; // avoid margins so index mapping consistent

            // create Volume chart area aligned under OHLC
            ChartArea caVol = new ChartArea("ChartArea_Volume"); // volume area
            caVol.Position = new ElementPosition(0, 70, 100, 30); // bottom 30%
            caVol.AlignWithChartArea = "ChartArea_OHLC"; // align X with OHLC
            caVol.AxisX.LabelStyle.Enabled = false; // hide x labels on volume area
            caVol.AxisY.MajorGrid.Enabled = false; // hide grid

            // add areas to chart
            chart_candlestick.ChartAreas.Add(caOHLC);
            chart_candlestick.ChartAreas.Add(caVol);

            // Create candlestick series and configure for data-binding
            Series sPrice = new Series("Series_OHLC"); // price series
            sPrice.ChartType = SeriesChartType.Candlestick; // candlestick
            sPrice.ChartArea = "ChartArea_OHLC"; // place in OHLC area
            sPrice.XValueMember = "Date"; // bound X member name
            sPrice.YValueMembers = "High,Low,Open,Close"; // bound Y members in order required by candlestick
            sPrice.YValuesPerPoint = 4; // four Y values
            sPrice.IsXValueIndexed = true; // index X values to avoid date gaps
            sPrice["PriceUpColor"] = "Lime"; // up color
            sPrice["PriceDownColor"] = "Red"; // down color
            sPrice["OpenCloseStyle"] = "Triangle"; // show open/close markers

            // Create volume series and configure for data-binding
            Series sVol = new Series("Series_Volume"); // volume series
            sVol.ChartType = SeriesChartType.Column; // column bars
            sVol.ChartArea = "ChartArea_Volume"; // place in volume area
            sVol.XValueMember = "Date"; // X member
            sVol.YValueMembers = "Volume"; // Y member for volume
            sVol.IsXValueIndexed = true; // align indexing with price series

            // Add series to chart
            chart_candlestick.Series.Add(sPrice);
            chart_candlestick.Series.Add(sVol);

            // Add title placeholder (updated on refresh)
            chart_candlestick.Titles.Add(""); // empty title - will be set later
        }

        /// <summary>
        /// Update the date-range used by this chart.
        /// </summary>
        /// <param name="start">New start date</param>
        /// <param name="end">New end date</param>
        public void UpdateDateRange(DateTime start, DateTime end)
        {
            // set new values
            this.startDate = start;
            this.endDate = end;
        }

        /// <summary>
        /// Refresh chart: filter data, bind to chart, normalize axes and update title.
        /// </summary>
        public void RefreshChart()
        {
            // filter the original list by the current date range
            currentList = originalList
                .Where(c => c.date.Date >= startDate.Date && c.date.Date <= endDate.Date)
                .OrderBy(c => c.date) // chronological order
                .ToList();

            // set data source for data-binding
            chart_candlestick.DataSource = currentList; // bind whole chart to list

            // perform DataBind for series so the chart auto-populates
            chart_candlestick.Series["Series_OHLC"].XValueMember = "date"; // ensure binding names match
            chart_candlestick.Series["Series_OHLC"].YValueMembers = "high,low,open,close"; // set binding members
            chart_candlestick.Series["Series_Volume"].XValueMember = "date"; // set binding member for volume
            chart_candlestick.Series["Series_Volume"].YValueMembers = "volume"; // set binding member for volume

            // call DataBind to populate series points from currentList
            chart_candlestick.DataBind(); // bind data source to chart

            // Normalize Y axis to use dynamic range of currentList with 2% padding
            NormalizeChartYAxis();

            // Update chart title with required format:
            // [SYMBOL]-[PERIOD]
            // [START_DATE] – [END_DATE]
            string titleLine1 = $"{stockSymbol}-{periodLabel}";
            string titleLine2 = $"{startDate:M/d/yyyy} – {endDate: M/d/yyyy}".Replace("  ", " "); // format range
            string fullTitle = titleLine1 + Environment.NewLine + titleLine2;
            chart_candlestick.Titles[0].Text = fullTitle; // set title

            // Force chart redraw
            chart_candlestick.Invalidate(); // redraw chart
        }

        /// <summary>
        /// Local handler for the optional Refresh button on the form.
        /// Calls public RefreshChart.
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            // call RefreshChart to rebind and redraw the chart
            RefreshChart();
        }

        /// <summary>
        /// Normalize chart Y axis ranges (price axis) using currentList values with 2% padding.
        /// </summary>
        private void NormalizeChartYAxis()
        {
            // if no data, nothing to normalize
            if (currentList == null || currentList.Count == 0)
            {
                return; // exit early
            }

            // find min low and max high from the filtered list
            decimal minLow = currentList.Min(c => c.low); // minimum low price
            decimal maxHigh = currentList.Max(c => c.high); // maximum high price

            // compute padding as 2% of the range
            decimal buffer = (maxHigh - minLow) * 0.02m; // 2% buffer
            if (buffer <= 0)
            {
                buffer = maxHigh * 0.02m; // fallback use 2% of maxHigh if degenerate
            }

            // set axis min and max with buffer
            ChartArea ca = chart_candlestick.ChartAreas["ChartArea_OHLC"]; // get OHLC area
            ca.AxisY.Minimum = Math.Max(0, (double)(minLow - buffer)); // set min (>=0)
            ca.AxisY.Maximum = (double)(maxHigh + buffer); // set max
        }
    }
}
