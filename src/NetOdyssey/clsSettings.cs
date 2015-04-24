using System;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NetOdyssey
{
	class clsSettings
	{
		// Create input arguments trough the console

		clsArguments _arguments;
		[Browsable(false)]
		public clsArguments prpArguments
		{
			get { return _arguments; }
		}

		static public EventWaitHandle _wh = new AutoResetEvent(false);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inArguments"></param>
		public clsSettings(clsArguments inArguments)
		{
			_arguments = inArguments;

			if (prpArguments["v"] != null)
				Verbose = true;

			if (prpArguments["h"] != null)
				PrintHelp = true;

			if (prpArguments["ld"] != null)
				ListDevices = true;

			if (prpArguments["asc"] != null)
				AutoStartCapture = true;

			if (prpArguments["nsc"] != null)
				ShowCompileWindow = false;
			
			if (prpArguments["rtp"] != null)
				RealTimePriority = true;

			if (prpArguments["dump"] != null)
				DumpCapture = true;

			if (prpArguments["filter"] != null)
				TcpDumpFilter = prpArguments["filter"];

			if (prpArguments["modulesFolder"] != null)
				ModulesFolder = prpArguments["modulesFolder"];

			if (prpArguments["reportsFolder"] != null)
				ReportsFolder = prpArguments["reportsFolder"];

			if (prpArguments["createFolderPerFlow"] != null)
				CreateAFolderPerFlow = true;

			#region Device selection
			try
			{
				if (prpArguments["d"] != null && prpArguments["f"] != null)
					clsMessages.PrintInvalidDeviceArguments();
				else if (prpArguments["d"] != null)
					CaptureDevice = prpDevices[int.Parse(prpArguments["d"]) - 1];
				else if (prpArguments["f"] != null)
					CaptureDevice = new SharpPcap.OfflinePcapDevice(prpArguments["f"]);
				else
				{
					CaptureDevice = prpDevices[0];
					AutoStartCapture = false;
				}
			}
			catch (Exception ex)
			{
				clsMessages.PrintError("Invalid device selected: " + ex.Message);
				AutoStartCapture = false;
			}
			#endregion

			#region Packet count
			try
			{
				if (prpArguments["c"] != null)
					Packets_StatisticsToCapture = uint.Parse(prpArguments["c"]);
			}
			catch
			{
				clsMessages.PrintError("Invalid packet count.");
				AutoStartCapture = false;
			}
			#endregion

			#region Capture time
			try
			{
				if (prpArguments["t"] != null)
				{
					SecondsToCapture = uint.Parse(prpArguments["t"]);
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid capture time.");
				AutoStartCapture = false;
			}
			#endregion

			#region Analyzis Window Size
			try
			{
				if (prpArguments["aws"] != null)
				{
					AnalysisWindowSize = uint.Parse(prpArguments["aws"]);
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid window size.");
				AutoStartCapture = false;
			}
			#endregion

			#region Analysis Window Time
			try
			{
				if (prpArguments["awt"] != null)
				{
					AnalysisWindowTime = uint.Parse(prpArguments["awt"]);
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid analysis window time.");
				AutoStartCapture = false;
			}
			#endregion

			#region Bit Counter Time Unit
			try {
				if (prpArguments["bctu"] != null) {
					BitCountPerTimeUnit = uint.Parse(prpArguments["bctu"]);
				}
			}
			catch {
				clsMessages.PrintError("Invalid bit count per time unit.");
				AutoStartCapture = false;
			}
			#endregion

			#region Health Monitor Interval
			try
			{
				if (prpArguments["hmi"] != null)
					HealthMonitorInterval = uint.Parse(prpArguments["hmi"]);
			}
			catch
			{
				clsMessages.PrintError("Invalid health monitor interval.");
				HealthMonitorInterval = 1000;
			}
			#endregion


			#region Analysis Settings
			if (prpArguments["analysisSettings"] != null)
			{
				if (prpArguments["analysisSettings"].ToLower().Equals("at"))
				{
					AnalysisSettings = NetOdyssey.AnalysisSettings.AllTraffic;
				}
				if (prpArguments["analysisSettings"].ToLower().Equals("psip"))
				{
					AnalysisSettings = NetOdyssey.AnalysisSettings.PerSourceIP;
				}
				if (prpArguments["analysisSettings"].ToLower().Equals("pa"))
				{
					AnalysisSettings = NetOdyssey.AnalysisSettings.PerApplication;
				}
				if (prpArguments["analysisSettings"].ToLower().Equals("bctu"))
				{
					AnalysisSettings = NetOdyssey.AnalysisSettings.BitCountPerTimeUnit;
				}
				else
				{
					clsMessages.PrintError("Invalid analysis settings value.");
				}
			}
			#endregion
			
			#region Analysis Level
			if (prpArguments["analysisLevel"] != null)
			{
				if (prpArguments["analysisLevel"].ToLower().Equals("pbp"))
				{
					AnalysisLevel = NetOdyssey.AnalysisLevel.PacketByPacket;
				}
				if (prpArguments["analysisLevel"].ToLower().Equals("intraf"))
				{
					AnalysisLevel = NetOdyssey.AnalysisLevel.IntraFlow;
				}
				if (prpArguments["analysisLevel"].ToLower().Equals("interf"))
				{
					AnalysisLevel = NetOdyssey.AnalysisLevel.InterFlow;
				}
				else
				{
					clsMessages.PrintError("Invalid analysis level value.");
				}
			}
			#endregion

			#region Flow Timeout
			try
			{
				if (prpArguments["flowTimeout"] != null)
				{
					int flowTimeout = Int32.Parse(prpArguments["flowTimeout"]);
					if (flowTimeout <= 0)
					{
						throw new Exception();
					}
					else
					{
						FlowTimeout = prpArguments["flowTimeout"];
					}
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid flow timeout value.");
			}
			#endregion

			if (prpArguments["distinguishTransportProtocol"] != null)
			{
				DistinguishFlowsByTransportProtocol = true;
			}

			#region Flow Direction
			if (prpArguments["flowDirection"] != null)
			{
				if (prpArguments["flowDirection"].Equals("unid"))
				{
					FlowDirection = FlowDirection.Unidirectional;
				}
				if (prpArguments["flowDirection"].Equals("bid"))
				{
					FlowDirection = FlowDirection.Bidirectional;
				}
				else
				{
					clsMessages.PrintError("Invalid flow direction value.");
				}
			}
			#endregion

			#region Source IP Address
			try
			{
				if (prpArguments["srcIPAddress"] != null)
				{
					IPAddress ipAddress = IPAddress.Parse(prpArguments["srcIPAddress"]);
					SourceIPAddress = ipAddress;
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid source IP address value.");
			}
			#endregion

			#region Destination IP Address
			try
			{
				if (prpArguments["dstIPAddress"] != null)
				{
					IPAddress ipAddress = IPAddress.Parse(prpArguments["dstIPAddress"]);
					DestinationIPAddress = ipAddress;
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid dstIPAddress IP address value.");
			}
			#endregion

			#region Lower IP Address
			try
			{
				if (prpArguments["lowerIPAddress"] != null)
				{
					IPAddress ipAddress = IPAddress.Parse(prpArguments["lowerIPAddress"]);
					LowerIPAddress = ipAddress;
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid lower IP address value.");
			}
			#endregion

			#region Upper IP Address
			try
			{
				if (prpArguments["upperIPAddress"] != null)
				{
					IPAddress ipAddress = IPAddress.Parse(prpArguments["upperIPAddress"]);
					UpperIPAddress = ipAddress;
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid upper IP address value.");
			}
			#endregion

			#region Source Port
			try
			{
				if (prpArguments["srcPort"] != null)
				{
					ushort port = ushort.Parse(prpArguments["srcPort"]);
					if (port < 0 || port > 65535)
					{
						throw new Exception();
					}
					else
					{
						SourcePort = port;
					}
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid source port value.");
			}
			#endregion

			#region Destination Port
			try
			{
				if (prpArguments["dstPort"] != null)
				{
					ushort port = ushort.Parse(prpArguments["dstPort"]);
					if (port < 0 || port > 65535)
					{
						throw new Exception();
					}
					else
					{
						DestinationPort = port;
					}
				}
			}
			catch
			{
				clsMessages.PrintError("Invalid destination port value.");
			}
			#endregion

			#region Transport Protocol
			if (prpArguments["transportProtocol"] != null)
			{
				if (prpArguments["transportProtocol"].ToLower().Equals("tcp"))
				{
					TransportProtocol = NetOdysseyModule.TransportProtocol.TCP;
				}
				if (prpArguments["transportProtocol"].ToLower().Equals("udp"))
				{
					TransportProtocol = NetOdysseyModule.TransportProtocol.UDP;
				}
				else
				{
					clsMessages.PrintError("Invalid transport protocol value.");
				}
			}
			#endregion
		}

		/// <summary>
		/// Checks for user input settings.
		/// </summary>
		public void checkSettings()
		{
			if (Packets_StatisticsToCapture != 0 || SecondsToCapture != 0)
			{
				if (SecondsToCapture == 0 && Packets_StatisticsToCapture < AnalysisWindowSize)
					throw new Exception("PacketsToCapture is smaller than AnalysisWindowSize");
				if (Packets_StatisticsToCapture == 0 && SecondsToCapture < AnalysisWindowTime)
					throw new Exception("SecondsToCapture is smaller than AnalysisWindowTime");
			}
			if (AnalysisWindowSize == 0 && AnalysisWindowTime == 0)
				throw new Exception("No analysis window parameters defined");
			if (AnalysisWindowSize != 0 && AnalysisWindowTime != 0)
				throw new Exception("Only one analysis parameter may be defined");
			if (AnalysisWindowSize != 0 && BitCountPerTimeUnit != 0)
				throw new Exception("It is not possible to use AnalysisWindowSize and BitCountPerTimeUnit");

			if (BitCountPerTimeUnit != 0 && AnalysisSettings == AnalysisSettings.AllTraffic)
				throw new Exception("It is not possible to use Bit Count Per Time Unit and the All Traffic");
			if (BitCountPerTimeUnit != 0 && AnalysisSettings == AnalysisSettings.PerSourceIP)
				throw new Exception("It is not possible to use Bit Count Per Time Unit and the Per Source IP");
			if (BitCountPerTimeUnit != 0 && AnalysisSettings == AnalysisSettings.PerApplication)
					throw new Exception("It is not possible to use Bit Count Per Time Unit and the Per Application");

			if (CaptureDevice is SharpPcap.OfflinePcapDevice && TcpDumpFilter != "")
				throw new Exception("It is not possible to use a tcpdump filter with an offline device");

			_captureMode = (BitCountPerTimeUnit == 0) ? CaptureMode.Packets : CaptureMode.Statistics;

			if (_captureMode == CaptureMode.Statistics) {
				if (CaptureDevice is SharpPcap.OfflinePcapDevice)
					throw new Exception("It is not possible to calculate BitCountPerTimeUnit with offline capture files");
				if (DumpCapture)
					throw new Exception("It is not possible to dump capture while calculating BitCountPerTimeUnit");
			}
			try
			{
				DirectoryInfo modulesFolder = new DirectoryInfo(ModulesFolder);
				if (!modulesFolder.Exists)
					throw new Exception("Modules folder \"" + ModulesFolder + "\" does not exist");
			}
			catch
			{
				throw new Exception("Invalid modules folder: " + ModulesFolder);
			}
		}

		/// <summary>
		/// Dumps information of the capture to a file.
		/// </summary>
		/// <param name="inCapturedPackets_Statistics">The number of packets or statistics captured.</param>
		/// <param name="inCaptureDuration">The real duration of the capture.</param>
		/// <param name="inCapturedFlows">The number of flows captured.</param>
		public void dumpSettings(uint inCapturedPackets_Statistics, TimeSpan inCaptureDuration, int inCapturedFlows)
		{
			try {
				FileStream _settingsExportFile;
				StreamWriter _settingsExportFileStreamWriter;
				string _message = null;

				_settingsExportFile = File.Create(ReportsFolder + @"\CaptureSettings.txt");
				_settingsExportFileStreamWriter = new StreamWriter(_settingsExportFile);

				_message = @"NetOdyssey: Capture settings
Capture device: " + (CaptureDevice is SharpPcap.OfflinePcapDevice ? "(offline) " + (CaptureDevice as SharpPcap.OfflinePcapDevice).FileName : CaptureDevice.ToString()) + @"
TcpDumpFilter: " + TcpDumpFilter + @"
Packets\Statistics to capture: " + Packets_StatisticsToCapture + @"
Capture Settings: " + AnalysisSettings + @"
Seconds to capture: " + SecondsToCapture + @"
AWS: " + AnalysisWindowSize + @" 
AWT: " + AnalysisWindowTime + @"
BCTU: " + BitCountPerTimeUnit + @"

Packets\Statistics: " + inCapturedPackets_Statistics + @"

";

				if (AnalysisSettings != AnalysisSettings.BitCountPerTimeUnit)
						_message += @"
Flow Settings" + @"
Number of Flows: " + (inCapturedFlows + 1) + @"
Flow Timeout: " + FlowTimeout + @"
Analysis Level: " + AnalysisLevel + @"
Distinguish by Transport Protocol: " + DistinguishFlowsByTransportProtocol + @"
Flow Direction: " + FlowDirection + @"

";

				_message += "Capture duration: " + inCaptureDuration.ToString();

				_settingsExportFileStreamWriter.Write(_message);
				_settingsExportFileStreamWriter.Close();
				_settingsExportFile.Close();
			}
			catch (Exception ex) {
				throw new Exception("Exception while exporting capture settings: " + ex.Message);
			}
		}

		[Browsable(false)]
		public bool IsStopScheduled
		{
			get { return (Packets_StatisticsToCapture > 0 || SecondsToCapture > 0); }
		}

		#region General Settings
			#region Verbose
				bool _verbose;
				/// <summary>
				/// Gets or sets the Verbose mode, with detailed informational outputs.
				/// </summary>
				[DescriptionAttribute("Output will contain verbose information messages."),
				 CategoryAttribute("Global Settings"),
				 DefaultValueAttribute(true)]
				public bool Verbose
				{
					get { return _verbose; }
					set 
					{
						_verbose = value;
						if (_verbose)
							Console.WriteLine("Verbose: {0}", _verbose);
					}
				}
			#endregion

			#region PrintHelp
				bool _printHelp;
				/// <summary>
				/// Gets or sets a boolean indicating that Help is to be printed.
				/// </summary>
				[Browsable(false)]
				public bool PrintHelp
				{
					get { return _printHelp; }
					set { _printHelp = value; }
				}
			#endregion

			#region ListDevices
				bool _listDevices;
				/// <summary>
				/// Gets or sets a boolean indicating that devices are to be listed.
				/// </summary>
				[Browsable(false)]
				public bool ListDevices
				{
					get { return _listDevices; }
					set { _listDevices = value; }
				}
			#endregion

			#region Device
				SharpPcap.LivePcapDeviceList _devices = SharpPcap.LivePcapDeviceList.Instance;
				/// <summary>
				/// Gets the LivePcapDeviceList.
				/// </summary>
				[Browsable(false)]
				public SharpPcap.LivePcapDeviceList prpDevices
				{
					get { return _devices; }
				}

				SharpPcap.PcapDevice _device;
				/// <summary>
				/// Gets or sets the selected PcapDevice.
				/// </summary>
				/// <returns></returns>
				[Browsable(false)]
				public SharpPcap.PcapDevice CaptureDevice
				{
					get { return _device; }
					set 
					{ 
						_device = value;
						if (Verbose)
							Console.WriteLine("Capture Device: {0}", _device);
					}
				}
			#endregion

			#region TcpDumpFilter
				string _tcpdumpFilter = "";
				/// <summary>
				/// Gets or sets the current tcpdump filter to use on this capture.
				/// </summary>
				[Browsable(false)]
				public string TcpDumpFilter
				{
					get { return _tcpdumpFilter; }
					set 
					{ 
						_tcpdumpFilter = value;
						if (Verbose)
							Console.WriteLine("tcpdump Filter: {0}", _tcpdumpFilter);
					}
				}
			#endregion

			#region DumpCapture
				bool _dumpCapture = false;
				/// <summary>
				/// Dumps a capture file in the report folder.
				/// </summary>
				[DescriptionAttribute("Will create a capture dump file in the report folder."),
				 CategoryAttribute("Capture Settings"),
				 DefaultValueAttribute(false)]
				public bool DumpCapture
				{
					get { return _dumpCapture; }
					set 
					{ 
						_dumpCapture = value;
						if (Verbose)
							Console.WriteLine("Dump Capture: {0}", _dumpCapture);
					}
				}
				[Browsable(false)]
				public string prpDumpFile
				{
					get { return ReportsFolder + @"\captureDump.cap"; }
				}
			#endregion

			#region ModulesFolder
				string _modulesFolder = Environment.CurrentDirectory + @"\Modules";
					//Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Desktop\\Desktop\\NetOdyssey Tests\\";
				/// <summary>
				/// Folder where to find *.cs and *.vb files to compile and run as modules.
				/// </summary>        
				/// <remarks>Default is running folder \Modules.</remarks>
				[Browsable(false)]
				public string ModulesFolder
				{
					get { return _modulesFolder; }
					set 
					{ 
						_modulesFolder = value;
						if (Verbose)
							Console.WriteLine("Modules Folder: {0}", _modulesFolder);						 
					}
				}
			#endregion

			#region ReportsFolder
				string _reportsFolder = Environment.CurrentDirectory + @"\Reports\" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now); 
					//Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Desktop\\Desktop\\NetOdyssey Tests\\Reports\\";
				/// <summary>
				/// Root folder to place analysis reports.
				/// </summary>
				[Browsable(false)]
				public string ReportsFolder
				{
					get { return _reportsFolder; }
					set 
					{ 
						_reportsFolder = value;
						if (Verbose)
							Console.WriteLine("Reports Folder: {0}", _reportsFolder);
					}
				}

				public void createReportsFolder()
				{
					DirectoryInfo reportsFolder = new DirectoryInfo(ReportsFolder);
					if (!reportsFolder.Exists)
						reportsFolder.Create();
				}
			#endregion

			#region Create a Folder per Flow
				bool _createAFolderPerflow;
				/// <summary>
				/// Gets or sets a bool to create a folder per flow.
				/// </summary>
				[DescriptionAttribute("Will create a folder per flow for the reports."),
				 CategoryAttribute("Global Settings"),
				 DefaultValueAttribute(true)]
				public bool CreateAFolderPerFlow
				{
					get { return _createAFolderPerflow; }
					set
					{
						_createAFolderPerflow = value;
						if (_verbose)
							Console.WriteLine("Create a Folder per Flow: {0}", _createAFolderPerflow);
					}
				}
			#endregion

			#region RealTimePriority
				bool _realTimePriority = false;
				/// <summary>
				/// Automatically raise process priority to real-time.
				/// </summary>        
				/// <remarks>If set to false, process will run with default priority.</remarks>
				[DescriptionAttribute("Raise current process priority to real-time."),
				 CategoryAttribute("Global Settings"),
				 DefaultValueAttribute(true)]
				public bool RealTimePriority
				{
					get { return _realTimePriority; }
					set 
					{ 
						_realTimePriority = value;
						if (Verbose)
							Console.WriteLine("Real Time Priority: {0}", _realTimePriority);						 
					}
				}
			#endregion

			#region HealthMonitorInterval
				uint _healthMonitorInterval = 1000;
				/// <summary>
				/// Health monitor reporting interval.
				/// </summary>        
				/// <remarks>Number of milliseconds the health monitor should report.</remarks>
				[DescriptionAttribute("Number of milliseconds between health reports."),
				 CategoryAttribute("Health monitor"),
				 DefaultValueAttribute(1000)]
				public uint HealthMonitorInterval
				{
					get { return _healthMonitorInterval; }
					set
					{
						if (value > 0)
							_healthMonitorInterval = value;
						else
							throw new Exception("Health Monitor Interval must be greater than zero");
						if (Verbose)
							Console.WriteLine("Health Monitor Interval: {0}", _healthMonitorInterval);
					}
				}
			#endregion

			#region ShowCompile
				bool _showCompile = true;
				/// <summary>
				/// Automatically show compile window when compiling modules.
				/// </summary>        
				/// <remarks>If set to false, compile window will only show when there are compile errors.</remarks>
				[DescriptionAttribute("Will compile window to be shown even if there are no compile errors."),
				 CategoryAttribute("Global Settings"),
				 DefaultValueAttribute(true)]
				public bool ShowCompileWindow
				{
					get { return _showCompile; }
					set 
					{ 
						_showCompile = value;
						if (Verbose)
							Console.WriteLine("Show Compile: {0}", _showCompile);
					}
				}
			#endregion

			#region AutoStartCapture
				bool _autoStartCapture = false;
				/// <summary>
				/// Automatically start capture, if all parameters are valid.
				/// </summary>
				/// <remarks>If this flag is set in command line parameters but there are invalid settings, it will be reset to false.</remarks>
				/*[DescriptionAttribute("Auto start capture (if all parameters are valid, settings will not be displayed)."),
				 CategoryAttribute("Global Settings"),
				 DefaultValueAttribute(false)]*/
				[Browsable(false)]
				public bool AutoStartCapture
				{
					get { return _autoStartCapture; }
					set 
					{ 
						_autoStartCapture = value;
						if (Verbose)
							Console.WriteLine("Auto Start Capture: {0}", _autoStartCapture);
					}
				}
			#endregion

			#region Packets to capture
				uint _packetsToCapture = 0;
				/// <summary>
				/// Number of packets to be captured in the session. 0 means infinite.
				/// </summary>        
				[DescriptionAttribute("Number of packets or statistics to be captured. 0 is infinite."),
				 CategoryAttribute("Capture Settings"),
				 DefaultValueAttribute(0)]
				public uint Packets_StatisticsToCapture
				{
					get { return _packetsToCapture; }
					set 
					{
						_packetsToCapture = value;
						if (Verbose)
							Console.WriteLine("Packets to Capture: {0}", _packetsToCapture);
					}
				}
			#endregion

			#region Time to capture
				uint _secondsToCapture = 0;
				/// <summary>
				/// Duration in seconds of the capture session. 0 is infinite.
				/// </summary>
				[DescriptionAttribute("Number of seconds to capture packets. 0 is infinite"),
				 CategoryAttribute("Capture Settings")]
				public uint SecondsToCapture
				{
					get { return _secondsToCapture; }
					set 
					{ 
						_secondsToCapture = value;
						if (Verbose)
							Console.WriteLine("Seconds To Capture: {0}", _secondsToCapture);
					}
				}
			#endregion

			#region Analysis window size
				uint _aws = 0;
				/// <summary>
				/// Sliding analysis window's size in packets.
				/// </summary>
				[DescriptionAttribute("Size in packets\\statistics of the sliding analysis window."),
				 CategoryAttribute("Analysis Window Settings"),
				 DefaultValueAttribute(0)]
				public uint AnalysisWindowSize
				{
					get { return _aws; }
					set 
					{
						_aws = value;
						if (Verbose)
							Console.WriteLine("AWS: {0}", value);
					}
				}
			#endregion

			#region Analysis window time
				TimeSpan _awt = TimeSpan.Zero;
				/// <summary>
				/// Duration in milliseconds of the jumping analysis window.
				/// </summary>
				[DescriptionAttribute("Time in seconds of the jumping analysis window."),
				 CategoryAttribute("Analysis Window Settings"),
				 DefaultValueAttribute(0)]
				public uint AnalysisWindowTime
				{
					get { return (uint)_awt.TotalSeconds; }
					set
					{
						if (value <= 0)
							_awt = TimeSpan.Zero;
						else
							_awt = TimeSpan.FromSeconds(value);
						if (Verbose)
							Console.WriteLine("AWT: {0}", _awt);
					}
				}
			#endregion

			#region Bit Count per Time Unit
				uint _bctu = 0;
				/// <summary>
				/// Bit Count per Time Unit.
				/// </summary>
				[DescriptionAttribute("Time unit in milliseconds to calculate Bit Count."),
				 CategoryAttribute("Analysis Window Settings"),
				 DefaultValueAttribute(0)]
				public uint BitCountPerTimeUnit
				{
					get { return _bctu; }
					set 
					{ 
						_bctu = value;
						if (Verbose)
							Console.WriteLine("BCTU: {0}", _bctu);
					}
				}
			#endregion
		#endregion

		#region NetOdyssey Capture Mode
		CaptureMode _captureMode;
		/// <summary>
		/// Gets the capture mode.
		/// </summary>
		[Browsable(false)]
		public CaptureMode CaptureMode {
			get { return _captureMode; }
		}
		#endregion

		#region NetOdyssey Analysis Settings
			// All traffic by default
			AnalysisSettings _analysisSettings = AnalysisSettings.AllTraffic;
			/// <summary>
			/// Gets or sets the analysis settings.
			/// </summary>
			[Browsable(false)]
			public AnalysisSettings AnalysisSettings
			{
				get { return _analysisSettings; }
				set 
				{ 
					_analysisSettings = value;
					if (Verbose)
						Console.WriteLine("Analysis Settings: {0}", _analysisSettings);
				}
			}
		#endregion

		#region Flow Settings
			#region Analysis Level
			// Packet By Packet by default
			AnalysisLevel _analysisLevel = AnalysisLevel.PacketByPacket;
			/// <summary>
			/// Gets or sets the analysis level to be used in a capture.
			/// </summary>
			[Browsable(false)]
			public AnalysisLevel AnalysisLevel
			{
				get { return _analysisLevel; }
				set
				{
					_analysisLevel = value;
					if (Verbose)
						Console.WriteLine("Analysis Level: {0}", _analysisLevel);
				}
			}
			#endregion

			#region Flow Timeout
				// 64 seconds by default
				string _flowTimeout = "64";
				/// <summary>
				/// Gets or sets the flow timeout to be used in a flow capture.
				/// </summary>
				[Browsable(false)]
				public string FlowTimeout
				{
					get { return _flowTimeout; }
					set 
					{ 
						_flowTimeout = value;
						if (Verbose)
							Console.WriteLine("Flow Timeout: {0}", _flowTimeout);
					}
				}
			#endregion

			#region Distinguish Flows by Transport Protocol
				// False by default
				bool _distinguishFlowsByTransportProtocol = false;
				/// <summary>
				/// Gets or sets the option to distinguish flows by their transport protocol in a flow capture.
				/// </summary>
				[Browsable(false)]
				public bool DistinguishFlowsByTransportProtocol
				{
					get { return _distinguishFlowsByTransportProtocol; }
					set 
					{ 
						_distinguishFlowsByTransportProtocol = value;
						if (Verbose)
							Console.WriteLine("Distinguish Flows by Transport Protocol: {0}", _distinguishFlowsByTransportProtocol);
					}
				}
			#endregion

			#region Flow Direction
				// Unidirectional by default
				FlowDirection _flowDirection = FlowDirection.Unidirectional;
				/// <summary>
				/// Gets or sets the flow direction in a flow capture.
				/// </summary>
				[Browsable(false)]
				public FlowDirection FlowDirection
				{
					get { return _flowDirection; }
					set 
					{ 
						_flowDirection = value;
						if (Verbose)
							Console.WriteLine("Flow Direction: {0}", _flowDirection);
					}
				}
			#endregion

			#region Source IP Address
				// 0.0.0.0 by default
				IPAddress _srcIPAddress = IPAddress.Parse("0.0.0.0");
				/// <summary>
				/// Gets or sets the source IP Address of a flow for the IntraFlow.
				/// </summary>
				[Browsable(false)]
				public IPAddress SourceIPAddress
				{
					get { return _srcIPAddress; }
					set 
					{ 
						_srcIPAddress = value;
						if (Verbose)
							Console.WriteLine("Source IP Address: {0}", _srcIPAddress.ToString()); 
					}
				}
			#endregion

			#region Destination IP Address
				// 0.0.0.0 by default
				IPAddress _dstIPAddress = IPAddress.Parse("0.0.0.0");
				/// <summary>
				/// Gets or sets the destination IP Address of a flow for the IntraFlow.
				/// </summary>
				[Browsable(false)]
				public IPAddress DestinationIPAddress
				{
					get { return _dstIPAddress; }
					set 
					{ 
						_dstIPAddress = value;
						if (Verbose)
							Console.WriteLine("Destination IP Address: {0}", _dstIPAddress.ToString());
					}
				}
			#endregion

			#region Lower IP Address
				// 0.0.0.0 by default
				IPAddress _lowerIPAddress = IPAddress.Parse("0.0.0.0");
				/// <summary>
				/// Gets or sets the lower IP address of an IP range for the PerSourceIP and PerApplication.
				/// </summary>
				[Browsable(false)]
				public IPAddress LowerIPAddress
				{
					get { return _lowerIPAddress; }
					set
					{
						_lowerIPAddress = value;
						if (Verbose)
							Console.WriteLine("Lower IP Address: {0}", _lowerIPAddress.ToString());
					}
				}
			#endregion

			#region Upper IP Address
				// 255.255.255.255 by default
				IPAddress _upperIPAddress = IPAddress.Parse("255.255.255.255");
				/// <summary>
				/// Gets or sets the upper IP address of an IP range for the PerIpSource and PerApplication.
				/// </summary>
				[Browsable(false)]
				public IPAddress UpperIPAddress
				{
					get { return _upperIPAddress; }
					set
					{
						_upperIPAddress = value;
						if (Verbose)
							Console.WriteLine("Upper IP Address: {0}", _upperIPAddress.ToString());
					}
				}
			#endregion

			#region Source Port
				// 0 by default
				ushort _srcPort = 0;
				/// <summary>
				/// Gets or sets the source port of a flow for the IntraFlow.
				/// </summary>
				[Browsable(false)]
				public ushort SourcePort
				{
					get { return _srcPort; }
					set 
					{ 
						_srcPort = value;
						if (Verbose)
							Console.WriteLine("Source Port: {0}", _srcPort);
					}
				}
			#endregion

			#region Destination Port
				// 0 by default
				ushort _dstPort = 0;
				/// <summary>
				/// Gets or sets the destination port of a flow for the IntraFlow.
				/// </summary>
				[Browsable(false)]
				public ushort DestinationPort
				{
					get { return _dstPort; }
					set 
					{ 
						_dstPort = value;
						if (Verbose)
							Console.WriteLine("Destination Port: {0}", _dstPort);
					}
				}
			#endregion

			#region Transport Protocol
				NetOdysseyModule.TransportProtocol _transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
				/// <summary>
				/// Gets or sets the transport protocol of a flow for the IntraFlow.
				/// </summary>
				[Browsable(false)]
				public NetOdysseyModule.TransportProtocol TransportProtocol
				{
					get { return _transportProtocol; }
					set 
					{ 
						_transportProtocol = value;
						if (Verbose)
							Console.WriteLine("Transport Protocol: {0}", _transportProtocol);
					}
				}
			#endregion
		#endregion
	}

	/// <summary>
	/// The capture mode enumerator.
	/// </summary>
	public enum CaptureMode 
	{ 
		Packets,
		Statistics
	}

	/// <summary>
	/// The analysis settings enumerator.
	/// </summary>
	public enum AnalysisSettings
	{
		BitCountPerTimeUnit,
		AllTraffic,
		PerSourceIP,
		PerApplication
	}

	/// <summary>
	/// The analysis level enumerator of a capture.
	/// </summary>
	public enum AnalysisLevel
	{
		PacketByPacket,
		IntraFlow,
		InterFlow
	}

	/// <summary>
	/// The flow direction enumerator of a flow capture.
	/// </summary>
	public enum FlowDirection
	{
		Unidirectional,
		Bidirectional
	}
}
