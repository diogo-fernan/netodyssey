namespace NetOdyssey
{
    partial class uctModuleHealth
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uctModuleHealth));
            this.grpModuleName = new System.Windows.Forms.GroupBox();
            this.lblTasks = new System.Windows.Forms.Label();
            this.pcbKillModule = new System.Windows.Forms.PictureBox();
            this.grpModuleName.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpModuleName
            // 
            this.grpModuleName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModuleName.Controls.Add(this.lblTasks);
            this.grpModuleName.Controls.Add(this.pcbKillModule);
            this.grpModuleName.Location = new System.Drawing.Point(3, 3);
            this.grpModuleName.Name = "grpModuleName";
            this.grpModuleName.Size = new System.Drawing.Size(344, 47);
            this.grpModuleName.TabIndex = 0;
            this.grpModuleName.TabStop = false;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(6, 19);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(0, 13);
            this.lblTasks.TabIndex = 1;
            // 
            // pcbKillModule
            // 
            this.pcbKillModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbKillModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcbKillModule.Image = ((System.Drawing.Image)(resources.GetObject("pcbKillModule.Image")));
            this.pcbKillModule.Location = new System.Drawing.Point(322, 19);
            this.pcbKillModule.Name = "pcbKillModule";
            this.pcbKillModule.Size = new System.Drawing.Size(16, 16);
            this.pcbKillModule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbKillModule.TabIndex = 0;
            this.pcbKillModule.TabStop = false;
            // 
            // uctModuleHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpModuleName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "uctModuleHealth";
            this.Size = new System.Drawing.Size(350, 53);
            this.grpModuleName.ResumeLayout(false);
            this.grpModuleName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpModuleName;
        private System.Windows.Forms.PictureBox pcbKillModule;
        private System.Windows.Forms.Label lblTasks;
    }
}
