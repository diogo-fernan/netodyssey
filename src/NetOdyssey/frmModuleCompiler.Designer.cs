namespace NetOdyssey
{
    partial class frmModuleCompiler
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModuleCompiler));
			this.trvCompileResults = new System.Windows.Forms.TreeView();
			this.imlCompileResults = new System.Windows.Forms.ImageList(this.components);
			this.btnStart = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// trvCompileResults
			// 
			this.trvCompileResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.trvCompileResults.ImageIndex = 0;
			this.trvCompileResults.ImageList = this.imlCompileResults;
			this.trvCompileResults.Location = new System.Drawing.Point(12, 12);
			this.trvCompileResults.Name = "trvCompileResults";
			this.trvCompileResults.SelectedImageIndex = 1;
			this.trvCompileResults.Size = new System.Drawing.Size(323, 158);
			this.trvCompileResults.TabIndex = 0;
			this.trvCompileResults.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCompileResults_AfterSelect);
			// 
			// imlCompileResults
			// 
			this.imlCompileResults.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlCompileResults.ImageStream")));
			this.imlCompileResults.TransparentColor = System.Drawing.Color.Transparent;
			this.imlCompileResults.Images.SetKeyName(0, "Ok");
			this.imlCompileResults.Images.SetKeyName(1, "Warning");
			this.imlCompileResults.Images.SetKeyName(2, "Error");
			this.imlCompileResults.Images.SetKeyName(3, "Information");
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnStart.Location = new System.Drawing.Point(265, 176);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(70, 24);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(12, 176);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(70, 24);
			this.btnExit.TabIndex = 5;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			// 
			// frmModuleCompiler
			// 
			this.AcceptButton = this.btnStart;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(347, 212);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.trvCompileResults);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(240, 250);
			this.Name = "frmModuleCompiler";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Module Compile Results";
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView trvCompileResults;
        public System.Windows.Forms.ImageList imlCompileResults;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Button btnStart;

    }
}