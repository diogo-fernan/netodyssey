using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOdyssey
{
	class clsHealthMonitor
	{
		Thread _thrHealthMonitor;
		List<NetOdysseyHealthReporter.IHealthReporter> _modules = new List<NetOdysseyHealthReporter.IHealthReporter>();
		int _healthMonitorInterval;

		/// <summary>
		/// Constructor method. Used to instantiate a new health monitor.
		/// </summary>
		/// <param name="inHealthMonitorInterval">The interval between health reports.</param>
		public clsHealthMonitor(int inHealthMonitorInterval) 
		{
			_thrHealthMonitor = new Thread(Work);
			_healthMonitorInterval = inHealthMonitorInterval;
		}

		/// <summary>
		/// Adds a module to the health monitor.
		/// </summary>
		/// <param name="inModule">The module to add.</param>
		public void addModule(NetOdysseyHealthReporter.IHealthReporter inModule)
		{
			_modules.Add(inModule);            
		}

		/// <summary>
		/// Starts the health monitor inherent thread.
		/// </summary>
		public void Start() 
		{
			if (_thrHealthMonitor.IsAlive)
				throw new Exception("Health Monitor Thread is already alive");
			_thrHealthMonitor.Start();
		}

		/// <summary>
		/// Stops the health monitor.
		/// </summary>
		public void Stop()
		{
			clsMessages.PrintHealthThreadStopping();
			_thrHealthMonitor.Abort();
		}

		/// <summary>
		/// The work method for the health monitor thread.
		/// </summary>
		void Work() 
		{
			try
			{
				string report;
				while (true)
				{
					report = "";

					for (int i = 0; i < _modules.Count; i++)
						report += _modules[i].HealthReport() + " ";                        
					
					// foreach (NetOdysseyHealthReporter.IHealthReporter module in _modules)
					//    report += module.HealthReport() + " ";

					Console.WriteLine(report);
					Thread.Sleep(_healthMonitorInterval);
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				clsMessages.PrintError("Exception in Health monitor thread: " + ex.Message);
			}
		}
	}
}
