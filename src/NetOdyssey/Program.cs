using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetOdyssey {
	class Program {

		public const char Backspace = (char) 8;

		#region Static Variable Members
			static frmModuleCompiler _frmModuleCompiler = new frmModuleCompiler();
			public static frmModuleCompiler prpFrmModuleCompiler
			{
				get { return _frmModuleCompiler; }
			}

			static frmFlowSettings _frmFlowSettings = new frmFlowSettings();
			public static frmFlowSettings prpFrmFlowSettings
			{
				get { return _frmFlowSettings; }
			}
			
			static frmFlowSpecifications _frmFlowSpecifications = new frmFlowSpecifications();
			public static frmFlowSpecifications prpFrmFlowSpecifications
			{
				get { return _frmFlowSpecifications; }
			}

			static frmIPAddressRange _frmIPAddressRange = new frmIPAddressRange();
			public static frmIPAddressRange prpFrmIPAddressRange
			{
				get { return _frmIPAddressRange; }
			}

			static frmAbout _frmAbout = new frmAbout();
			public static frmAbout prpFrmAbout
			{
				get { return _frmAbout; }
			}

			static frmHelp _frmHelp = new frmHelp();
			public static frmHelp prpFrmHelp
			{
				get { return _frmHelp; }
			}

			static clsSettings _settings;
			public static clsSettings prpSettings {
				get { return _settings; }
			}
			
			static clsCapturer _capturer;
			public static clsCapturer prpCapturer {
				get { return _capturer; }
			}

			static clsHealthMonitor _healthMonitor;
			public static clsHealthMonitor prpHealthMonitor
			{
				get { return _healthMonitor; }
			} 

			static Dictionary<int, List<NetOdysseyModule.NetOdysseyModuleBase>> _dicModules = new Dictionary<int, List<NetOdysseyModule.NetOdysseyModuleBase>>();
			public static Dictionary<int, List<NetOdysseyModule.NetOdysseyModuleBase>> prpModulesDictionary
			{
				get { return _dicModules; }
			}

			static List<Type> _modulesTypeList = new List<Type>();
			public static List<Type> prpModulesTypeList
			{
				get { return _modulesTypeList; }
			}

			static List<string> _modulesName = new List<string>();
			public static List<string> prpModulesNameList
			{
				get { return _modulesName; }
			}    
		#endregion

		[STAThread] // Because of the file open dialogs
		static void Main(string[] args) {
			Application.EnableVisualStyles();

			_settings = new clsSettings(new clsArguments(args));
			if (prpSettings.PrintHelp) { clsMessages.PrintHelp(); return; }
			if (prpSettings.ListDevices) { clsMessages.PrintDevices(); return; }

			try {
				prpSettings.checkSettings();
			}
			catch {
				prpSettings.AutoStartCapture = false;
			}

			if (prpSettings.AutoStartCapture == true || new frmSettings().ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				bool compileErrors = clsModules.compileModules(new System.IO.DirectoryInfo(prpSettings.ModulesFolder));
				if (compileErrors || prpSettings.ShowCompileWindow) 
				{
					if (compileErrors)
						prpFrmModuleCompiler.btnStart.Enabled = false;
					if (prpFrmModuleCompiler.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						return;
					prpFrmModuleCompiler.Dispose();
					prpFrmFlowSettings.Dispose();
					prpFrmAbout.Dispose();
					prpFrmHelp.Dispose();
					prpFrmFlowSpecifications.Dispose();
				}

				if (prpSettings.RealTimePriority) 
				{
					clsMessages.PrintRaisingPriority();
					try {
						System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
					}
					catch (Exception ex) {
						clsMessages.PrintError("Could not raise current process priority. Ex: " + ex.Message);
					}
				}

				_healthMonitor = new clsHealthMonitor((int) prpSettings.HealthMonitorInterval);
				_capturer = new clsCapturer(prpSettings.CaptureDevice);

				prpHealthMonitor.addModule(_capturer);
				prpCapturer.Start(prpHealthMonitor);

				Thread _th = new Thread(delegate() { while (!Console.KeyAvailable) { Thread.Sleep(500); } clsSettings._wh.Set(); });
				_th.Start();
				clsMessages.PrintPressKeyToStop();
				clsSettings._wh.WaitOne();
				_th.Abort();

				if (Program.prpSettings.Verbose)
					Console.WriteLine(Environment.NewLine + "I NetOdyssey is terminating, please standby . . ." + Environment.NewLine);
  
				prpCapturer.Stop();

				prpSettings.dumpSettings(prpCapturer.prpCapturedPackets_Statistics, prpCapturer.prpCaptureDuration, prpCapturer.prpCapturedFlows);

				if (Program.prpSettings.Verbose)
					Console.WriteLine(Environment.NewLine + "I NetOdyssey terminated, exiting . . .");
				Console.WriteLine(Environment.NewLine + "Press any key to exit . . .");
				Console.ReadKey();
			}
		}
	}
}
