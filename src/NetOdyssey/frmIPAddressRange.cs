using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetOdyssey
{
	public partial class frmIPAddressRange : Form
	{
		public frmIPAddressRange()
		{
			InitializeComponent();
		}

		#region KeyPress
		private void textBoxLowerIPAddress1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxLowerIPAddress2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxLowerIPAddress3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxLowerIPAddress4_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxUpperIPAddress1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxUpperIPAddress2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxUpperIPAddress3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}	
		#endregion

		private bool CheckInput(string inString)
		{
			if (!inString.Equals(""))
			{
				if (Convert.ToInt32(inString) > 255)
					return false;
				else
					return true;
			}
			else
				return false;
		}

		#region Leave
		private void textBoxLowerIPAddress1_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxLowerIPAddress1.Text))
				textBoxLowerIPAddress1.Text = "";
		}
		private void textBoxLowerIPAddress2_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxLowerIPAddress2.Text))
				textBoxLowerIPAddress2.Text = "";
		}
		private void textBoxLowerIPAddress3_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxLowerIPAddress3.Text))
				textBoxLowerIPAddress3.Text = "";
		}
		private void textBoxLowerIPAddress4_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxLowerIPAddress4.Text))
				textBoxLowerIPAddress4.Text = "";
		}
		private void textBoxUpperIPAddress1_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxUpperIPAddress1.Text))
				textBoxUpperIPAddress1.Text = "";
		}
		private void textBoxUpperIPAddress2_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxUpperIPAddress2.Text))
				textBoxUpperIPAddress2.Text = "";
		}
		private void textBoxUpperIPAddress3_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxUpperIPAddress3.Text))
				textBoxUpperIPAddress3.Text = "";
		}
		private void textBoxUpperIPAddress4_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxUpperIPAddress4.Text))
				textBoxUpperIPAddress4.Text = "";
		}
		#endregion

		private void buttonOk_Click(object sender, EventArgs e)
		{
			string _lowerIPAddress = "";
			string _upperIPAddress = "";
			
			for (int i = 0; i < tableLayoutPanelLowerIPAddress.Controls.Count; i++)
			{
				TextBox _textBox1 = tableLayoutPanelLowerIPAddress.Controls[i] as TextBox;
				TextBox _textBox2 = tableLayoutPanelUpperIPAddress.Controls[i] as TextBox;

				if (_textBox1 != null && _textBox2 != null )
				{
					if (_textBox1.Text.Equals("") || _textBox2.Text.Equals(""))
					{
						this.DialogResult = System.Windows.Forms.DialogResult.None;
						clsMessages.ShowMessageBox("Invalid input parameters. Please rectify.");
						return;
					}
					else
					{
						_lowerIPAddress += _textBox1.Text + ".";
						_upperIPAddress += _textBox2.Text + ".";
					}
				}
			}

			// Remove the unwanted last dots
			_lowerIPAddress = _lowerIPAddress.Remove(_lowerIPAddress.Length - 1, 1);
			_upperIPAddress = _upperIPAddress.Remove(_upperIPAddress.Length - 1, 1);

			Program.prpSettings.LowerIPAddress = System.Net.IPAddress.Parse(_lowerIPAddress);
			Program.prpSettings.UpperIPAddress = System.Net.IPAddress.Parse(_upperIPAddress);
		}	
	}
}
