namespace NetOdyssey
{
	partial class frmFlowSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlowSettings));
			this.groupBoxTimeout = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanelTimeout = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanelTimeout2 = new System.Windows.Forms.TableLayoutPanel();
			this.labelSeconds = new System.Windows.Forms.Label();
			this.labelTimeout = new System.Windows.Forms.Label();
			this.textBoxTimeout = new System.Windows.Forms.TextBox();
			this.labelTimeoutMain = new System.Windows.Forms.Label();
			this.labelMain = new System.Windows.Forms.Label();
			this.labelUnidirectional = new System.Windows.Forms.Label();
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxDirection = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanelDirection = new System.Windows.Forms.TableLayoutPanel();
			this.radioButtonBidirectional = new System.Windows.Forms.RadioButton();
			this.radioButtonUnidirectional = new System.Windows.Forms.RadioButton();
			this.labelBidirectional = new System.Windows.Forms.Label();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBoxAnalysisLevel = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanelAnalysisLevel = new System.Windows.Forms.TableLayoutPanel();
			this.labelPerApplication = new System.Windows.Forms.Label();
			this.labelPerSourceIPAddress = new System.Windows.Forms.Label();
			this.labelPacketByPacket = new System.Windows.Forms.Label();
			this.tableLayoutPanelPacketByPacket = new System.Windows.Forms.TableLayoutPanel();
			this.radioButtonPacketByPacket = new System.Windows.Forms.RadioButton();
			this.buttonPacketByPacketIPAddressRange = new System.Windows.Forms.Button();
			this.tableLayoutPanelIntraFlow = new System.Windows.Forms.TableLayoutPanel();
			this.buttonIntraFlowIPAddressRange = new System.Windows.Forms.Button();
			this.radioButtonIntraFlow = new System.Windows.Forms.RadioButton();
			this.buttonIntraFlowFlowSettings = new System.Windows.Forms.Button();
			this.tableLayoutPanelInterFlow = new System.Windows.Forms.TableLayoutPanel();
			this.buttonInterFlowIPAddressRange = new System.Windows.Forms.Button();
			this.radioButtonInterFlow = new System.Windows.Forms.RadioButton();
			this.groupBoxTransportProtocol = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanelTransportProtocol = new System.Windows.Forms.TableLayoutPanel();
			this.labelTransportProtocol = new System.Windows.Forms.Label();
			this.checkBoxTransportProtocol = new System.Windows.Forms.CheckBox();
			this.groupBoxTimeout.SuspendLayout();
			this.tableLayoutPanelTimeout.SuspendLayout();
			this.tableLayoutPanelTimeout2.SuspendLayout();
			this.tableLayoutPanelMain.SuspendLayout();
			this.groupBoxDirection.SuspendLayout();
			this.tableLayoutPanelDirection.SuspendLayout();
			this.panelButtons.SuspendLayout();
			this.groupBoxAnalysisLevel.SuspendLayout();
			this.tableLayoutPanelAnalysisLevel.SuspendLayout();
			this.tableLayoutPanelPacketByPacket.SuspendLayout();
			this.tableLayoutPanelIntraFlow.SuspendLayout();
			this.tableLayoutPanelInterFlow.SuspendLayout();
			this.groupBoxTransportProtocol.SuspendLayout();
			this.tableLayoutPanelTransportProtocol.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxTimeout
			// 
			this.groupBoxTimeout.Controls.Add(this.tableLayoutPanelTimeout);
			this.groupBoxTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxTimeout.Location = new System.Drawing.Point(3, 41);
			this.groupBoxTimeout.Name = "groupBoxTimeout";
			this.groupBoxTimeout.Size = new System.Drawing.Size(674, 79);
			this.groupBoxTimeout.TabIndex = 1;
			this.groupBoxTimeout.TabStop = false;
			this.groupBoxTimeout.Text = "Timeout";
			// 
			// tableLayoutPanelTimeout
			// 
			this.tableLayoutPanelTimeout.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanelTimeout.ColumnCount = 1;
			this.tableLayoutPanelTimeout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelTimeout.Controls.Add(this.tableLayoutPanelTimeout2, 0, 1);
			this.tableLayoutPanelTimeout.Controls.Add(this.labelTimeoutMain, 0, 0);
			this.tableLayoutPanelTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelTimeout.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanelTimeout.Name = "tableLayoutPanelTimeout";
			this.tableLayoutPanelTimeout.RowCount = 2;
			this.tableLayoutPanelTimeout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.76119F));
			this.tableLayoutPanelTimeout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.23881F));
			this.tableLayoutPanelTimeout.Size = new System.Drawing.Size(668, 60);
			this.tableLayoutPanelTimeout.TabIndex = 17;
			// 
			// tableLayoutPanelTimeout2
			// 
			this.tableLayoutPanelTimeout2.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanelTimeout2.ColumnCount = 3;
			this.tableLayoutPanelTimeout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanelTimeout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanelTimeout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanelTimeout2.Controls.Add(this.labelSeconds, 2, 0);
			this.tableLayoutPanelTimeout2.Controls.Add(this.labelTimeout, 0, 0);
			this.tableLayoutPanelTimeout2.Controls.Add(this.textBoxTimeout, 1, 0);
			this.tableLayoutPanelTimeout2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelTimeout2.Location = new System.Drawing.Point(3, 31);
			this.tableLayoutPanelTimeout2.Name = "tableLayoutPanelTimeout2";
			this.tableLayoutPanelTimeout2.RowCount = 1;
			this.tableLayoutPanelTimeout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelTimeout2.Size = new System.Drawing.Size(662, 26);
			this.tableLayoutPanelTimeout2.TabIndex = 18;
			// 
			// labelSeconds
			// 
			this.labelSeconds.AutoSize = true;
			this.labelSeconds.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSeconds.Location = new System.Drawing.Point(399, 0);
			this.labelSeconds.Name = "labelSeconds";
			this.labelSeconds.Size = new System.Drawing.Size(260, 26);
			this.labelSeconds.TabIndex = 3;
			this.labelSeconds.Text = "seconds";
			this.labelSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelTimeout
			// 
			this.labelTimeout.AutoSize = true;
			this.labelTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTimeout.Location = new System.Drawing.Point(3, 0);
			this.labelTimeout.Name = "labelTimeout";
			this.labelTimeout.Size = new System.Drawing.Size(258, 26);
			this.labelTimeout.TabIndex = 0;
			this.labelTimeout.Text = "&Timeout:";
			this.labelTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxTimeout
			// 
			this.textBoxTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxTimeout.Location = new System.Drawing.Point(267, 3);
			this.textBoxTimeout.Name = "textBoxTimeout";
			this.textBoxTimeout.Size = new System.Drawing.Size(126, 20);
			this.textBoxTimeout.TabIndex = 0;
			this.textBoxTimeout.Text = "64";
			this.textBoxTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTimeout_KeyPress);
			this.textBoxTimeout.Leave += new System.EventHandler(this.textBoxTimeout_Leave);
			// 
			// labelTimeoutMain
			// 
			this.labelTimeoutMain.AutoSize = true;
			this.labelTimeoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTimeoutMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTimeoutMain.Location = new System.Drawing.Point(3, 0);
			this.labelTimeoutMain.Name = "labelTimeoutMain";
			this.labelTimeoutMain.Size = new System.Drawing.Size(662, 28);
			this.labelTimeoutMain.TabIndex = 0;
			this.labelTimeoutMain.Text = "The default flow timeout is 64 seconds. If you are unsure what to put, leave it a" +
				"s it is.\r\nBig values may cause bad results and lower the overal performance of a" +
				" certain analysis.";
			this.labelTimeoutMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelMain
			// 
			this.labelMain.AutoSize = true;
			this.labelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelMain.Location = new System.Drawing.Point(3, 0);
			this.labelMain.Name = "labelMain";
			this.labelMain.Size = new System.Drawing.Size(674, 38);
			this.labelMain.TabIndex = 0;
			this.labelMain.Text = resources.GetString("labelMain.Text");
			this.labelMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelUnidirectional
			// 
			this.labelUnidirectional.AutoSize = true;
			this.labelUnidirectional.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelUnidirectional.Location = new System.Drawing.Point(3, 0);
			this.labelUnidirectional.Name = "labelUnidirectional";
			this.labelUnidirectional.Size = new System.Drawing.Size(328, 27);
			this.labelUnidirectional.TabIndex = 8;
			this.labelUnidirectional.Text = "Unidirectional flows are composed by the packets of a single direction of a certa" +
				"in connection (e.g., from source to destination).";
			this.labelUnidirectional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanelMain
			// 
			this.tableLayoutPanelMain.ColumnCount = 1;
			this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.Controls.Add(this.groupBoxDirection, 0, 4);
			this.tableLayoutPanelMain.Controls.Add(this.panelButtons, 0, 5);
			this.tableLayoutPanelMain.Controls.Add(this.labelMain, 0, 0);
			this.tableLayoutPanelMain.Controls.Add(this.groupBoxTimeout, 0, 1);
			this.tableLayoutPanelMain.Controls.Add(this.groupBoxAnalysisLevel, 0, 2);
			this.tableLayoutPanelMain.Controls.Add(this.groupBoxTransportProtocol, 0, 3);
			this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			this.tableLayoutPanelMain.RowCount = 6;
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.64706F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.31624F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.16239F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(680, 401);
			this.tableLayoutPanelMain.TabIndex = 10;
			// 
			// groupBoxDirection
			// 
			this.groupBoxDirection.Controls.Add(this.tableLayoutPanelDirection);
			this.groupBoxDirection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxDirection.Location = new System.Drawing.Point(3, 287);
			this.groupBoxDirection.Name = "groupBoxDirection";
			this.groupBoxDirection.Size = new System.Drawing.Size(674, 78);
			this.groupBoxDirection.TabIndex = 5;
			this.groupBoxDirection.TabStop = false;
			this.groupBoxDirection.Text = "Direction";
			// 
			// tableLayoutPanelDirection
			// 
			this.tableLayoutPanelDirection.ColumnCount = 2;
			this.tableLayoutPanelDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelDirection.Controls.Add(this.radioButtonBidirectional, 1, 1);
			this.tableLayoutPanelDirection.Controls.Add(this.labelUnidirectional, 0, 0);
			this.tableLayoutPanelDirection.Controls.Add(this.radioButtonUnidirectional, 0, 1);
			this.tableLayoutPanelDirection.Controls.Add(this.labelBidirectional, 1, 0);
			this.tableLayoutPanelDirection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tableLayoutPanelDirection.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanelDirection.Name = "tableLayoutPanelDirection";
			this.tableLayoutPanelDirection.RowCount = 2;
			this.tableLayoutPanelDirection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.34146F));
			this.tableLayoutPanelDirection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.65854F));
			this.tableLayoutPanelDirection.Size = new System.Drawing.Size(668, 59);
			this.tableLayoutPanelDirection.TabIndex = 21;
			// 
			// radioButtonBidirectional
			// 
			this.radioButtonBidirectional.AutoSize = true;
			this.radioButtonBidirectional.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioButtonBidirectional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonBidirectional.Location = new System.Drawing.Point(354, 30);
			this.radioButtonBidirectional.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.radioButtonBidirectional.Name = "radioButtonBidirectional";
			this.radioButtonBidirectional.Size = new System.Drawing.Size(311, 26);
			this.radioButtonBidirectional.TabIndex = 1;
			this.radioButtonBidirectional.TabStop = true;
			this.radioButtonBidirectional.Text = "&Bi-directional";
			this.radioButtonBidirectional.UseVisualStyleBackColor = true;
			this.radioButtonBidirectional.Click += new System.EventHandler(this.radioButtonBidirectional_Click);
			// 
			// radioButtonUnidirectional
			// 
			this.radioButtonUnidirectional.AutoSize = true;
			this.radioButtonUnidirectional.Checked = true;
			this.radioButtonUnidirectional.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioButtonUnidirectional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonUnidirectional.Location = new System.Drawing.Point(20, 30);
			this.radioButtonUnidirectional.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.radioButtonUnidirectional.Name = "radioButtonUnidirectional";
			this.radioButtonUnidirectional.Size = new System.Drawing.Size(311, 26);
			this.radioButtonUnidirectional.TabIndex = 0;
			this.radioButtonUnidirectional.TabStop = true;
			this.radioButtonUnidirectional.Text = "&Unidirectional";
			this.radioButtonUnidirectional.UseVisualStyleBackColor = true;
			this.radioButtonUnidirectional.Click += new System.EventHandler(this.radioButtonUnidirectional_Click);
			// 
			// labelBidirectional
			// 
			this.labelBidirectional.AutoSize = true;
			this.labelBidirectional.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelBidirectional.Location = new System.Drawing.Point(337, 0);
			this.labelBidirectional.Name = "labelBidirectional";
			this.labelBidirectional.Size = new System.Drawing.Size(328, 27);
			this.labelBidirectional.TabIndex = 9;
			this.labelBidirectional.Text = "Bidirectional flows are composed by the packets of both directions of a certain c" +
				"onnection.";
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.buttonOk);
			this.panelButtons.Controls.Add(this.buttonCancel);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelButtons.Location = new System.Drawing.Point(3, 371);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(674, 27);
			this.panelButtons.TabIndex = 15;
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonOk.Location = new System.Drawing.Point(546, 0);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(128, 27);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "&OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Left;
			this.buttonCancel.Location = new System.Drawing.Point(0, 0);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(128, 27);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// groupBoxAnalysisLevel
			// 
			this.groupBoxAnalysisLevel.Controls.Add(this.tableLayoutPanelAnalysisLevel);
			this.groupBoxAnalysisLevel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxAnalysisLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxAnalysisLevel.Location = new System.Drawing.Point(3, 126);
			this.groupBoxAnalysisLevel.Name = "groupBoxAnalysisLevel";
			this.groupBoxAnalysisLevel.Size = new System.Drawing.Size(674, 87);
			this.groupBoxAnalysisLevel.TabIndex = 21;
			this.groupBoxAnalysisLevel.TabStop = false;
			this.groupBoxAnalysisLevel.Text = "Analysis Level";
			// 
			// tableLayoutPanelAnalysisLevel
			// 
			this.tableLayoutPanelAnalysisLevel.ColumnCount = 3;
			this.tableLayoutPanelAnalysisLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanelAnalysisLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanelAnalysisLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.labelPerApplication, 2, 0);
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.labelPerSourceIPAddress, 1, 0);
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.labelPacketByPacket, 0, 0);
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.tableLayoutPanelPacketByPacket, 0, 1);
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.tableLayoutPanelIntraFlow, 1, 1);
			this.tableLayoutPanelAnalysisLevel.Controls.Add(this.tableLayoutPanelInterFlow, 2, 1);
			this.tableLayoutPanelAnalysisLevel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelAnalysisLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tableLayoutPanelAnalysisLevel.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanelAnalysisLevel.Name = "tableLayoutPanelAnalysisLevel";
			this.tableLayoutPanelAnalysisLevel.RowCount = 2;
			this.tableLayoutPanelAnalysisLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.69231F));
			this.tableLayoutPanelAnalysisLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.30769F));
			this.tableLayoutPanelAnalysisLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelAnalysisLevel.Size = new System.Drawing.Size(668, 68);
			this.tableLayoutPanelAnalysisLevel.TabIndex = 20;
			// 
			// labelPerApplication
			// 
			this.labelPerApplication.AutoSize = true;
			this.labelPerApplication.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPerApplication.Location = new System.Drawing.Point(447, 0);
			this.labelPerApplication.Name = "labelPerApplication";
			this.labelPerApplication.Size = new System.Drawing.Size(218, 39);
			this.labelPerApplication.TabIndex = 3;
			this.labelPerApplication.Text = "The analysis window goes through flows within a traffic subset.";
			this.labelPerApplication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPerSourceIPAddress
			// 
			this.labelPerSourceIPAddress.AutoSize = true;
			this.labelPerSourceIPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPerSourceIPAddress.Location = new System.Drawing.Point(225, 0);
			this.labelPerSourceIPAddress.Name = "labelPerSourceIPAddress";
			this.labelPerSourceIPAddress.Size = new System.Drawing.Size(216, 39);
			this.labelPerSourceIPAddress.TabIndex = 2;
			this.labelPerSourceIPAddress.Text = "The analysis window goes through packets of only one flow within a traffic subset" +
				".";
			this.labelPerSourceIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPacketByPacket
			// 
			this.labelPacketByPacket.AutoSize = true;
			this.labelPacketByPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPacketByPacket.Location = new System.Drawing.Point(3, 0);
			this.labelPacketByPacket.Name = "labelPacketByPacket";
			this.labelPacketByPacket.Size = new System.Drawing.Size(216, 39);
			this.labelPacketByPacket.TabIndex = 4;
			this.labelPacketByPacket.Text = "The analysis window goes through all packets within a traffic subset.";
			this.labelPacketByPacket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanelPacketByPacket
			// 
			this.tableLayoutPanelPacketByPacket.ColumnCount = 2;
			this.tableLayoutPanelPacketByPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanelPacketByPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanelPacketByPacket.Controls.Add(this.radioButtonPacketByPacket, 0, 0);
			this.tableLayoutPanelPacketByPacket.Controls.Add(this.buttonPacketByPacketIPAddressRange, 1, 0);
			this.tableLayoutPanelPacketByPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelPacketByPacket.Location = new System.Drawing.Point(3, 42);
			this.tableLayoutPanelPacketByPacket.Name = "tableLayoutPanelPacketByPacket";
			this.tableLayoutPanelPacketByPacket.RowCount = 1;
			this.tableLayoutPanelPacketByPacket.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelPacketByPacket.Size = new System.Drawing.Size(216, 23);
			this.tableLayoutPanelPacketByPacket.TabIndex = 5;
			// 
			// radioButtonPacketByPacket
			// 
			this.radioButtonPacketByPacket.AutoSize = true;
			this.radioButtonPacketByPacket.Checked = true;
			this.radioButtonPacketByPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radioButtonPacketByPacket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonPacketByPacket.Location = new System.Drawing.Point(20, 3);
			this.radioButtonPacketByPacket.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.radioButtonPacketByPacket.Name = "radioButtonPacketByPacket";
			this.radioButtonPacketByPacket.Size = new System.Drawing.Size(149, 17);
			this.radioButtonPacketByPacket.TabIndex = 0;
			this.radioButtonPacketByPacket.TabStop = true;
			this.radioButtonPacketByPacket.Text = "&Packet-By-Packet";
			this.radioButtonPacketByPacket.UseVisualStyleBackColor = true;
			this.radioButtonPacketByPacket.Click += new System.EventHandler(this.radioButtonPacketByPacket_Click);
			// 
			// buttonPacketByPacketIPAddressRange
			// 
			this.buttonPacketByPacketIPAddressRange.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonPacketByPacketIPAddressRange.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.buttonPacketByPacketIPAddressRange.FlatAppearance.BorderSize = 2;
			this.buttonPacketByPacketIPAddressRange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPacketByPacketIPAddressRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonPacketByPacketIPAddressRange.Location = new System.Drawing.Point(175, 3);
			this.buttonPacketByPacketIPAddressRange.Name = "buttonPacketByPacketIPAddressRange";
			this.buttonPacketByPacketIPAddressRange.Size = new System.Drawing.Size(38, 17);
			this.buttonPacketByPacketIPAddressRange.TabIndex = 6;
			this.buttonPacketByPacketIPAddressRange.Text = "AR";
			this.buttonPacketByPacketIPAddressRange.UseVisualStyleBackColor = true;
			this.buttonPacketByPacketIPAddressRange.Click += new System.EventHandler(this.buttonPacketByPacketIPAddressRange_Click);
			// 
			// tableLayoutPanelIntraFlow
			// 
			this.tableLayoutPanelIntraFlow.ColumnCount = 3;
			this.tableLayoutPanelIntraFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanelIntraFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanelIntraFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanelIntraFlow.Controls.Add(this.buttonIntraFlowIPAddressRange, 1, 0);
			this.tableLayoutPanelIntraFlow.Controls.Add(this.radioButtonIntraFlow, 0, 0);
			this.tableLayoutPanelIntraFlow.Controls.Add(this.buttonIntraFlowFlowSettings, 1, 0);
			this.tableLayoutPanelIntraFlow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelIntraFlow.Location = new System.Drawing.Point(225, 42);
			this.tableLayoutPanelIntraFlow.Name = "tableLayoutPanelIntraFlow";
			this.tableLayoutPanelIntraFlow.RowCount = 1;
			this.tableLayoutPanelIntraFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelIntraFlow.Size = new System.Drawing.Size(216, 23);
			this.tableLayoutPanelIntraFlow.TabIndex = 6;
			// 
			// buttonIntraFlowIPAddressRange
			// 
			this.buttonIntraFlowIPAddressRange.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.buttonIntraFlowIPAddressRange.FlatAppearance.BorderSize = 2;
			this.buttonIntraFlowIPAddressRange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonIntraFlowIPAddressRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonIntraFlowIPAddressRange.Location = new System.Drawing.Point(175, 3);
			this.buttonIntraFlowIPAddressRange.Name = "buttonIntraFlowIPAddressRange";
			this.buttonIntraFlowIPAddressRange.Size = new System.Drawing.Size(38, 17);
			this.buttonIntraFlowIPAddressRange.TabIndex = 7;
			this.buttonIntraFlowIPAddressRange.Text = "AR";
			this.buttonIntraFlowIPAddressRange.UseVisualStyleBackColor = true;
			this.buttonIntraFlowIPAddressRange.Click += new System.EventHandler(this.buttonIntraFlowIPAddressRange_Click);
			// 
			// radioButtonIntraFlow
			// 
			this.radioButtonIntraFlow.AutoSize = true;
			this.radioButtonIntraFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonIntraFlow.Location = new System.Drawing.Point(20, 3);
			this.radioButtonIntraFlow.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.radioButtonIntraFlow.Name = "radioButtonIntraFlow";
			this.radioButtonIntraFlow.Size = new System.Drawing.Size(81, 17);
			this.radioButtonIntraFlow.TabIndex = 0;
			this.radioButtonIntraFlow.Text = "Intr&a-Flow";
			this.radioButtonIntraFlow.UseVisualStyleBackColor = true;
			this.radioButtonIntraFlow.Click += new System.EventHandler(this.radioButtonIntraFlow_Click);
			// 
			// buttonIntraFlowFlowSettings
			// 
			this.buttonIntraFlowFlowSettings.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.buttonIntraFlowFlowSettings.FlatAppearance.BorderSize = 2;
			this.buttonIntraFlowFlowSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonIntraFlowFlowSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonIntraFlowFlowSettings.Location = new System.Drawing.Point(132, 3);
			this.buttonIntraFlowFlowSettings.Name = "buttonIntraFlowFlowSettings";
			this.buttonIntraFlowFlowSettings.Size = new System.Drawing.Size(37, 17);
			this.buttonIntraFlowFlowSettings.TabIndex = 8;
			this.buttonIntraFlowFlowSettings.Text = "FS";
			this.buttonIntraFlowFlowSettings.UseVisualStyleBackColor = true;
			this.buttonIntraFlowFlowSettings.Click += new System.EventHandler(this.buttonIntraFlowFlowSettings_Click);
			// 
			// tableLayoutPanelInterFlow
			// 
			this.tableLayoutPanelInterFlow.ColumnCount = 2;
			this.tableLayoutPanelInterFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanelInterFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanelInterFlow.Controls.Add(this.buttonInterFlowIPAddressRange, 1, 0);
			this.tableLayoutPanelInterFlow.Controls.Add(this.radioButtonInterFlow, 0, 0);
			this.tableLayoutPanelInterFlow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelInterFlow.Location = new System.Drawing.Point(447, 42);
			this.tableLayoutPanelInterFlow.Name = "tableLayoutPanelInterFlow";
			this.tableLayoutPanelInterFlow.RowCount = 1;
			this.tableLayoutPanelInterFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelInterFlow.Size = new System.Drawing.Size(218, 23);
			this.tableLayoutPanelInterFlow.TabIndex = 7;
			// 
			// buttonInterFlowIPAddressRange
			// 
			this.buttonInterFlowIPAddressRange.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.buttonInterFlowIPAddressRange.FlatAppearance.BorderSize = 2;
			this.buttonInterFlowIPAddressRange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonInterFlowIPAddressRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonInterFlowIPAddressRange.Location = new System.Drawing.Point(177, 3);
			this.buttonInterFlowIPAddressRange.Name = "buttonInterFlowIPAddressRange";
			this.buttonInterFlowIPAddressRange.Size = new System.Drawing.Size(38, 17);
			this.buttonInterFlowIPAddressRange.TabIndex = 8;
			this.buttonInterFlowIPAddressRange.Text = "AR";
			this.buttonInterFlowIPAddressRange.UseVisualStyleBackColor = true;
			this.buttonInterFlowIPAddressRange.Click += new System.EventHandler(this.buttonInterFlowIPAddressRange_Click);
			// 
			// radioButtonInterFlow
			// 
			this.radioButtonInterFlow.AutoSize = true;
			this.radioButtonInterFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonInterFlow.Location = new System.Drawing.Point(20, 3);
			this.radioButtonInterFlow.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.radioButtonInterFlow.Name = "radioButtonInterFlow";
			this.radioButtonInterFlow.Size = new System.Drawing.Size(81, 17);
			this.radioButtonInterFlow.TabIndex = 1;
			this.radioButtonInterFlow.Text = "Int&er-Flow";
			this.radioButtonInterFlow.UseVisualStyleBackColor = true;
			this.radioButtonInterFlow.Click += new System.EventHandler(this.radioButtonInterFlow_Click);
			// 
			// groupBoxTransportProtocol
			// 
			this.groupBoxTransportProtocol.Controls.Add(this.tableLayoutPanelTransportProtocol);
			this.groupBoxTransportProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxTransportProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxTransportProtocol.Location = new System.Drawing.Point(3, 219);
			this.groupBoxTransportProtocol.Name = "groupBoxTransportProtocol";
			this.groupBoxTransportProtocol.Size = new System.Drawing.Size(674, 62);
			this.groupBoxTransportProtocol.TabIndex = 23;
			this.groupBoxTransportProtocol.TabStop = false;
			this.groupBoxTransportProtocol.Text = "Transport Protocol";
			// 
			// tableLayoutPanelTransportProtocol
			// 
			this.tableLayoutPanelTransportProtocol.ColumnCount = 1;
			this.tableLayoutPanelTransportProtocol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelTransportProtocol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelTransportProtocol.Controls.Add(this.labelTransportProtocol, 0, 0);
			this.tableLayoutPanelTransportProtocol.Controls.Add(this.checkBoxTransportProtocol, 0, 1);
			this.tableLayoutPanelTransportProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelTransportProtocol.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanelTransportProtocol.Name = "tableLayoutPanelTransportProtocol";
			this.tableLayoutPanelTransportProtocol.RowCount = 2;
			this.tableLayoutPanelTransportProtocol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.8983F));
			this.tableLayoutPanelTransportProtocol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.10169F));
			this.tableLayoutPanelTransportProtocol.Size = new System.Drawing.Size(668, 43);
			this.tableLayoutPanelTransportProtocol.TabIndex = 0;
			// 
			// labelTransportProtocol
			// 
			this.labelTransportProtocol.AutoSize = true;
			this.labelTransportProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTransportProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTransportProtocol.Location = new System.Drawing.Point(3, 0);
			this.labelTransportProtocol.Name = "labelTransportProtocol";
			this.labelTransportProtocol.Size = new System.Drawing.Size(662, 14);
			this.labelTransportProtocol.TabIndex = 0;
			this.labelTransportProtocol.Text = "Distinguish flows by their transport protocol (Transmission Control Protocol (TCP" +
				") and User Datagram Protocol (UDP))?";
			this.labelTransportProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBoxTransportProtocol
			// 
			this.checkBoxTransportProtocol.AutoSize = true;
			this.checkBoxTransportProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBoxTransportProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBoxTransportProtocol.Location = new System.Drawing.Point(20, 17);
			this.checkBoxTransportProtocol.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.checkBoxTransportProtocol.Name = "checkBoxTransportProtocol";
			this.checkBoxTransportProtocol.Size = new System.Drawing.Size(645, 23);
			this.checkBoxTransportProtocol.TabIndex = 1;
			this.checkBoxTransportProtocol.Text = "&Yes";
			this.checkBoxTransportProtocol.UseVisualStyleBackColor = true;
			this.checkBoxTransportProtocol.CheckedChanged += new System.EventHandler(this.checkBoxTransportProtocol_CheckedChanged);
			// 
			// frmFlowSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 401);
			this.Controls.Add(this.tableLayoutPanelMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(526, 297);
			this.Name = "frmFlowSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Flow Settings";
			this.groupBoxTimeout.ResumeLayout(false);
			this.tableLayoutPanelTimeout.ResumeLayout(false);
			this.tableLayoutPanelTimeout.PerformLayout();
			this.tableLayoutPanelTimeout2.ResumeLayout(false);
			this.tableLayoutPanelTimeout2.PerformLayout();
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.tableLayoutPanelMain.PerformLayout();
			this.groupBoxDirection.ResumeLayout(false);
			this.tableLayoutPanelDirection.ResumeLayout(false);
			this.tableLayoutPanelDirection.PerformLayout();
			this.panelButtons.ResumeLayout(false);
			this.groupBoxAnalysisLevel.ResumeLayout(false);
			this.tableLayoutPanelAnalysisLevel.ResumeLayout(false);
			this.tableLayoutPanelAnalysisLevel.PerformLayout();
			this.tableLayoutPanelPacketByPacket.ResumeLayout(false);
			this.tableLayoutPanelPacketByPacket.PerformLayout();
			this.tableLayoutPanelIntraFlow.ResumeLayout(false);
			this.tableLayoutPanelIntraFlow.PerformLayout();
			this.tableLayoutPanelInterFlow.ResumeLayout(false);
			this.tableLayoutPanelInterFlow.PerformLayout();
			this.groupBoxTransportProtocol.ResumeLayout(false);
			this.tableLayoutPanelTransportProtocol.ResumeLayout(false);
			this.tableLayoutPanelTransportProtocol.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		public System.Windows.Forms.RadioButton radioButtonIntraFlow;
		public System.Windows.Forms.RadioButton radioButtonInterFlow;
		public System.Windows.Forms.RadioButton radioButtonPacketByPacket;
		public System.Windows.Forms.CheckBox checkBoxTransportProtocol;
		public System.Windows.Forms.TextBox textBoxTimeout;
		public System.Windows.Forms.RadioButton radioButtonBidirectional;
		public System.Windows.Forms.RadioButton radioButtonUnidirectional;
		public System.Windows.Forms.GroupBox groupBoxTimeout;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelTimeout;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelTimeout2;
		public System.Windows.Forms.Label labelSeconds;
		public System.Windows.Forms.Label labelTimeout;
		public System.Windows.Forms.Label labelTimeoutMain;
		public System.Windows.Forms.Label labelMain;
		public System.Windows.Forms.Label labelUnidirectional;
		public System.Windows.Forms.Panel panelButtons;
		public System.Windows.Forms.GroupBox groupBoxDirection;
		public System.Windows.Forms.Button buttonOk;
		public System.Windows.Forms.Button buttonCancel;
		public System.Windows.Forms.GroupBox groupBoxTransportProtocol;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelTransportProtocol;
		public System.Windows.Forms.Label labelTransportProtocol;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelDirection;
		public System.Windows.Forms.Label labelBidirectional;
		public System.Windows.Forms.GroupBox groupBoxAnalysisLevel;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelAnalysisLevel;
		public System.Windows.Forms.Label labelPerApplication;
		public System.Windows.Forms.Label labelPerSourceIPAddress;
		public System.Windows.Forms.Label labelPacketByPacket;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPacketByPacket;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelIntraFlow;
		public System.Windows.Forms.Button buttonPacketByPacketIPAddressRange;
		public System.Windows.Forms.Button buttonIntraFlowIPAddressRange;
		public System.Windows.Forms.Button buttonIntraFlowFlowSettings;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInterFlow;
		public System.Windows.Forms.Button buttonInterFlowIPAddressRange;
	}
}