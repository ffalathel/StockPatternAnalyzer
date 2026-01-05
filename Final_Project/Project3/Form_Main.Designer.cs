namespace Project3
{
    partial class Form_Main
    {
        private System.ComponentModel.IContainer components = null;

        // button to load stock files
        private System.Windows.Forms.Button button_loadFiles;

        // button to refresh all charts
        private System.Windows.Forms.Button button_refreshAll;

        // date picker for start date
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;

        // date picker for end date
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;

        // label for start date
        private System.Windows.Forms.Label label_start;

        // label for end date
        private System.Windows.Forms.Label label_end;

        // file dialog for selecting CSV files
        private System.Windows.Forms.OpenFileDialog openFileDialog_files;

        // panel that holds the first chart
        private System.Windows.Forms.Panel panel_hostChart;

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
            this.button_loadFiles = new System.Windows.Forms.Button();
            this.button_refreshAll = new System.Windows.Forms.Button();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label_start = new System.Windows.Forms.Label();
            this.label_end = new System.Windows.Forms.Label();
            this.openFileDialog_files = new System.Windows.Forms.OpenFileDialog();
            this.panel_hostChart = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // button_loadFiles
            this.button_loadFiles.Location = new System.Drawing.Point(16, 12);
            this.button_loadFiles.Name = "button_loadFiles";
            this.button_loadFiles.Size = new System.Drawing.Size(140, 32);
            this.button_loadFiles.TabIndex = 0;
            this.button_loadFiles.Text = "Load Stock File(s)";
            this.button_loadFiles.UseVisualStyleBackColor = true;
            this.button_loadFiles.Click += new System.EventHandler(this.button_loadFiles_Click);

            // button_refreshAll
            this.button_refreshAll.Location = new System.Drawing.Point(168, 12);
            this.button_refreshAll.Name = "button_refreshAll";
            this.button_refreshAll.Size = new System.Drawing.Size(120, 32);
            this.button_refreshAll.TabIndex = 1;
            this.button_refreshAll.Text = "Refresh All";
            this.button_refreshAll.UseVisualStyleBackColor = true;
            this.button_refreshAll.Click += new System.EventHandler(this.button_refreshAll_Click);

            // label_start
            this.label_start.AutoSize = true;
            this.label_start.Location = new System.Drawing.Point(310, 18);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(58, 13);
            this.label_start.TabIndex = 2;
            this.label_start.Text = "Start Date:";

            // dateTimePicker_start
            this.dateTimePicker_start.Location = new System.Drawing.Point(390, 14);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(160, 20);
            this.dateTimePicker_start.TabIndex = 3;

            // label_end
            this.label_end.AutoSize = true;
            this.label_end.Location = new System.Drawing.Point(566, 18);
            this.label_end.Name = "label_end";
            this.label_end.Size = new System.Drawing.Size(55, 13);
            this.label_end.TabIndex = 4;
            this.label_end.Text = "End Date:";

            // dateTimePicker_end
            this.dateTimePicker_end.Location = new System.Drawing.Point(640, 14);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(160, 20);
            this.dateTimePicker_end.TabIndex = 5;

            // openFileDialog_files
            this.openFileDialog_files.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            this.openFileDialog_files.Multiselect = true;
            this.openFileDialog_files.Title = "Select Stock CSV File(s)";
            this.openFileDialog_files.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_files_FileOk);

            // panel_hostChart - where the first chart goes
            this.panel_hostChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_hostChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_hostChart.Location = new System.Drawing.Point(16, 56);
            this.panel_hostChart.Name = "panel_hostChart";
            this.panel_hostChart.Size = new System.Drawing.Size(1250, 720);
            this.panel_hostChart.TabIndex = 6;

            // Form_Main
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 791);
            this.Controls.Add(this.panel_hostChart);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.label_end);
            this.Controls.Add(this.dateTimePicker_start);
            this.Controls.Add(this.label_start);
            this.Controls.Add(this.button_refreshAll);
            this.Controls.Add(this.button_loadFiles);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Pattern Analyzer - Project 3";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
