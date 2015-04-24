using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace NetOdyssey
{
	public partial class frmFlowSpecifications : Form
	{
		public frmFlowSpecifications()
		{
			InitializeComponent();
		}

		#region KeyPress
		private void textBoxSourceIPAddress1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxSourceIPAddress2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxSourceIPAddress3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxSourceIPAddress4_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxDestinationIPAddress1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxDestinationIPAddress2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxDestinationIPAddress3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxDestinationIPAddress4_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}

		private void textBoxSourcePort_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
		}
		private void textBoxDestinationPort_KeyPress(object sender, KeyPressEventArgs e)
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
		private void textBoxSourceIPAddress1_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxSourceIPAddress1.Text))
				textBoxSourceIPAddress1.Text = "";
		}
		private void textBoxSourceIPAddress2_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxSourceIPAddress2.Text))
				textBoxSourceIPAddress2.Text = "";
		}
		private void textBoxSourceIPAddress3_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxSourceIPAddress3.Text))
				textBoxSourceIPAddress3.Text = "";
		}
		private void textBoxSourceIPAddress4_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxSourceIPAddress4.Text))
				textBoxSourceIPAddress4.Text = "";
		}
		private void textBoxDestinationIPAddress1_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxDestinationIPAddress1.Text))
				textBoxDestinationIPAddress1.Text = "";
		}
		private void textBoxDestinationIPAddress2_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxDestinationIPAddress2.Text))
				textBoxDestinationIPAddress2.Text = "";
		}
		private void textBoxDestinationIPAddress3_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxDestinationIPAddress3.Text))
				textBoxDestinationIPAddress3.Text = "";
		}
		private void textBoxDestinationIPAddress4_Leave(object sender, EventArgs e)
		{
			if (!CheckInput(textBoxDestinationIPAddress4.Text))
				textBoxDestinationIPAddress4.Text = "";
		}

		private void textBoxSourcePort_Leave(object sender, EventArgs e)
		{
			if (textBoxSourcePort.Text != "")
				if (Convert.ToInt32(textBoxSourcePort.Text) > 65535)
					textBoxSourcePort.Text = "";
		}
		private void textBoxDestinationPort_Leave(object sender, EventArgs e)
		{
			if (textBoxDestinationPort.Text != "")
				if (Convert.ToInt32(textBoxDestinationPort.Text) > 65535)
					textBoxDestinationPort.Text = "";
		}
		#endregion

		private void radioButtonTCP_Click(object sender, EventArgs e)
		{
			Program.prpSettings.TransportProtocol = NetOdysseyModule.TransportProtocol.TCP;
		}
		private void radioButtonUDP_Click(object sender, EventArgs e)
		{
			Program.prpSettings.TransportProtocol = NetOdysseyModule.TransportProtocol.UDP;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			string _ipAddress = null;
			if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				switch (Program.prpSettings.AnalysisSettings)
				{
					case AnalysisSettings.AllTraffic:
						if (textBoxSourceIPAddress1.Text == "" ||
							textBoxSourceIPAddress2.Text == "" ||
							textBoxSourceIPAddress3.Text == "" ||
							textBoxSourceIPAddress4.Text == "" ||
							textBoxDestinationIPAddress1.Text == "" ||
							textBoxDestinationIPAddress2.Text == "" ||
							textBoxDestinationIPAddress3.Text == "" ||
							textBoxDestinationIPAddress4.Text == "" ||
							textBoxSourcePort.Text == "" ||
							textBoxDestinationPort.Text == "")
						{
							this.DialogResult = System.Windows.Forms.DialogResult.None;
							clsMessages.ShowMessageBox("Invalid input parameters. Please rectify.");
						}
						else
						{
							_ipAddress = textBoxSourceIPAddress1.Text +
										"." + textBoxSourceIPAddress2.Text +
										"." + textBoxSourceIPAddress3.Text +
										"." + textBoxSourceIPAddress4.Text;
							Program.prpSettings.SourceIPAddress = IPAddress.Parse(_ipAddress);

							_ipAddress = null;

							_ipAddress = textBoxDestinationIPAddress1.Text +
										"." + textBoxDestinationIPAddress2.Text +
										"." + textBoxDestinationIPAddress3.Text +
										"." + textBoxDestinationIPAddress4.Text;
							Program.prpSettings.DestinationIPAddress = IPAddress.Parse(_ipAddress);

							Program.prpSettings.SourcePort = Convert.ToUInt16(textBoxSourcePort.Text);
							Program.prpSettings.DestinationPort = Convert.ToUInt16(textBoxDestinationPort.Text);
						}
						break;
					case AnalysisSettings.PerSourceIP:
						if (textBoxDestinationIPAddress1.Text == "" ||
							textBoxDestinationIPAddress2.Text == "" ||
							textBoxDestinationIPAddress3.Text == "" ||
							textBoxDestinationIPAddress4.Text == "" ||
							textBoxDestinationPort.Text == "")
						{
							this.DialogResult = System.Windows.Forms.DialogResult.None;
							clsMessages.ShowMessageBox("Invalid input parameters. Please rectify.");
						}
						else
						{
							_ipAddress = textBoxDestinationIPAddress1.Text +
										"." + textBoxDestinationIPAddress2.Text +
										"." + textBoxDestinationIPAddress3.Text +
										"." + textBoxDestinationIPAddress4.Text;
							Program.prpSettings.DestinationIPAddress = IPAddress.Parse(_ipAddress);
							Program.prpSettings.DestinationPort = Convert.ToUInt16(textBoxDestinationPort.Text);
						}
						break;
					case AnalysisSettings.PerApplication:
						if (textBoxDestinationIPAddress1.Text == "" ||
							textBoxDestinationIPAddress2.Text == "" ||
							textBoxDestinationIPAddress3.Text == "" ||
							textBoxDestinationIPAddress4.Text == "" ||
							textBoxDestinationPort.Text == "")
						{
							this.DialogResult = System.Windows.Forms.DialogResult.None;
							clsMessages.ShowMessageBox("Invalid input parameters. Please rectify.");
						}
						else
						{
							_ipAddress = textBoxDestinationIPAddress1.Text +
										"." + textBoxDestinationIPAddress2.Text +
										"." + textBoxDestinationIPAddress3.Text +
										"." + textBoxDestinationIPAddress4.Text;
							Program.prpSettings.DestinationIPAddress = IPAddress.Parse(_ipAddress);

							Program.prpSettings.DestinationPort = Convert.ToUInt16(textBoxDestinationPort.Text);
						}
						break;
				}
			}
		}
	}
}
