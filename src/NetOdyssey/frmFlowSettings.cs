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
	public partial class frmFlowSettings : Form
	{
		public frmFlowSettings()
		{
			InitializeComponent();
		}

		private void txtBoxTimeout_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Program.Backspace;
        }
		private void textBoxTimeout_Leave(object sender, EventArgs e)
		{
			// The flow timeout textbox must not be 0 and must not be empty
			if (textBoxTimeout.Text.Equals("") || textBoxTimeout.Text.Equals("0"))
			{
				textBoxTimeout.Text = "64";
			}

			Program.prpSettings.FlowTimeout = textBoxTimeout.Text;
		}

		private void checkBoxTransportProtocol_CheckedChanged(object sender, EventArgs e)
		{
			Program.prpSettings.DistinguishFlowsByTransportProtocol = checkBoxTransportProtocol.Checked;
		}

		private void radioButtonUnidirectional_Click(object sender, EventArgs e)
		{
			Program.prpSettings.FlowDirection = FlowDirection.Unidirectional;
		}
		private void radioButtonBidirectional_Click(object sender, EventArgs e)
		{
			Program.prpSettings.FlowDirection = FlowDirection.Bidirectional;
		}

		private void radioButtonPacketByPacket_Click(object sender, EventArgs e)
		{
			radioButtonPacketByPacket.Checked = true;
			radioButtonIntraFlow.Checked = radioButtonInterFlow.Checked = false;
			Program.prpSettings.AnalysisLevel = AnalysisLevel.PacketByPacket;
			groupBoxTransportProtocol.Enabled = false;
			groupBoxTimeout.Enabled = false;
			if (Program.prpSettings.AnalysisSettings == AnalysisSettings.AllTraffic)
			{
				groupBoxDirection.Enabled = false;
			}
			else
			{
				groupBoxDirection.Enabled = true;
			}
		}
		private void radioButtonIntraFlow_Click(object sender, EventArgs e)
		{
			radioButtonIntraFlow.Checked = true;
			radioButtonPacketByPacket.Checked = radioButtonInterFlow.Checked = false;
			Program.prpSettings.AnalysisLevel = AnalysisLevel.IntraFlow;
			groupBoxTimeout.Enabled = false;
			groupBoxDirection.Enabled = true;
			groupBoxTransportProtocol.Enabled = false;
			if (Program.prpSettings.AnalysisSettings == AnalysisSettings.AllTraffic)
			{
				Program.prpFrmFlowSpecifications.labelSourceIPAddress.Enabled = true;
				Program.prpFrmFlowSpecifications.labelSourcePort.Enabled = true;
				Program.prpFrmFlowSpecifications.tableLayoutPanelSourceIPAddress.Enabled = true;
				Program.prpFrmFlowSpecifications.textBoxSourcePort.Enabled = true;
			}
			else if (Program.prpSettings.AnalysisSettings == AnalysisSettings.PerSourceIP)
			{
				Program.prpFrmFlowSpecifications.labelSourceIPAddress.Enabled = false;
				Program.prpFrmFlowSpecifications.labelSourcePort.Enabled = false;
				Program.prpFrmFlowSpecifications.tableLayoutPanelSourceIPAddress.Enabled = false;
				Program.prpFrmFlowSpecifications.textBoxSourcePort.Enabled = false;
			}
			else
			{
				Program.prpFrmFlowSpecifications.labelSourceIPAddress.Enabled = false;
				Program.prpFrmFlowSpecifications.labelSourcePort.Enabled = false;
				Program.prpFrmFlowSpecifications.tableLayoutPanelSourceIPAddress.Enabled = false;
				Program.prpFrmFlowSpecifications.textBoxSourcePort.Enabled = false;
			}
		}
		private void radioButtonInterFlow_Click(object sender, EventArgs e)
		{
			radioButtonInterFlow.Checked = true;
			radioButtonPacketByPacket.Checked = radioButtonIntraFlow.Checked = false;
			Program.prpSettings.AnalysisLevel = AnalysisLevel.InterFlow;
			groupBoxTransportProtocol.Enabled = true;
			groupBoxDirection.Enabled = true;
			groupBoxTimeout.Enabled = true;
		}

		private void buttonPacketByPacketIPAddressRange_Click(object sender, EventArgs e)
		{
			radioButtonPacketByPacket_Click(sender, e);
			Program.prpFrmIPAddressRange.ShowDialog();
		}
		private void buttonIntraFlowIPAddressRange_Click(object sender, EventArgs e)
		{
			radioButtonIntraFlow_Click(sender, e);
			Program.prpFrmIPAddressRange.ShowDialog();
		}
		private void buttonInterFlowIPAddressRange_Click(object sender, EventArgs e)
		{
			radioButtonInterFlow_Click(sender, e);
			Program.prpFrmIPAddressRange.ShowDialog();
		}
		private void buttonIntraFlowFlowSettings_Click(object sender, EventArgs e)
		{
			radioButtonIntraFlow_Click(sender, e);
			Program.prpFrmFlowSpecifications.ShowDialog();
		}
	}
}
