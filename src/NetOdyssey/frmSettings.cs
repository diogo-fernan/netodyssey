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
	public partial class frmSettings : Form
	{
		public frmSettings()
		{
			InitializeComponent();
			prgSettings.SelectedObject = Program.prpSettings;
			cmbDevice.Items.Clear();
			int d = 1;
			foreach (SharpPcap.PcapDevice device in Program.prpSettings.prpDevices) {                
				cmbDevice.Items.Add(d++ + ") " + device.Description);
				if (Program.prpSettings.CaptureDevice == device)
					cmbDevice.SelectedIndex = d-2;
			}
			rdbDevice.Checked = (Program.prpSettings.CaptureDevice is SharpPcap.PcapDevice);
			if (Program.prpSettings.CaptureDevice is SharpPcap.OfflinePcapDevice) {
				rdbOfflineCapture.Checked = true;
				txtOfflineCaptureFile.Text = (Program.prpSettings.CaptureDevice as SharpPcap.OfflinePcapDevice).Name;
			}
			txtModulesFolder.Text = Program.prpSettings.ModulesFolder;
			txtReportsFolder.Text = Program.prpSettings.ReportsFolder;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			try
			{
				if (rdbDevice.Checked)
				{
					if (cmbDevice.SelectedIndex < 0)
					{
						MessageBox.Show("Please select a device.", "No device selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					Program.prpSettings.CaptureDevice = Program.prpSettings.prpDevices[cmbDevice.SelectedIndex];
				}
				else if (rdbOfflineCapture.Checked)
				{
					Program.prpSettings.TcpDumpFilter = "";
					Program.prpSettings.CaptureDevice = new SharpPcap.OfflinePcapDevice(txtOfflineCaptureFile.Text);                    
				}
				else
				{
					MessageBox.Show("Please select a capture method.", "No capture method selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Program.prpSettings.TcpDumpFilter = txtTcpDumpFilter.Text;
				Program.prpSettings.checkSettings();
				this.DialogResult = DialogResult.OK;
			}
			catch (Exception ex) {
				clsMessages.ShowMessageBox(ex.Message);
			}
		}

		private void rdbDevice_CheckedChanged(object sender, EventArgs e)
		{
			cmbDevice.Enabled = rdbDevice.Checked;
		}
		private void rdbOfflineCapture_CheckedChanged(object sender, EventArgs e)
		{
			txtOfflineCaptureFile.Enabled = rdbOfflineCapture.Checked;
			btnFindCaptureFile.Enabled = rdbOfflineCapture.Checked;
			txtTcpDumpFilter.Enabled = !rdbOfflineCapture.Checked;
		}
		private void rdbOfflineCapture_Click(object sender, EventArgs e)
		{
			btnFindCaptureFile_Click(sender, e);
		}
		private void btnFindCaptureFile_Click(object sender, EventArgs e)
		{
			if (ofdCaptureFile.ShowDialog() == DialogResult.OK)                
				txtOfflineCaptureFile.Text = ofdCaptureFile.FileName;            
		}
		private void txtOfflineCaptureFile_Click(object sender, EventArgs e)
		{
			btnFindCaptureFile_Click(sender, e);
		}

		private void btnFindModulesRootFolder_Click(object sender, EventArgs e)
		{
			folderBrowser.Description = @"Folder with *.cs and *.vb files to compile and run as modules";
			folderBrowser.SelectedPath = txtModulesFolder.Text;
			if (folderBrowser.ShowDialog() == DialogResult.OK)
			{
				txtModulesFolder.Text = folderBrowser.SelectedPath;
				Program.prpSettings.ModulesFolder = txtModulesFolder.Text;
			}
		}
		private void txtModulesFolder_Click(object sender, EventArgs e)
		{
			btnFindModulesRootFolder_Click(sender, e);	
		}

		private void btnFindReportsFolder_Click(object sender, EventArgs e)
		{
			folderBrowser.Description = @"Root folder for Report folders";
			folderBrowser.SelectedPath = txtReportsFolder.Text;
			if (folderBrowser.ShowDialog() == DialogResult.OK)
			{
				txtReportsFolder.Text = folderBrowser.SelectedPath;
				Program.prpSettings.ReportsFolder = txtReportsFolder.Text;
			}
		}
		private void txtReportsFolder_Click(object sender, EventArgs e)
		{
			btnFindReportsFolder_Click(sender, e);
		}

		private void netOdysseyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Program.prpFrmAbout.ShowDialog();
		}
		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Program.prpFrmHelp.ShowDialog();
		}
		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void bitCountPerTimeUnitBCPTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bitCountPerTimeUnitBCPTToolStripMenuItem.Checked = true;
			allTrafficToolStripMenuItem.Checked = 
				perSourceIPToolStripMenuItem.Checked =
					perApplicationToolStripMenuItem.Checked = false;

			Program.prpSettings.AnalysisSettings = AnalysisSettings.BitCountPerTimeUnit;
		}
		private void allTrafficToolStripMenuItem_Click(object sender, EventArgs e)
		{
			allTrafficToolStripMenuItem.Checked = true;
			bitCountPerTimeUnitBCPTToolStripMenuItem.Checked =
				perSourceIPToolStripMenuItem.Checked =
					perApplicationToolStripMenuItem.Checked = false;

			Program.prpSettings.AnalysisSettings = AnalysisSettings.AllTraffic;

			if (Program.prpSettings.AnalysisLevel == AnalysisLevel.PacketByPacket)
			{
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = false;
				Program.prpFrmFlowSettings.groupBoxDirection.Enabled = false;
				Program.prpFrmFlowSettings.groupBoxTransportProtocol.Enabled = false;
			}
			if (Program.prpSettings.AnalysisLevel == AnalysisLevel.IntraFlow)
			{
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = false;
				Program.prpFrmFlowSettings.groupBoxDirection.Enabled = true;
				Program.prpFrmFlowSettings.groupBoxTransportProtocol.Enabled = false;
			}
			Program.prpFrmFlowSettings.buttonPacketByPacketIPAddressRange.Enabled = false;
			Program.prpFrmFlowSettings.buttonIntraFlowIPAddressRange.Enabled = false;
			Program.prpFrmFlowSettings.buttonInterFlowIPAddressRange.Enabled = false;
			Program.prpFrmFlowSettings.buttonIntraFlowFlowSettings.Enabled = true;
			Program.prpFrmFlowSettings.ShowDialog();
		}
		private void perSourceIPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			perSourceIPToolStripMenuItem.Checked = true;
			bitCountPerTimeUnitBCPTToolStripMenuItem.Checked = 
				perApplicationToolStripMenuItem.Checked =
					allTrafficToolStripMenuItem.Checked = false;

			Program.prpSettings.AnalysisSettings = AnalysisSettings.PerSourceIP;
			Program.prpFrmFlowSettings.groupBoxDirection.Enabled = true;
			if (Program.prpSettings.AnalysisLevel == AnalysisLevel.InterFlow)
			{
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = true;
			}
			else
			{
				Program.prpFrmFlowSettings.groupBoxTransportProtocol.Enabled = false;
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = false;
			}
			Program.prpFrmFlowSettings.buttonPacketByPacketIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonIntraFlowIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonInterFlowIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonIntraFlowFlowSettings.Enabled = true;
			Program.prpFrmFlowSettings.ShowDialog();
		}
		private void perApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			perApplicationToolStripMenuItem.Checked = true;
			bitCountPerTimeUnitBCPTToolStripMenuItem.Checked =
				perSourceIPToolStripMenuItem.Checked =
					allTrafficToolStripMenuItem.Checked = false;

			Program.prpSettings.AnalysisSettings = AnalysisSettings.PerApplication;
			Program.prpFrmFlowSettings.groupBoxDirection.Enabled = true;
			if (Program.prpSettings.AnalysisLevel == AnalysisLevel.InterFlow)
			{
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = true;
			}
			else
			{
				Program.prpFrmFlowSettings.groupBoxTransportProtocol.Enabled = false;
				Program.prpFrmFlowSettings.groupBoxTimeout.Enabled = false;
			}
			Program.prpFrmFlowSettings.buttonPacketByPacketIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonIntraFlowIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonInterFlowIPAddressRange.Enabled = true;
			Program.prpFrmFlowSettings.buttonIntraFlowFlowSettings.Enabled = true;
			Program.prpFrmFlowSettings.ShowDialog();
		}
	}
}
