using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOdyssey
{
	abstract class clsMessages
	{
		public static bool prpVerbose {
			get { return Program.prpSettings.Verbose; }
		}

		/// <summary>
		/// Prints the argument list in Console.Error.
		/// </summary>        
		public static void PrintHelp()
		{
			Console.Error.WriteLine(@"I Usage: {0} <arguments>", System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName);
			Console.Error.WriteLine(@"I Arguments:");
			Console.Error.WriteLine(@"I -h                          Print this help screen");
			Console.Error.WriteLine(@"I -v                          Use verbose output");
			Console.Error.WriteLine(@"I -asc                        Automatically start the capture if all parameters are valid");
			Console.Error.WriteLine(@"I -nsc                        Do not show compile window");
			Console.Error.WriteLine(@"I -hmi milliseconds           Interval in milliseconds between health reports");
			Console.Error.WriteLine(@"I -d deviceNumber             Use the selected device");
			Console.Error.WriteLine(@"I -f=""fileName""               Use the selected capture file");
			Console.Error.WriteLine(@"I -dump                       Dump the capture to a file");
			Console.Error.WriteLine(@"I -ld                         List all available devices");
			Console.Error.WriteLine(@"I -aws packets                Analysis window size");
			Console.Error.WriteLine(@"I -awt milliseconds           Analysis window time");
			Console.Error.WriteLine(@"I -bctu milliseconds          Bit Count per Time Unit");
			Console.Error.WriteLine(@"I -rtp                        Raise current process priority to Real-Time");
			Console.Error.WriteLine(@"I -c count                    Number of packets to capture (default: infinite)");
			Console.Error.WriteLine(@"I -t seconds                  Capture duration in seconds (default: infinite)");
			Console.Error.WriteLine(@"I -filter=""tcpdumpfilter""     tcpdump filter of the capture (default: no filter)");
			Console.Error.WriteLine(@"I -modulesFolder=""path""       Folder to find *.cs and *.vb files to compile and use as modules (default: .\Modules)");
			Console.Error.WriteLine(@"I -reportsFolder=""path""       Root folder where to place analysis results (default: .\Reports)");
			
			Console.Error.WriteLine(@"I -createFolderPerFlow          Creates a report folder per flow (default: false)");
			Console.Error.WriteLine(@"I -analysisSettings=""value""   Analysis settings (default: at)");
			Console.Error.WriteLine(@"I -analysisLevel=""value""      Analysis level (default: pbp)");
			Console.Error.WriteLine(@"I -flowTimeout=""value""        Flow timeout value (default: 64)");
			Console.Error.WriteLine(@"I -distinguishTransportProtocol Distinguishes flows by their transport protocol - TCP or UDP (default: false)");
			Console.Error.WriteLine(@"I -flowDirection=""value""      Flow direction (default: unid)");
			Console.Error.WriteLine(@"I -srcIPAddress=""value""       Source IP address of a flow (default: 0.0.0.0)");
			Console.Error.WriteLine(@"I -dstIPAddress=""value""       Destination IP address of a flow (default: 0.0.0.0)");
			Console.Error.WriteLine(@"I -lowerIPAddress=""value""     Lower IP address of the IP range (default: 0.0.0.0)");
			Console.Error.WriteLine(@"I -upperIPAddress=""value""     Upper IP address of the IP range (default: 255.255.255.255)");
			Console.Error.WriteLine(@"I -srcPort=""value""            Source port of a flow (default: 0)");
			Console.Error.WriteLine(@"I -dstPort=""value""            Destination port of a flow (default: 0)");
			Console.Error.WriteLine(@"I -transportProtocol=""value""  Transport protocol of a flow - TCP or UDP (default: tcp)");
		}

		/// <summary>
		/// Prints the chosen settings in Console.Error if verbose mode is activated.
		/// </summary>
		public static void PrintSettings() 
		{
			if (!prpVerbose) return;
			Console.Error.WriteLine("I Verbose mode activated");
			
			if (Program.prpSettings.CaptureDevice is SharpPcap.OfflinePcapDevice)
				Console.Error.WriteLine("I Offline device selected: " + ((SharpPcap.OfflinePcapDevice)Program.prpSettings.CaptureDevice).FileName);
			else
				Console.Error.WriteLine("I Network device selected: " + Program.prpSettings.CaptureDevice.Description);

			if (Program.prpSettings.TcpDumpFilter.Length <= 0)
				Console.Error.WriteLine("I No tcpdump filter defined");
			else
				Console.Error.WriteLine("I tcpdump filter: " + Program.prpSettings.TcpDumpFilter);

			if (!Program.prpSettings.DumpCapture)
				Console.Error.WriteLine("I Not dumping the capture");
			else
				Console.Error.WriteLine("I Dumping the capture to a file");

			if (Program.prpSettings.Packets_StatisticsToCapture <= 0)
				Console.Error.WriteLine("I No packet capture limit defined");
			else
				Console.Error.WriteLine("I Packets to capture: " + Program.prpSettings.Packets_StatisticsToCapture);

			if (Program.prpSettings.SecondsToCapture <= 0)
				Console.Error.WriteLine("I No capture time limit defined");
			else
				Console.Error.WriteLine("I Capture duration: {0} seconds (will end at {1})", Program.prpSettings.SecondsToCapture, DateTime.Now.AddSeconds(Program.prpSettings.SecondsToCapture).ToString());

			if (!Program.prpSettings.IsStopScheduled)
				Console.Error.WriteLine("I No capture limit parameters defined. Press Enter key to stop the capture");

			Console.Error.WriteLine("I Modules folder: " + Program.prpSettings.ModulesFolder);
			Console.Error.WriteLine("I Reports folder: " + Program.prpSettings.ReportsFolder);

			if (Program.prpSettings.AnalysisWindowSize > 0)
				Console.Error.WriteLine("I Analysis Window Size: {0} packets",Program.prpSettings.AnalysisWindowSize);
			if (Program.prpSettings.AnalysisWindowTime > 0)
				Console.Error.WriteLine("I Analysis Window Time: {0} seconds", Program.prpSettings.AnalysisWindowTime);

			Console.Error.WriteLine("I Capture Settings: {0}", Program.prpSettings.AnalysisSettings.ToString());
			if (Program.prpSettings.AnalysisSettings != AnalysisSettings.BitCountPerTimeUnit)
			{
				Console.Error.WriteLine("I Flow Timeout: {0}", Program.prpSettings.FlowTimeout);
				Console.Error.WriteLine("I Analysis Level: {0}", Program.prpSettings.AnalysisLevel.ToString());
				Console.Error.WriteLine("I Distinguish by Transport Protocol: {0}", Program.prpSettings.DistinguishFlowsByTransportProtocol);
				Console.Error.WriteLine("I Flow Direction: {0}", Program.prpSettings.FlowDirection);				
			}
		}

		/// <summary>
		/// Prints errors in Console.Error given by the input string.
		/// </summary>
		/// <param name="inError">The input string to be printed.</param>
		public static void PrintError(string inError) 
		{
			Console.Error.WriteLine("E Error: {0}", inError);
		}

		/// <summary>
		/// Prints a list of available devices in Console.Error.
		/// </summary>        
		public static void PrintDevices() 
		{
			SharpPcap.LivePcapDeviceList _devices = Program.prpSettings.prpDevices;
			try
			{
				if (_devices.Count < 1)
				{
					Console.Error.WriteLine("E The are no devices available!");
				}
				else
				{
					int d = 0;
					Console.Error.WriteLine("I The following devices are available:");
					foreach (SharpPcap.PcapDevice device in _devices)
						Console.Error.WriteLine("D  {0}) {1}", ++d, device.Description);
				}                
			}
			catch (Exception ex)
			{
				clsMessages.PrintError(ex.Message);
			}
		}

		/// <summary>
		/// Prints the following error message in Console.Error: "Invalid device selection arguments".
		/// </summary>
		public static void PrintInvalidDeviceArguments() 
		{
			PrintError("Invalid device selection arguments.");
		}

		/// <summary>
		/// Prints an input message from the module compiler if verbose mode is activated.
		/// </summary>
		/// <param name="inMessage">The input message from the module compiler.</param>
		public static void PrintCompilerMessage(string inMessage) 
		{
			if (!prpVerbose) return;
			Console.WriteLine("C {0}", inMessage);
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Raising current process priority to High".
		/// </summary>
		public static void PrintRaisingPriority() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Raising current process priority to High");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Stopping capturer thread".
		/// </summary>
		public static void PrintCapturerThreadStopping() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Stopping capturer thread");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Capturer thread stopped".
		/// </summary>
		public static void PrintCapturerThreadStopped() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Capturer thread stopped");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Stopping analyzer thread".
		/// </summary>
		public static void PrintAnalyzerThreadStopping() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Stopping analyzer thread");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Stopping health monitor thread".
		/// </summary>
		public static void PrintHealthThreadStopping() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Stopping health monitor thread");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Analyzer thread stopped".
		/// </summary>
		public static void PrintAnalyzerThreadStopped() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Analyzer thread stopped");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Health monitor thread stopped".
		/// </summary>
		public static void PrintHealthThreadStopped() 
		{
			if (!prpVerbose) return;
			Console.WriteLine("I Health monitor thread stopped");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Press ENTER to stop capture".
		/// </summary>
		public static void PrintPressKeyToStop() 
		{
			if (!prpVerbose) return;
				Console.Error.WriteLine("I Press ENTER to stop capture");            
		}

		/// <summary>
		/// Prints the following message: Environment.NewLine + "I Stopping conditions reached".
		/// </summary>
		public static void PrintAllDone() 
		{
			Console.WriteLine(Environment.NewLine + "I Stopping conditions reached");
		}

		/// <summary>
		/// Prints the following message if verbose mode is activated: "I Application terminated".
		/// </summary>
		public static void PrintEnd() 
		{
			if (!prpVerbose) return;
			Console.Error.WriteLine("I Application terminated");            
		}

		/// <summary>
		/// Shows a MessageBox with a message given by the input string.
		/// </summary>
		/// <param name="inMessage">The input string to be shown in the MessageBox.</param>
		public static void ShowMessageBox(string inMessage) {
			System.Windows.Forms.MessageBox.Show(inMessage, "NetOdyssey", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
			Console.Error.WriteLine("E Error: {0}", inMessage);
		}
	}
}