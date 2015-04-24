using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOdysseyModule
{
	public class NetOdysseyModuleBase : INetOdysseyPacketAnalyzerModule, INetOdysseyBCTUAnalyzerModule, IDisposable, NetOdysseyHealthReporter.IHealthReporter
	{
		#region IDisposable Members
		public void Dispose()
		{
			AddTask(null);
			_moduleThread.Join();
		}
		#endregion

		#region NetOdyssey.IHealthReporter Members
		public string HealthReport()
		{
			string healthReport;
			lock (_tasksQueue)
			{
				healthReport = _moduleName + ": Tasks queue: " + _tasksQueue.Count;
			}
			return healthReport;
		}
		#endregion

		Queue<Task> _tasksQueue = new Queue<Task>();
		EventWaitHandle _wh = new AutoResetEvent(false);
		Thread _moduleThread;
		bool _isTaskFree;

		string _moduleName;
		public string prpModuleName
		{
			get { return _moduleName; }
			set { _moduleName = value; }
		}

		string _reportFolder;
		public string prpReportFolder
		{
			get { return _reportFolder; }
			set { _reportFolder = value; }
		}

		FileStream _reportFile;
		StreamWriter _reportFileStreamWriter;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public NetOdysseyModuleBase()
		{
			_moduleThread = new Thread(Work);
		}

		/// <summary>
		/// Work method for the module thread.
		/// </summary>
		void Work()
		{
			Task _currTask;
			try
			{
				while (true)
				{
					_currTask = null;
					lock (_tasksQueue)
						if (_tasksQueue.Count > 0)
						{
							_currTask = _tasksQueue.Dequeue();
							if (_currTask == null) return;
						}
					if (_currTask != null) {
						_isTaskFree = false;
						switch (_currTask.prpTask)
						{
							case ModuleTask.AddFlow: AnalyzeFlowIn(_currTask.prpFlow, _currTask.prpWindowSize); break;
							case ModuleTask.AddPacket: AnalyzePacketIn(_currTask.prpPacket, _currTask.prpWindowSize); break;
							case ModuleTask.AddBCTU: AnalyzeBCTUIn(_currTask.prpBCTU, _currTask.prpWindowSize); break;
							case ModuleTask.RemoveFlow: AnalyzeFlowOut(_currTask.prpFlow, _currTask.prpWindowSize); break;
							case ModuleTask.RemovePacket: AnalyzePacketOut(_currTask.prpPacket, _currTask.prpWindowSize); break;
							case ModuleTask.RemoveBCTU: AnalyzeBCTUOut(_currTask.prpBCTU, _currTask.prpWindowSize); break;
							case ModuleTask.Clear: Clear(); break;
							case ModuleTask.Report: ModuleReport(); break;
							case ModuleTask.Start: StartModule(); break;
							case ModuleTask.Finish: TerminateModule(); break;
						}
					}
					else 
					{
						_isTaskFree = true;
						_wh.WaitOne();
					}
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception e)
			{
				Console.WriteLine(_moduleName + " exception: " + e.Message);
			}
		}

		/// <summary>
		/// Method to start the module thread.
		/// </summary>
		public void Start()
		{
			if (_moduleName == null)
				throw new Exception("prpModuleName must not be null");
			if (_reportFolder == null)
				throw new Exception("prpReportFolder must not be null");
			if (_moduleThread.IsAlive)
				throw new Exception("Module Thread is already alive (" + _moduleName + ")");
			_reportFile = File.Create(prpReportFolder + @"\" + prpModuleName + ".NetOdysseyReport");
			_reportFileStreamWriter = new StreamWriter(_reportFile);
			_moduleThread.Start();
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.Start)));
		}

		void AddTask(Task inTask)
		{
			lock (_tasksQueue)
				_tasksQueue.Enqueue(inTask);
			_wh.Set();
		}

		public void FlowIn(Flow Flow, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.AddFlow, Flow, WindowSize)));
		}

		public void PacketIn(PacketDotNet.Packet Packet, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.AddPacket, Packet, WindowSize)));
		}

		public void BCTUIn(ulong BCTU, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.AddBCTU, BCTU, WindowSize)));
		}

		public void FlowOut(Flow Flow, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.RemoveFlow, Flow, WindowSize)));
		}

		public void PacketOut(PacketDotNet.Packet Packet, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.RemovePacket, Packet, WindowSize)));
		}

		public void BCTUOut(ulong BCTU, int WindowSize)
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.RemoveBCTU, BCTU, WindowSize)));
		}

		public void ClearPacketsFlows()
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.Clear)));
		}

		public void Report()
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.Report)));
		}

		public void TerminateThread()
		{
			AddTask(new Task(new clsTaskArgumentWrapper(ModuleTask.Finish)));
		}

		public bool isTaskFree
		{
			get { return _isTaskFree; }
		}

		#region To be Overriden Methods
		public virtual string ModuleStart()
		{
			Console.WriteLine("ModuleStart() was not overriden!");
			return "ModuleStart() was not overriden!";
		}

		public virtual string ModuleEnd()
		{
			Console.WriteLine("ModuleEnd() was not overriden!");
			return "ModuleEnd() was not overriden!";
		}

		public virtual void AnalyzeFlowIn(Flow Flow, int WindowSize)
		{
			Console.WriteLine("Default AnalyzeFlowIn(Flow Flow, int WindowSize) !please override! |" +
								" WindowSize: " + WindowSize);
		}

		public virtual void AnalyzePacketIn(PacketDotNet.Packet Packet, int WindowSize)
		{
			Console.WriteLine("Default AnalyzePacketIn(PacketDotNet.Packet Packet, int WindowSize) !please override! | seconds: " + Packet.Timeval.Seconds +
								" microseconds: " + Packet.Timeval.MicroSeconds +
								" len: " + Packet.BytesHighPerformance.Length +
								" WindowSize: " + WindowSize);
		}

		public virtual void AnalyzeBCTUIn(ulong BCTU, int WindowSize)
		{
			Console.WriteLine("Default AnalyzeBCTUIn(ulong BCTU, int WindowSize) !please override! | BCTU: " + BCTU +
								" WindowSize: " + WindowSize);
		}

		public virtual void AnalyzeFlowOut(Flow Flow, int WindowSize)
		{
			Console.WriteLine("Default AnalyzeFlowOut(Flow Flow, int WindowSize) !please override! |" +
								" WindowSize: " + WindowSize);
		}

		public virtual void AnalyzePacketOut(PacketDotNet.Packet Packet, int WindowSize)
		{
			Console.WriteLine("Default AnalyzePacketOut(PacketDotNet.Packet Packet, int WindowSize) !please override! | seconds: " + Packet.Timeval.Seconds +
								" microseconds: " + Packet.Timeval.MicroSeconds +
								" len: " + Packet.BytesHighPerformance.Length +
								" WindowSize: " + WindowSize);
		}

		public virtual void AnalyzeBCTUOut(ulong BCTU, int WindowSize)
		{
			Console.WriteLine("Default AnalyzeBCTUOut(ulong BCTU, int WindowSize) !please override! | BCTU: " + BCTU +
								" WindowSize: " + WindowSize);
		}

		public virtual void Clear()
		{
			Console.WriteLine("Default Clear() !Please override!");
		}

		public virtual string ReportAnalysis()
		{
			Console.WriteLine("ReportAnalysis() was not overriden!");
			return "ReportAnalysis() was not overriden!";
		}
		#endregion

		/// <summary>
		/// Starts a module.
		/// </summary>
		void StartModule()
		{
			_reportFileStreamWriter.Write(ModuleStart());
		}

		/// <summary>
		/// Terminates a module.
		/// </summary>
		void TerminateModule()
		{
			_reportFileStreamWriter.Write(ModuleEnd());
			_reportFileStreamWriter.Close();
			_reportFile.Close();
			_moduleThread.Abort();
		}

		/// <summary>
		/// Reports window analysis results to the file.
		/// </summary>
		public void ModuleReport()
		{
			_reportFileStreamWriter.Write(ReportAnalysis());
		}
	}

	/// <summary>
	/// The module Task enumerator. Contains all available tasks.
	/// </summary>
	enum ModuleTask
	{
		AddFlow,
		AddPacket,
		AddBCTU,
		RemoveFlow,
		RemovePacket,
		RemoveBCTU,
		Clear,
		Report,
		Start,
		Finish
	}

	class Task
	{
		Flow _flow;
		public Flow prpFlow
		{
			get { return _flow; }
		}

		PacketDotNet.Packet _packet;
		public PacketDotNet.Packet prpPacket
		{
			get { return _packet; }
		}

		ulong _bctu;
		public ulong prpBCTU
		{
			get { return _bctu; }
		}

		ModuleTask _task;
		public ModuleTask prpTask
		{
			get { return _task; }
		}

		int _windowSize;
		public int prpWindowSize
		{
			get { return _windowSize; }
		}

		public Task(clsTaskArgumentWrapper inArgumentWrapper)
		{
			_task = inArgumentWrapper.ModuleTask;
			_flow = inArgumentWrapper.Flow;
			_packet = inArgumentWrapper.Packet;
			_bctu = inArgumentWrapper.BCTU;
			_windowSize = inArgumentWrapper.WindowSize;
		}
	}
}
