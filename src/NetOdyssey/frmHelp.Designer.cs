namespace NetOdyssey
{
	partial class frmHelp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelp));
			this.txtLabel = new System.Windows.Forms.Label();
			this.okBttn = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtLabel
			// 
			this.txtLabel.AutoSize = true;
			this.txtLabel.Location = new System.Drawing.Point(12, 9);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(503, 13);
			this.txtLabel.TabIndex = 0;
			this.txtLabel.Text = "If you have any questions, suggestions or bug report, please contact us using the" +
				" following email address:";
			this.txtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// okBttn
			// 
			this.okBttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBttn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okBttn.Location = new System.Drawing.Point(457, 50);
			this.okBttn.Name = "okBttn";
			this.okBttn.Size = new System.Drawing.Size(75, 23);
			this.okBttn.TabIndex = 25;
			this.okBttn.Text = "&OK";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(109, 42);
			this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxDescription.Size = new System.Drawing.Size(207, 20);
			this.textBoxDescription.TabIndex = 26;
			this.textBoxDescription.TabStop = false;
			this.textBoxDescription.Text = "netodyssey@penhas.di.ubi.pt";
			this.textBoxDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frmHelp
			// 
			this.AcceptButton = this.okBttn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(544, 84);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.okBttn);
			this.Controls.Add(this.txtLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(550, 112);
			this.Name = "frmHelp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Help";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label txtLabel;
		private System.Windows.Forms.Button okBttn;
		private System.Windows.Forms.TextBox textBoxDescription;

	}
}