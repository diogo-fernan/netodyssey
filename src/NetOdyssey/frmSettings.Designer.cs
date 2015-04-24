namespace NetOdyssey
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
			this.cmbDevice = new System.Windows.Forms.ComboBox();
			this.prgSettings = new System.Windows.Forms.PropertyGrid();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.rdbDevice = new System.Windows.Forms.RadioButton();
			this.rdbOfflineCapture = new System.Windows.Forms.RadioButton();
			this.txtOfflineCaptureFile = new System.Windows.Forms.TextBox();
			this.btnFindCaptureFile = new System.Windows.Forms.Button();
			this.ofdCaptureFile = new System.Windows.Forms.OpenFileDialog();
			this.btnFindModulesFolder = new System.Windows.Forms.Button();
			this.txtModulesFolder = new System.Windows.Forms.TextBox();
			this.lblModuleFolder = new System.Windows.Forms.Label();
			this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.lblReportsFolder = new System.Windows.Forms.Label();
			this.btnFindReportsFolder = new System.Windows.Forms.Button();
			this.txtReportsFolder = new System.Windows.Forms.TextBox();
			this.lclTcpdumpFilter = new System.Windows.Forms.Label();
			this.txtTcpDumpFilter = new System.Windows.Forms.TextBox();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.netOdysseyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.analysisSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bitCountPerTimeUnitBCPTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.allTrafficToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.perSourceIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.perApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.netOdysseyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbDevice
			// 
			this.cmbDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDevice.Enabled = false;
			this.cmbDevice.FormattingEnabled = true;
			this.cmbDevice.Location = new System.Drawing.Point(101, 27);
			this.cmbDevice.Name = "cmbDevice";
			this.cmbDevice.Size = new System.Drawing.Size(346, 21);
			this.cmbDevice.TabIndex = 1;
			// 
			// prgSettings
			// 
			this.prgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.prgSettings.Location = new System.Drawing.Point(12, 158);
			this.prgSettings.Name = "prgSettings";
			this.prgSettings.Size = new System.Drawing.Size(435, 307);
			this.prgSettings.TabIndex = 10;
			this.prgSettings.ToolbarVisible = false;
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(12, 471);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(70, 24);
			this.btnExit.TabIndex = 12;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(377, 471);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(70, 24);
			this.btnStart.TabIndex = 11;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// rdbDevice
			// 
			this.rdbDevice.AutoSize = true;
			this.rdbDevice.Location = new System.Drawing.Point(12, 28);
			this.rdbDevice.Name = "rdbDevice";
			this.rdbDevice.Size = new System.Drawing.Size(59, 17);
			this.rdbDevice.TabIndex = 0;
			this.rdbDevice.TabStop = true;
			this.rdbDevice.Text = "Device";
			this.rdbDevice.UseVisualStyleBackColor = true;
			this.rdbDevice.CheckedChanged += new System.EventHandler(this.rdbDevice_CheckedChanged);
			// 
			// rdbOfflineCapture
			// 
			this.rdbOfflineCapture.AutoSize = true;
			this.rdbOfflineCapture.Location = new System.Drawing.Point(12, 55);
			this.rdbOfflineCapture.Name = "rdbOfflineCapture";
			this.rdbOfflineCapture.Size = new System.Drawing.Size(78, 17);
			this.rdbOfflineCapture.TabIndex = 2;
			this.rdbOfflineCapture.TabStop = true;
			this.rdbOfflineCapture.Text = "Capture file";
			this.rdbOfflineCapture.UseVisualStyleBackColor = true;
			this.rdbOfflineCapture.CheckedChanged += new System.EventHandler(this.rdbOfflineCapture_CheckedChanged);
			this.rdbOfflineCapture.Click += new System.EventHandler(this.rdbOfflineCapture_Click);
			// 
			// txtOfflineCaptureFile
			// 
			this.txtOfflineCaptureFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOfflineCaptureFile.Enabled = false;
			this.txtOfflineCaptureFile.Location = new System.Drawing.Point(101, 54);
			this.txtOfflineCaptureFile.Name = "txtOfflineCaptureFile";
			this.txtOfflineCaptureFile.Size = new System.Drawing.Size(311, 20);
			this.txtOfflineCaptureFile.TabIndex = 3;
			this.txtOfflineCaptureFile.Click += new System.EventHandler(this.txtOfflineCaptureFile_Click);
			// 
			// btnFindCaptureFile
			// 
			this.btnFindCaptureFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindCaptureFile.Enabled = false;
			this.btnFindCaptureFile.Location = new System.Drawing.Point(418, 54);
			this.btnFindCaptureFile.Name = "btnFindCaptureFile";
			this.btnFindCaptureFile.Size = new System.Drawing.Size(29, 20);
			this.btnFindCaptureFile.TabIndex = 4;
			this.btnFindCaptureFile.Text = "...";
			this.btnFindCaptureFile.UseVisualStyleBackColor = true;
			this.btnFindCaptureFile.Click += new System.EventHandler(this.btnFindCaptureFile_Click);
			// 
			// ofdCaptureFile
			// 
			this.ofdCaptureFile.Title = "Select offline capture file";
			// 
			// btnFindModulesFolder
			// 
			this.btnFindModulesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindModulesFolder.Location = new System.Drawing.Point(418, 106);
			this.btnFindModulesFolder.Name = "btnFindModulesFolder";
			this.btnFindModulesFolder.Size = new System.Drawing.Size(29, 20);
			this.btnFindModulesFolder.TabIndex = 7;
			this.btnFindModulesFolder.Text = "...";
			this.btnFindModulesFolder.UseVisualStyleBackColor = true;
			this.btnFindModulesFolder.Click += new System.EventHandler(this.btnFindModulesRootFolder_Click);
			// 
			// txtModulesFolder
			// 
			this.txtModulesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtModulesFolder.Location = new System.Drawing.Point(101, 106);
			this.txtModulesFolder.Name = "txtModulesFolder";
			this.txtModulesFolder.ReadOnly = true;
			this.txtModulesFolder.Size = new System.Drawing.Size(311, 20);
			this.txtModulesFolder.TabIndex = 6;
			this.txtModulesFolder.Click += new System.EventHandler(this.txtModulesFolder_Click);
			// 
			// lblModuleFolder
			// 
			this.lblModuleFolder.AutoSize = true;
			this.lblModuleFolder.Location = new System.Drawing.Point(9, 109);
			this.lblModuleFolder.Name = "lblModuleFolder";
			this.lblModuleFolder.Size = new System.Drawing.Size(76, 13);
			this.lblModuleFolder.TabIndex = 11;
			this.lblModuleFolder.Text = "Modules folder";
			// 
			// lblReportsFolder
			// 
			this.lblReportsFolder.AutoSize = true;
			this.lblReportsFolder.Location = new System.Drawing.Point(9, 135);
			this.lblReportsFolder.Name = "lblReportsFolder";
			this.lblReportsFolder.Size = new System.Drawing.Size(73, 13);
			this.lblReportsFolder.TabIndex = 14;
			this.lblReportsFolder.Text = "Reports folder";
			// 
			// btnFindReportsFolder
			// 
			this.btnFindReportsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindReportsFolder.Location = new System.Drawing.Point(418, 132);
			this.btnFindReportsFolder.Name = "btnFindReportsFolder";
			this.btnFindReportsFolder.Size = new System.Drawing.Size(29, 20);
			this.btnFindReportsFolder.TabIndex = 9;
			this.btnFindReportsFolder.Text = "...";
			this.btnFindReportsFolder.UseVisualStyleBackColor = true;
			this.btnFindReportsFolder.Click += new System.EventHandler(this.btnFindReportsFolder_Click);
			// 
			// txtReportsFolder
			// 
			this.txtReportsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtReportsFolder.Location = new System.Drawing.Point(101, 132);
			this.txtReportsFolder.Name = "txtReportsFolder";
			this.txtReportsFolder.ReadOnly = true;
			this.txtReportsFolder.Size = new System.Drawing.Size(311, 20);
			this.txtReportsFolder.TabIndex = 8;
			this.txtReportsFolder.Click += new System.EventHandler(this.txtReportsFolder_Click);
			// 
			// lclTcpdumpFilter
			// 
			this.lclTcpdumpFilter.AutoSize = true;
			this.lclTcpdumpFilter.Location = new System.Drawing.Point(9, 83);
			this.lclTcpdumpFilter.Name = "lclTcpdumpFilter";
			this.lclTcpdumpFilter.Size = new System.Drawing.Size(70, 13);
			this.lclTcpdumpFilter.TabIndex = 16;
			this.lclTcpdumpFilter.Text = "tcpdump filter";
			// 
			// txtTcpDumpFilter
			// 
			this.txtTcpDumpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTcpDumpFilter.Location = new System.Drawing.Point(101, 80);
			this.txtTcpDumpFilter.Name = "txtTcpDumpFilter";
			this.txtTcpDumpFilter.Size = new System.Drawing.Size(346, 20);
			this.txtTcpDumpFilter.TabIndex = 5;
			// 
			// menuStrip
			// 
			this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netOdysseyToolStripMenuItem1,
            this.analysisSettingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(459, 24);
			this.menuStrip.TabIndex = 17;
			this.menuStrip.Text = "menuStrip1";
			// 
			// netOdysseyToolStripMenuItem1
			// 
			this.netOdysseyToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
			this.netOdysseyToolStripMenuItem1.Name = "netOdysseyToolStripMenuItem1";
			this.netOdysseyToolStripMenuItem1.Size = new System.Drawing.Size(82, 20);
			this.netOdysseyToolStripMenuItem1.Text = "NetOdyssey";
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quitToolStripMenuItem.Image")));
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.quitToolStripMenuItem.Text = "&Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// analysisSettingsToolStripMenuItem
			// 
			this.analysisSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitCountPerTimeUnitBCPTToolStripMenuItem,
            this.toolStripSeparator,
            this.allTrafficToolStripMenuItem,
            this.perSourceIPToolStripMenuItem,
            this.perApplicationToolStripMenuItem});
			this.analysisSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("analysisSettingsToolStripMenuItem.Image")));
			this.analysisSettingsToolStripMenuItem.Name = "analysisSettingsToolStripMenuItem";
			this.analysisSettingsToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
			this.analysisSettingsToolStripMenuItem.Text = "Analysis Settings";
			// 
			// bitCountPerTimeUnitBCPTToolStripMenuItem
			// 
			this.bitCountPerTimeUnitBCPTToolStripMenuItem.Name = "bitCountPerTimeUnitBCPTToolStripMenuItem";
			this.bitCountPerTimeUnitBCPTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.bitCountPerTimeUnitBCPTToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.bitCountPerTimeUnitBCPTToolStripMenuItem.Text = "Bit Count Per Time Unit (BCTU)";
			this.bitCountPerTimeUnitBCPTToolStripMenuItem.Click += new System.EventHandler(this.bitCountPerTimeUnitBCPTToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(278, 6);
			// 
			// allTrafficToolStripMenuItem
			// 
			this.allTrafficToolStripMenuItem.Checked = true;
			this.allTrafficToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.allTrafficToolStripMenuItem.Name = "allTrafficToolStripMenuItem";
			this.allTrafficToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.allTrafficToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.allTrafficToolStripMenuItem.Text = "&All Traffic";
			this.allTrafficToolStripMenuItem.Click += new System.EventHandler(this.allTrafficToolStripMenuItem_Click);
			// 
			// perSourceIPToolStripMenuItem
			// 
			this.perSourceIPToolStripMenuItem.Name = "perSourceIPToolStripMenuItem";
			this.perSourceIPToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.perSourceIPToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.perSourceIPToolStripMenuItem.Text = "Per Source &IP";
			this.perSourceIPToolStripMenuItem.Click += new System.EventHandler(this.perSourceIPToolStripMenuItem_Click);
			// 
			// perApplicationToolStripMenuItem
			// 
			this.perApplicationToolStripMenuItem.Name = "perApplicationToolStripMenuItem";
			this.perApplicationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.perApplicationToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.perApplicationToolStripMenuItem.Text = "Per A&pplication";
			this.perApplicationToolStripMenuItem.Click += new System.EventHandler(this.perApplicationToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.netOdysseyToolStripMenuItem});
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.helpToolStripMenuItem.Text = "&Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// netOdysseyToolStripMenuItem
			// 
			this.netOdysseyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("netOdysseyToolStripMenuItem.Image")));
			this.netOdysseyToolStripMenuItem.Name = "netOdysseyToolStripMenuItem";
			this.netOdysseyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.netOdysseyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.netOdysseyToolStripMenuItem.Text = "&NetOdyssey";
			this.netOdysseyToolStripMenuItem.Click += new System.EventHandler(this.netOdysseyToolStripMenuItem_Click);
			// 
			// frmSettings
			// 
			this.AcceptButton = this.btnStart;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(459, 507);
			this.Controls.Add(this.lclTcpdumpFilter);
			this.Controls.Add(this.txtTcpDumpFilter);
			this.Controls.Add(this.lblReportsFolder);
			this.Controls.Add(this.btnFindReportsFolder);
			this.Controls.Add(this.txtReportsFolder);
			this.Controls.Add(this.lblModuleFolder);
			this.Controls.Add(this.btnFindModulesFolder);
			this.Controls.Add(this.txtModulesFolder);
			this.Controls.Add(this.btnFindCaptureFile);
			this.Controls.Add(this.txtOfflineCaptureFile);
			this.Controls.Add(this.rdbOfflineCapture);
			this.Controls.Add(this.rdbDevice);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.cmbDevice);
			this.Controls.Add(this.prgSettings);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new System.Drawing.Size(475, 545);
			this.Name = "frmSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NetOdyssey: Capture Settings";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.PropertyGrid prgSettings;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RadioButton rdbDevice;
        private System.Windows.Forms.RadioButton rdbOfflineCapture;
        private System.Windows.Forms.TextBox txtOfflineCaptureFile;
        private System.Windows.Forms.Button btnFindCaptureFile;
        private System.Windows.Forms.OpenFileDialog ofdCaptureFile;
        private System.Windows.Forms.Button btnFindModulesFolder;
        private System.Windows.Forms.TextBox txtModulesFolder;
        private System.Windows.Forms.Label lblModuleFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Label lblReportsFolder;
        private System.Windows.Forms.Button btnFindReportsFolder;
        private System.Windows.Forms.TextBox txtReportsFolder;
        private System.Windows.Forms.Label lclTcpdumpFilter;
		private System.Windows.Forms.TextBox txtTcpDumpFilter;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem analysisSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem netOdysseyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allTrafficToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem netOdysseyToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perSourceIPToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bitCountPerTimeUnitBCPTToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}