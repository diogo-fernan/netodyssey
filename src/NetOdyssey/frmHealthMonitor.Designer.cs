namespace NetOdyssey
{
    partial class frmHealthMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHealthMonitor));
            this.flpModules = new System.Windows.Forms.FlowLayoutPanel();
            this.tmrHealthReport = new System.Windows.Forms.Timer();
            this.bgwHealthReport = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // flpModules
            // 
            this.flpModules.AutoScroll = true;
            this.flpModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpModules.Location = new System.Drawing.Point(0, 0);
            this.flpModules.Margin = new System.Windows.Forms.Padding(0);
            this.flpModules.Name = "flpModules";
            this.flpModules.Size = new System.Drawing.Size(374, 261);
            this.flpModules.TabIndex = 4;
            // 
            // tmrHealthReport
            // 
            this.tmrHealthReport.Interval = 1000;
            // 
            // bgwHealthReport
            // 
            this.bgwHealthReport.WorkerReportsProgress = true;
            this.bgwHealthReport.WorkerSupportsCancellation = true;
            this.bgwHealthReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHealthReport_DoWork);
            this.bgwHealthReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwHealthReport_ProgressChanged);
            // 
            // frmHealthMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 261);
            this.Controls.Add(this.flpModules);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 150);
            this.Name = "frmHealthMonitor";
            this.Text = "NetOdyssey - Health Monitor";
            this.Shown += new System.EventHandler(this.frmHealthMonitor_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpModules;
        private System.Windows.Forms.Timer tmrHealthReport;
        private System.ComponentModel.BackgroundWorker bgwHealthReport;
    }
}