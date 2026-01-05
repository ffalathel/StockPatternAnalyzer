namespace Project3
{
    partial class Form_Chart
    {
        private System.ComponentModel.IContainer components = null;
        
        // the chart where I display candlesticks and volume
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlestick;
        
        // button to refresh the chart
        private System.Windows.Forms.Button button_refresh;
        
        // timer for the animation
        private System.Windows.Forms.Timer timer_ticker;
        
        // scrollbar to control animation speed (100-2000ms)
        private System.Windows.Forms.HScrollBar hScrollBar_timerInterval;
        
        // textbox showing current interval value
        private System.Windows.Forms.TextBox textBox_timerInterval;
        
        // button to start animation
        private System.Windows.Forms.Button button_startAnimation;
        
        // button to stop animation
        private System.Windows.Forms.Button button_stopAnimation;
        
        // label for the speed control
        private System.Windows.Forms.Label label_timerInterval;
        
        // label showing "ms" after the textbox
        private System.Windows.Forms.Label label_ms;
        
        // dropdown to select which pattern to look for
        private System.Windows.Forms.ComboBox comboBox_patterns;
        
        // label for the pattern dropdown
        private System.Windows.Forms.Label label_patterns;

        /// <summary>
        /// Clean up resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Sets up all the controls on the form
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chart_candlestick = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_refresh = new System.Windows.Forms.Button();
            this.timer_ticker = new System.Windows.Forms.Timer(this.components);
            this.hScrollBar_timerInterval = new System.Windows.Forms.HScrollBar();
            this.textBox_timerInterval = new System.Windows.Forms.TextBox();
            this.button_startAnimation = new System.Windows.Forms.Button();
            this.button_stopAnimation = new System.Windows.Forms.Button();
            this.label_timerInterval = new System.Windows.Forms.Label();
            this.label_ms = new System.Windows.Forms.Label();
            this.comboBox_patterns = new System.Windows.Forms.ComboBox();
            this.label_patterns = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.chart_candlestick)).BeginInit();
            this.SuspendLayout();

            // chart_candlestick - the main chart
            this.chart_candlestick.Location = new System.Drawing.Point(8, 8);
            this.chart_candlestick.Name = "chart_candlestick";
            this.chart_candlestick.Size = new System.Drawing.Size(980, 560);
            this.chart_candlestick.TabIndex = 0;
            this.chart_candlestick.Text = "chart_candlestick";

            // button_refresh
            this.button_refresh.Location = new System.Drawing.Point(1000, 8);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(120, 32);
            this.button_refresh.TabIndex = 1;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);

            // timer_ticker - default to 500ms
            this.timer_ticker.Interval = 500;
            this.timer_ticker.Tick += new System.EventHandler(this.timer_ticker_Tick);

            // label_timerInterval
            this.label_timerInterval.AutoSize = true;
            this.label_timerInterval.Location = new System.Drawing.Point(1000, 60);
            this.label_timerInterval.Name = "label_timerInterval";
            this.label_timerInterval.Size = new System.Drawing.Size(80, 13);
            this.label_timerInterval.TabIndex = 2;
            this.label_timerInterval.Text = "Animation Speed:";

            // hScrollBar_timerInterval - range 100 to 2000
            this.hScrollBar_timerInterval.Location = new System.Drawing.Point(1000, 80);
            this.hScrollBar_timerInterval.Minimum = 100;
            this.hScrollBar_timerInterval.Maximum = 2010;
            this.hScrollBar_timerInterval.LargeChange = 10;
            this.hScrollBar_timerInterval.SmallChange = 50;
            this.hScrollBar_timerInterval.Name = "hScrollBar_timerInterval";
            this.hScrollBar_timerInterval.Size = new System.Drawing.Size(200, 20);
            this.hScrollBar_timerInterval.TabIndex = 3;
            this.hScrollBar_timerInterval.Value = 500;
            this.hScrollBar_timerInterval.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_timerInterval_Scroll);

            // textBox_timerInterval - shows current value
            this.textBox_timerInterval.Location = new System.Drawing.Point(1000, 105);
            this.textBox_timerInterval.Name = "textBox_timerInterval";
            this.textBox_timerInterval.Size = new System.Drawing.Size(60, 20);
            this.textBox_timerInterval.TabIndex = 4;
            this.textBox_timerInterval.Text = "500";
            this.textBox_timerInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_timerInterval.TextChanged += new System.EventHandler(this.textBox_timerInterval_TextChanged);

            // label_ms
            this.label_ms.AutoSize = true;
            this.label_ms.Location = new System.Drawing.Point(1065, 108);
            this.label_ms.Name = "label_ms";
            this.label_ms.Size = new System.Drawing.Size(20, 13);
            this.label_ms.TabIndex = 5;
            this.label_ms.Text = "ms";

            // button_startAnimation
            this.button_startAnimation.Location = new System.Drawing.Point(1000, 140);
            this.button_startAnimation.Name = "button_startAnimation";
            this.button_startAnimation.Size = new System.Drawing.Size(95, 32);
            this.button_startAnimation.TabIndex = 6;
            this.button_startAnimation.Text = "▶ Start";
            this.button_startAnimation.UseVisualStyleBackColor = true;
            this.button_startAnimation.Click += new System.EventHandler(this.button_startAnimation_Click);

            // button_stopAnimation
            this.button_stopAnimation.Location = new System.Drawing.Point(1105, 140);
            this.button_stopAnimation.Name = "button_stopAnimation";
            this.button_stopAnimation.Size = new System.Drawing.Size(95, 32);
            this.button_stopAnimation.TabIndex = 7;
            this.button_stopAnimation.Text = "■ Stop";
            this.button_stopAnimation.UseVisualStyleBackColor = true;
            this.button_stopAnimation.Click += new System.EventHandler(this.button_stopAnimation_Click);

            // label_patterns
            this.label_patterns.AutoSize = true;
            this.label_patterns.Location = new System.Drawing.Point(1000, 190);
            this.label_patterns.Name = "label_patterns";
            this.label_patterns.Size = new System.Drawing.Size(80, 13);
            this.label_patterns.TabIndex = 8;
            this.label_patterns.Text = "Select Pattern:";

            // comboBox_patterns
            this.comboBox_patterns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_patterns.FormattingEnabled = true;
            this.comboBox_patterns.Location = new System.Drawing.Point(1000, 210);
            this.comboBox_patterns.Name = "comboBox_patterns";
            this.comboBox_patterns.Size = new System.Drawing.Size(200, 21);
            this.comboBox_patterns.TabIndex = 9;
            this.comboBox_patterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_patterns_SelectedIndexChanged);

            // Form_Chart
            this.ClientSize = new System.Drawing.Size(1220, 580);
            this.Controls.Add(this.comboBox_patterns);
            this.Controls.Add(this.label_patterns);
            this.Controls.Add(this.button_stopAnimation);
            this.Controls.Add(this.button_startAnimation);
            this.Controls.Add(this.label_ms);
            this.Controls.Add(this.textBox_timerInterval);
            this.Controls.Add(this.hScrollBar_timerInterval);
            this.Controls.Add(this.label_timerInterval);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.chart_candlestick);
            this.Name = "Form_Chart";
            this.Text = "Stock Chart";

            ((System.ComponentModel.ISupportInitialize)(this.chart_candlestick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
