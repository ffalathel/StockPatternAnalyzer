using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Project1; // using aCandlestick from Project1

namespace Project3
{
    /// <summary>
    /// This form displays a stock chart with candlesticks and volume
    /// I also added animation and pattern recognition here
    /// </summary>
    public partial class Form_Chart : Form
    {
        // path to the CSV file I loaded
        private readonly string filePath;

        // the original list of candlesticks from the file
        private readonly List<aCandlestick> originalList;

        // all the smart candlesticks for the current date range
        private List<aSmartCandlestick> allCandlesticks = new List<aSmartCandlestick>();

        // candlesticks currently being displayed (used for animation)
        private BindingList<aSmartCandlestick> displayedCandlesticks = new BindingList<aSmartCandlestick>();

        // keeps track of which candlestick to show next during animation
        private int currentCandlestickIndex = 0;

        // the date range for filtering
        private DateTime startDate;
        private DateTime endDate;

        // stock symbol and period parsed from filename
        private readonly string stockSymbol;
        private readonly string periodLabel;

        // my controller that handles all the pattern recognizers
        private RecognizerController recognizerController;

        /// <summary>
        /// Creates the chart form with the loaded candlestick data
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <param name="list">List of candlesticks from the file</param>
        /// <param name="start">Start date for filtering</param>
        /// <param name="end">End date for filtering</param>
        public Form_Chart(string filePath, List<aCandlestick> list, DateTime start, DateTime end)
        {
            InitializeComponent();

            // save everything I need
            this.filePath = filePath;
            this.originalList = list.ToList();
            this.startDate = start;
            this.endDate = end;

            // parse the filename to get symbol and period
            // filename is like "AAPL-Day.csv"
            string fn = System.IO.Path.GetFileNameWithoutExtension(filePath);
            var parts = fn.Split('-');
            this.stockSymbol = parts.Length > 0 ? parts[0] : fn;
            this.periodLabel = parts.Length > 1 ? parts[1] : "Day";

            // create my recognizer controller
            recognizerController = new RecognizerController();

            // fill up the pattern combobox
            PopulatePatternComboBox();

            // set up the chart
            SetupChartControl();

            // show the data
            RefreshChart();
        }

        /// <summary>
        /// Fills the combobox with all available pattern names
        /// </summary>
        private void PopulatePatternComboBox()
        {
            // get all the pattern names from my controller
            List<string> patternNames = recognizerController.GetPatternNames();

            // add a "none" option first
            comboBox_patterns.Items.Add("(None)");

            // add all the pattern names
            foreach (string name in patternNames)
            {
                comboBox_patterns.Items.Add(name);
            }

            // start with none selected
            comboBox_patterns.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets up the chart control with all the areas and series
        /// </summary>
        private void SetupChartControl()
        {
            // clear everything first
            chart_candlestick.Series.Clear();
            chart_candlestick.ChartAreas.Clear();
            chart_candlestick.Titles.Clear();
            chart_candlestick.Legends.Clear(); // don't need the legend

            // create the main price chart area - takes up top 70%
            ChartArea caOHLC = new ChartArea("ChartArea_OHLC");
            caOHLC.Position = new ElementPosition(0, 0, 100, 70);
            caOHLC.AxisX.LabelStyle.Format = "yyyy-MM-dd";
            caOHLC.AxisX.MajorGrid.Enabled = false;
            caOHLC.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            caOHLC.AxisX.IsMarginVisible = false;

            // create the volume chart area - bottom 30%
            ChartArea caVol = new ChartArea("ChartArea_Volume");
            caVol.Position = new ElementPosition(0, 70, 100, 30);
            caVol.AlignWithChartArea = "ChartArea_OHLC";
            caVol.AxisX.LabelStyle.Enabled = false;
            caVol.AxisY.MajorGrid.Enabled = false;

            // add them to the chart
            chart_candlestick.ChartAreas.Add(caOHLC);
            chart_candlestick.ChartAreas.Add(caVol);

            // create the candlestick series
            Series sPrice = new Series("Series_OHLC");
            sPrice.ChartType = SeriesChartType.Candlestick;
            sPrice.ChartArea = "ChartArea_OHLC";
            sPrice.XValueMember = "date";
            sPrice.YValueMembers = "high,low,open,close";
            sPrice.YValuesPerPoint = 4;
            sPrice.IsXValueIndexed = true; // this removes weekend gaps
            sPrice["PriceUpColor"] = "Lime";
            sPrice["PriceDownColor"] = "Red";
            sPrice["OpenCloseStyle"] = "Triangle";

            // create the volume series
            Series sVol = new Series("Series_Volume");
            sVol.ChartType = SeriesChartType.Column;
            sVol.ChartArea = "ChartArea_Volume";
            sVol.XValueMember = "date";
            sVol.YValueMembers = "volume";
            sVol.IsXValueIndexed = true;

            // add series to chart
            chart_candlestick.Series.Add(sPrice);
            chart_candlestick.Series.Add(sVol);

            // add empty title - will set it later
            chart_candlestick.Titles.Add("");
        }

        /// <summary>
        /// Updates the date range for this chart
        /// </summary>
        /// <param name="start">New start date</param>
        /// <param name="end">New end date</param>
        public void UpdateDateRange(DateTime start, DateTime end)
        {
            this.startDate = start;
            this.endDate = end;
        }

        /// <summary>
        /// Refreshes the chart with current data and date range
        /// </summary>
        public void RefreshChart()
        {
            // filter by date and convert to smart candlesticks
            allCandlesticks = originalList
                .Where(c => c.date.Date >= startDate.Date && c.date.Date <= endDate.Date)
                .OrderBy(c => c.date)
                .Select(c => new aSmartCandlestick(c))
                .ToList();

            // reset animation stuff
            currentCandlestickIndex = 0;

            // put all candlesticks in the display list
            displayedCandlesticks = new BindingList<aSmartCandlestick>(allCandlesticks.ToList());

            // bind to chart
            BindChartData();

            // set the Y axis range
            NormalizeChartYAxis();

            // update the title
            UpdateChartTitle();

            // redraw
            chart_candlestick.Invalidate();
        }

        /// <summary>
        /// Binds the candlestick data to the chart
        /// </summary>
        private void BindChartData()
        {
            // set data source
            chart_candlestick.DataSource = displayedCandlesticks;

            // set up the bindings
            chart_candlestick.Series["Series_OHLC"].XValueMember = "date";
            chart_candlestick.Series["Series_OHLC"].YValueMembers = "high,low,open,close";
            chart_candlestick.Series["Series_Volume"].XValueMember = "date";
            chart_candlestick.Series["Series_Volume"].YValueMembers = "volume";

            // do the binding
            chart_candlestick.DataBind();
        }

        /// <summary>
        /// Updates the chart title with symbol, period and date range
        /// </summary>
        private void UpdateChartTitle()
        {
            string titleLine1 = $"{stockSymbol}-{periodLabel}";
            string titleLine2 = $"{startDate:M/d/yyyy} – {endDate:M/d/yyyy}";
            chart_candlestick.Titles[0].Text = titleLine1 + Environment.NewLine + titleLine2;
        }

        /// <summary>
        /// Sets the Y axis to fit the data with 2% padding
        /// </summary>
        private void NormalizeChartYAxis()
        {
            // need data to normalize
            if (displayedCandlesticks == null || displayedCandlesticks.Count == 0)
            {
                return;
            }

            // find min and max prices
            decimal minLow = displayedCandlesticks.Min(c => c.low);
            decimal maxHigh = displayedCandlesticks.Max(c => c.high);

            // add 2% padding
            decimal buffer = (maxHigh - minLow) * 0.02m;
            if (buffer <= 0)
            {
                buffer = maxHigh * 0.02m;
            }

            // set the axis range
            ChartArea ca = chart_candlestick.ChartAreas["ChartArea_OHLC"];
            ca.AxisY.Minimum = Math.Max(0, (double)(minLow - buffer));
            ca.AxisY.Maximum = (double)(maxHigh + buffer);
        }

        /// <summary>
        /// Runs pattern recognition for the selected pattern
        /// </summary>
        private void RunPatternRecognition()
        {
            // clear old annotations first
            ClearAnnotations();

            // check if a pattern is selected
            if (comboBox_patterns.SelectedIndex <= 0)
            {
                return; // none selected
            }

            string patternName = comboBox_patterns.SelectedItem.ToString();

            // get the recognizer
            Recognizer recognizer = recognizerController.GetRecognizerByName(patternName);
            if (recognizer == null)
            {
                MessageBox.Show($"Couldn't find recognizer for: {patternName}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // make sure we have data
            if (displayedCandlesticks == null || displayedCandlesticks.Count == 0)
            {
                MessageBox.Show("No data to analyze. Load a stock file first.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // find all matches
            List<int> matchingIndices = recognizerController.RecognizePattern(
                displayedCandlesticks.ToList(), patternName);

            // let user know if nothing found
            if (matchingIndices.Count == 0)
            {
                MessageBox.Show($"No '{patternName}' patterns found in the current data.\n\n" +
                    $"Total candlesticks analyzed: {displayedCandlesticks.Count}\n" +
                    $"Try selecting a different pattern or loading different data.",
                    "No Patterns Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // add annotations for each match
            foreach (int idx in matchingIndices)
            {
                if (recognizer.patternSize == 2)
                {
                    // 2-candle pattern
                    AddTwoCandlestickAnnotation(idx, patternName);
                }
                else
                {
                    // single candle pattern
                    AddPatternAnnotation(idx, patternName);
                }
            }

            // update title to show how many found
            this.Text = $"Stock Chart - Found {matchingIndices.Count} {patternName} pattern(s)";

            // redraw
            chart_candlestick.Invalidate();
        }

        /// <summary>
        /// Removes all annotations from the chart
        /// </summary>
        private void ClearAnnotations()
        {
            chart_candlestick.Annotations.Clear();
        }

        /// <summary>
        /// Figures out what color to use for a pattern annotation
        /// Green for bullish, red for bearish, blue for neutral
        /// </summary>
        /// <param name="patternName">Name of the pattern</param>
        /// <returns>Color for the annotation</returns>
        private Color GetPatternColor(string patternName)
        {
            string lowerName = patternName.ToLower();

            // bullish patterns get green
            if (lowerName.Contains("bullish") || 
                lowerName.Contains("dragonfly") ||
                lowerName == "hammer")
            {
                return Color.Green;
            }
            // bearish patterns get red
            else if (lowerName.Contains("bearish") || 
                     lowerName.Contains("gravestone"))
            {
                return Color.Red;
            }
            // everything else gets blue
            else
            {
                return Color.Blue;
            }
        }

        /// <summary>
        /// Makes a semi-transparent version of a color
        /// </summary>
        /// <param name="baseColor">The base color to make transparent</param>
        /// <param name="alpha">Transparency level (0-255)</param>
        /// <returns>Transparent version of the color</returns>
        private Color GetTransparentColor(Color baseColor, int alpha = 50)
        {
            return Color.FromArgb(alpha, baseColor);
        }

        /// <summary>
        /// Adds annotations to highlight a single-candlestick pattern
        /// </summary>
        /// <param name="index">Index of the candlestick to highlight</param>
        /// <param name="patternName">Name of the pattern found</param>
        private void AddPatternAnnotation(int index, string patternName)
        {
            // check index is valid
            if (index < 0 || index >= displayedCandlesticks.Count)
            {
                return;
            }

            // make sure chart has data points
            if (index >= chart_candlestick.Series["Series_OHLC"].Points.Count)
            {
                return;
            }

            // get the color for this pattern
            Color patternColor = GetPatternColor(patternName);

            // create rectangle to highlight the candlestick
            RectangleAnnotation rect = new RectangleAnnotation();
            rect.Name = $"Rect_{index}";
            rect.ToolTip = patternName;
            rect.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            rect.Width = 2;
            rect.Height = 8;
            rect.LineColor = patternColor;
            rect.LineWidth = 2;
            rect.LineDashStyle = ChartDashStyle.Solid;
            rect.BackColor = GetTransparentColor(patternColor, 40);
            rect.AllowMoving = false;
            rect.AllowSelecting = false;

            chart_candlestick.Annotations.Add(rect);

            // create arrow pointing at the candlestick
            ArrowAnnotation arrow = new ArrowAnnotation();
            arrow.Name = $"Arrow_{index}";
            arrow.ToolTip = patternName;
            arrow.Height = -4;
            arrow.Width = 0;
            arrow.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            arrow.ArrowStyle = ArrowStyle.Simple;
            arrow.ArrowSize = 3;
            arrow.LineColor = patternColor;
            arrow.BackColor = patternColor;
            arrow.LineWidth = 2;
            arrow.AllowMoving = false;
            arrow.AllowSelecting = false;

            chart_candlestick.Annotations.Add(arrow);

            // create text label with pattern name
            TextAnnotation text = new TextAnnotation();
            text.Name = $"Text_{index}";
            text.Text = patternName;
            text.ForeColor = patternColor;
            text.Font = new Font("Arial", 7, FontStyle.Bold);
            text.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            text.AnchorOffsetY = -6;
            text.AllowMoving = false;
            text.AllowSelecting = false;
            text.AllowAnchorMoving = false;

            chart_candlestick.Annotations.Add(text);
        }

        /// <summary>
        /// Adds annotations for a 2-candlestick pattern
        /// Highlights both candles and draws a line between them
        /// </summary>
        /// <param name="index">Index of the second candlestick in the pattern</param>
        /// <param name="patternName">Name of the pattern found</param>
        private void AddTwoCandlestickAnnotation(int index, string patternName)
        {
            // need at least 2 candles
            if (index < 1 || index >= displayedCandlesticks.Count)
            {
                return;
            }

            // make sure chart has data points
            if (index >= chart_candlestick.Series["Series_OHLC"].Points.Count)
            {
                return;
            }

            Color patternColor = GetPatternColor(patternName);

            // rectangle for first candle (dashed border)
            RectangleAnnotation rect1 = new RectangleAnnotation();
            rect1.Name = $"Rect1_{index}";
            rect1.ToolTip = $"{patternName} (1st candle)";
            rect1.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index - 1];
            rect1.Width = 2;
            rect1.Height = 8;
            rect1.LineColor = patternColor;
            rect1.LineWidth = 2;
            rect1.LineDashStyle = ChartDashStyle.Dash;
            rect1.BackColor = GetTransparentColor(patternColor, 30);
            rect1.AllowMoving = false;
            rect1.AllowSelecting = false;

            chart_candlestick.Annotations.Add(rect1);

            // rectangle for second candle (solid border)
            RectangleAnnotation rect2 = new RectangleAnnotation();
            rect2.Name = $"Rect2_{index}";
            rect2.ToolTip = $"{patternName} (2nd candle)";
            rect2.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            rect2.Width = 2;
            rect2.Height = 8;
            rect2.LineColor = patternColor;
            rect2.LineWidth = 2;
            rect2.LineDashStyle = ChartDashStyle.Solid;
            rect2.BackColor = GetTransparentColor(patternColor, 50);
            rect2.AllowMoving = false;
            rect2.AllowSelecting = false;

            chart_candlestick.Annotations.Add(rect2);

            // line connecting the two candles
            LineAnnotation line = new LineAnnotation();
            line.Name = $"Line_{index}";
            line.ToolTip = patternName;
            line.StartCap = LineAnchorCapStyle.Round;
            line.EndCap = LineAnchorCapStyle.Arrow;
            line.LineColor = patternColor;
            line.LineWidth = 2;
            line.LineDashStyle = ChartDashStyle.Solid;
            line.AllowMoving = false;
            line.AllowSelecting = false;

            line.SetAnchor(
                chart_candlestick.Series["Series_OHLC"].Points[index - 1],
                chart_candlestick.Series["Series_OHLC"].Points[index]);

            chart_candlestick.Annotations.Add(line);

            // arrow pointing at second candle
            ArrowAnnotation arrow = new ArrowAnnotation();
            arrow.Name = $"Arrow_{index}";
            arrow.ToolTip = patternName;
            arrow.Height = -4;
            arrow.Width = 0;
            arrow.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            arrow.ArrowStyle = ArrowStyle.Simple;
            arrow.ArrowSize = 3;
            arrow.LineColor = patternColor;
            arrow.BackColor = patternColor;
            arrow.LineWidth = 2;
            arrow.AllowMoving = false;
            arrow.AllowSelecting = false;

            chart_candlestick.Annotations.Add(arrow);

            // text label
            TextAnnotation text = new TextAnnotation();
            text.Name = $"Text_{index}";
            text.Text = patternName;
            text.ForeColor = patternColor;
            text.Font = new Font("Arial", 7, FontStyle.Bold);
            text.AnchorDataPoint = chart_candlestick.Series["Series_OHLC"].Points[index];
            text.AnchorOffsetY = -6;
            text.AllowMoving = false;
            text.AllowSelecting = false;
            text.AllowAnchorMoving = false;

            chart_candlestick.Annotations.Add(text);
        }

        /// <summary>
        /// Refresh button click - refreshes the chart
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            // stop animation if running
            timer_ticker.Stop();

            // refresh the chart
            RefreshChart();

            // run pattern recognition
            RunPatternRecognition();
        }

        /// <summary>
        /// Scrollbar changed - update the timer interval
        /// </summary>
        private void hScrollBar_timerInterval_Scroll(object sender, ScrollEventArgs e)
        {
            // update timer
            timer_ticker.Interval = hScrollBar_timerInterval.Value;

            // update textbox
            if (textBox_timerInterval.Text != hScrollBar_timerInterval.Value.ToString())
            {
                textBox_timerInterval.Text = hScrollBar_timerInterval.Value.ToString();
            }
        }

        /// <summary>
        /// Textbox changed - this gives me 2-way binding with the scrollbar
        /// </summary>
        private void textBox_timerInterval_TextChanged(object sender, EventArgs e)
        {
            // try to parse the value
            if (int.TryParse(textBox_timerInterval.Text, out int value))
            {
                // clamp to valid range
                value = Math.Max(100, Math.Min(2000, value));

                // update scrollbar
                if (hScrollBar_timerInterval.Value != value)
                {
                    hScrollBar_timerInterval.Value = value;
                }

                // update timer
                timer_ticker.Interval = value;
            }
        }

        /// <summary>
        /// Start button click - starts the animation
        /// </summary>
        private void button_startAnimation_Click(object sender, EventArgs e)
        {
            // reset animation
            currentCandlestickIndex = 0;
            displayedCandlesticks.Clear();

            // clear annotations
            ClearAnnotations();

            // rebind empty list
            BindChartData();

            // start timer
            timer_ticker.Start();
        }

        /// <summary>
        /// Stop button click - stops the animation
        /// </summary>
        private void button_stopAnimation_Click(object sender, EventArgs e)
        {
            timer_ticker.Stop();
        }

        /// <summary>
        /// Timer tick - adds one more candlestick to the display
        /// </summary>
        private void timer_ticker_Tick(object sender, EventArgs e)
        {
            // check if there are more candles to show
            if (currentCandlestickIndex < allCandlesticks.Count)
            {
                // add the next one
                displayedCandlesticks.Add(allCandlesticks[currentCandlestickIndex]);
                currentCandlestickIndex++;

                // refresh chart
                BindChartData();
                NormalizeChartYAxis();

                chart_candlestick.Invalidate();
            }
            else
            {
                // done - stop timer and run pattern recognition
                timer_ticker.Stop();
                RunPatternRecognition();
            }
        }

        /// <summary>
        /// Pattern combobox changed - run pattern recognition
        /// </summary>
        private void comboBox_patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // only run if animation isn't running and we have data
            if (!timer_ticker.Enabled && displayedCandlesticks.Count > 0)
            {
                RunPatternRecognition();
            }
        }
    }
}
