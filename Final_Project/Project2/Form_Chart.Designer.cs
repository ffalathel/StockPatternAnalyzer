namespace Project2
{
    partial class Form_Chart
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlestick;
        private System.Windows.Forms.Button button_refresh; // optional local refresh

        /// <summary>
        /// Dispose resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose(); // dispose components
            }
            base.Dispose(disposing); // call base
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initialize UI controls for Form_Chart.
        /// </summary>
        private void InitializeComponent()
        {
            this.chart_candlestick = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_refresh = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.chart_candlestick)).BeginInit();
            this.SuspendLayout();

            // chart_candlestick
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

            // Form_Chart
            this.ClientSize = new System.Drawing.Size(1140, 580);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.chart_candlestick);
            this.Name = "form_chart";
            this.Text = "Chart";

            ((System.ComponentModel.ISupportInitialize)(this.chart_candlestick)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
