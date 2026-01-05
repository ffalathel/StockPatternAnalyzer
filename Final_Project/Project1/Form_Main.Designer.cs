namespace Project1
{
    partial class Form_Main
    {

        /// Required designer variable.

        private System.ComponentModel.IContainer components = null;

        /// Clean up any resources being used.
       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFileDialog_Stock = new System.Windows.Forms.OpenFileDialog();
            this.button_LoadStock = new System.Windows.Forms.Button();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label_StartDate = new System.Windows.Forms.Label();
            this.label_EndDate = new System.Windows.Forms.Label();
            this.button_Update = new System.Windows.Forms.Button();
            this.dataGridView_Candlesticks = new System.Windows.Forms.DataGridView();
            this.chart_Candlesticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_Period = new System.Windows.Forms.ComboBox();
            this.label_Period = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Candlesticks)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog_Stock
            // 
            this.openFileDialog_Stock.DefaultExt = "CSV";
            this.openFileDialog_Stock.FileName = "ABBV-Day.CSV";
            this.openFileDialog_Stock.Filter = "All|*.csv|Month|*-Month.csv";
            this.openFileDialog_Stock.FilterIndex = 4;
            this.openFileDialog_Stock.InitialDirectory = "Stock Data";
            this.openFileDialog_Stock.Multiselect = true;
            this.openFileDialog_Stock.ReadOnlyChecked = true;
            this.openFileDialog_Stock.ShowReadOnly = true;
            this.openFileDialog_Stock.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_Stock_FileOk);
            // 
            // button_LoadStock
            // 
            this.button_LoadStock.Location = new System.Drawing.Point(501, 22);
            this.button_LoadStock.Name = "button_LoadStock";
            this.button_LoadStock.Size = new System.Drawing.Size(103, 26);
            this.button_LoadStock.TabIndex = 0;
            this.button_LoadStock.Text = "Load Ticker";
            this.button_LoadStock.UseVisualStyleBackColor = true;
            this.button_LoadStock.Click += new System.EventHandler(this.button_LoadStock_Click);
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(601, 65);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_Start.TabIndex = 1;
            this.dateTimePicker_Start.ValueChanged += new System.EventHandler(this.dateTimePicker_Start_ValueChanged);
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_End.Location = new System.Drawing.Point(601, 100);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_End.TabIndex = 2;
            this.dateTimePicker_End.ValueChanged += new System.EventHandler(this.dateTimePicker_End_ValueChanged);
            // 
            // label_StartDate
            // 
            this.label_StartDate.AutoSize = true;
            this.label_StartDate.Location = new System.Drawing.Point(510, 65);
            this.label_StartDate.Name = "label_StartDate";
            this.label_StartDate.Size = new System.Drawing.Size(58, 13);
            this.label_StartDate.TabIndex = 3;
            this.label_StartDate.Text = "Start Date:";
            this.label_StartDate.Click += new System.EventHandler(this.label_StartDate_Click);
            // 
            // label_EndDate
            // 
            this.label_EndDate.AutoSize = true;
            this.label_EndDate.Location = new System.Drawing.Point(510, 100);
            this.label_EndDate.Name = "label_EndDate";
            this.label_EndDate.Size = new System.Drawing.Size(55, 13);
            this.label_EndDate.TabIndex = 4;
            this.label_EndDate.Text = "End Date:";
            this.label_EndDate.Click += new System.EventHandler(this.label_EndDate_Click);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(627, 23);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(101, 26);
            this.button_Update.TabIndex = 5;
            this.button_Update.Text = "Refresh";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // dataGridView_Candlesticks
            // 
            this.dataGridView_Candlesticks.AllowUserToAddRows = false;
            this.dataGridView_Candlesticks.AllowUserToDeleteRows = false;
            this.dataGridView_Candlesticks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Candlesticks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Candlesticks.Location = new System.Drawing.Point(14, 12);
            this.dataGridView_Candlesticks.Name = "dataGridView_Candlesticks";
            this.dataGridView_Candlesticks.ReadOnly = true;
            this.dataGridView_Candlesticks.RowHeadersWidth = 82;
            this.dataGridView_Candlesticks.Size = new System.Drawing.Size(440, 160);
            this.dataGridView_Candlesticks.TabIndex = 6;
            this.dataGridView_Candlesticks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Candlesticks_CellContentClick);
            // 
            // chart_Candlesticks
            // 
            this.chart_Candlesticks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_Candlesticks.ChartAreas.Add(chartArea1);
            this.chart_Candlesticks.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chart_Candlesticks.Legends.Add(legend1);
            this.chart_Candlesticks.Location = new System.Drawing.Point(12, 189);
            this.chart_Candlesticks.Name = "chart_Candlesticks";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.Legend = "Legend1";
            series1.Name = "Series_OHLC";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "ChartArea_Volume";
            series2.Legend = "Legend1";
            series2.Name = "Series_Volume";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.chart_Candlesticks.Series.Add(series1);
            this.chart_Candlesticks.Series.Add(series2);
            this.chart_Candlesticks.Size = new System.Drawing.Size(756, 330);
            this.chart_Candlesticks.TabIndex = 7;
            this.chart_Candlesticks.Text = "chart1";
            this.chart_Candlesticks.Click += new System.EventHandler(this.chart_Candlesticks_Click);
            // 
            // comboBox_Period
            // 
            this.comboBox_Period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Period.FormattingEnabled = true;
            this.comboBox_Period.Items.AddRange(new object[] {
            "Day",
            "Week",
            "Month"});
            this.comboBox_Period.Location = new System.Drawing.Point(601, 135);
            this.comboBox_Period.Name = "comboBox_Period";
            this.comboBox_Period.Size = new System.Drawing.Size(200, 21);
            this.comboBox_Period.TabIndex = 8;
            this.comboBox_Period.SelectedIndexChanged += new System.EventHandler(this.comboBox_Period_SelectedIndexChanged);
            // 
            // label_Period
            // 
            this.label_Period.AutoSize = true;
            this.label_Period.Location = new System.Drawing.Point(510, 135);
            this.label_Period.Name = "label_Period";
            this.label_Period.Size = new System.Drawing.Size(40, 13);
            this.label_Period.TabIndex = 9;
            this.label_Period.Text = "Period:";
            this.label_Period.Click += new System.EventHandler(this.label_Period_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 531);
            this.Controls.Add(this.label_Period);
            this.Controls.Add(this.comboBox_Period);
            this.Controls.Add(this.chart_Candlesticks);
            this.Controls.Add(this.dataGridView_Candlesticks);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.label_EndDate);
            this.Controls.Add(this.label_StartDate);
            this.Controls.Add(this.dateTimePicker_End);
            this.Controls.Add(this.dateTimePicker_Start);
            this.Controls.Add(this.button_LoadStock);
            this.Name = "Form_Main";
            this.Text = "Stock Analysis - Candlestick Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Candlesticks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog_Stock;
        private System.Windows.Forms.Button button_LoadStock;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label_StartDate;
        private System.Windows.Forms.Label label_EndDate;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.DataGridView dataGridView_Candlesticks;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Candlesticks;
        private System.Windows.Forms.ComboBox comboBox_Period;
        private System.Windows.Forms.Label label_Period;
    }
}