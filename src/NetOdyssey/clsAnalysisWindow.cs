using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NetOdyssey {
	class clsAnalysisWindow : IDisposable, NetOdysseyHealthReporter.IHealthReporter {
		#region IDisposable Members
		public void Dispose() {
			_thrAnalysisWindow.Join();
			//_wh.Close();
			clsMessages.PrintAnalyzerThreadStopped();
		}
		#endregion

		#region IHealthReporter Members
		public string HealthReport() {
			string healthReport;
			if (_analysisLevel != AnalysisLevel.InterFlow)
			{
				lock (_packetsQueue) {
					healthReport = "Packet queue: " + _packetsQueue.Count;
				}
			}
			else
			{
				lock (_flowsQueue)
				{
					healthReport = "Flow queue: " + _flowsQueue.Count;					
				}
			}

			return healthReport;
		}
		#endregion

		AnalysisLevel _analysisLevel = Program.prpSettings.AnalysisLevel;

		Queue<NetOdysseyModule.clsFlow> _flowsQueue = new Queue<NetOdysseyModule.clsFlow>();
		Queue<PacketDotNet.Packet> _packetsQueue = new Queue<PacketDotNet.Packet>();
		Queue<ulong> _bctuQueue = new Queue<ulong>();

		Queue<NetOdysseyModule.clsFlow> _awFlowsQueue = new Queue<NetOdysseyModule.clsFlow>();
		Queue<PacketDotNet.Packet> _awPacketsQueue = new Queue<PacketDotNet.Packet>();
		Queue<ulong> _awBCTUQueue = new Queue<ulong>();

		EventWaitHandle _wh = new AutoResetEvent(false);
		Thread _thrAnalysisWindow;
		Thread _thrAWT;

		int _modulesDictionaryKey;

		/// <summary>
		/// Constructor method. Used to instantiate a new analysis window.
		/// </summary>
		/// <param name="inModulesDictionaryKey">The key of the correspondent modules list of this analysis window in the modules dictionary.</param>
		public clsAnalysisWindow(int inModulesDictionaryKey)
		{
			_modulesDictionaryKey = inModulesDictionaryKey;
			_thrAnalysisWindow = new Thread(Work);
			// _thrAWT = new Thread(AWTWork);
		}

		/// <summary>
		/// Starts the analysis window inherent thread.
		/// </summary>
		public void Start()
		{
			if (_thrAnalysisWindow.IsAlive)
				throw new Exception("Analysis Thread is already alive");
			_thrAnalysisWindow.Start();
		}

		/// <summary>
		/// Stops the analysis window.
		/// </summary>
		public void Stop()
		{
			clsMessages.PrintAnalyzerThreadStopping();
			EnqueueFlow(null);
			Enqueue(null);
			Enqueue(ulong.MaxValue);
			this.Dispose();
		}

		/// <summary>
		/// Enqueues a flow to the flows queue.
		/// </summary>
		/// <param name="inFlow">The flow to enqueue.</param>
		public void EnqueueFlow(NetOdysseyModule.clsFlow inFlow)
		{
			lock (_flowsQueue)
				_flowsQueue.Enqueue(inFlow);
			_wh.Set();
		}

		/// <summary>
		/// Enqueues a packet to the packets queue.
		/// </summary>
		/// <param name="inPacket">The packet to enqueue.</param>
		public void Enqueue(PacketDotNet.Packet inPacket)
		{
			lock (_packetsQueue)
				_packetsQueue.Enqueue(inPacket);
			_wh.Set();
		}

		/// <summary>
		/// Enqueues a BCTU to the BCTU queue.
		/// </summary>
		/// <param name="inBCTU">The BCTU to enqueue.</param>
		public void Enqueue(ulong inBCTU)
		{
			lock (_bctuQueue)
				_bctuQueue.Enqueue(inBCTU);
			_wh.Set();
		}

		/// <summary>
		/// The work method for the capturer thread.
		/// </summary>
		void Work()
		{
			int _aws = (int) Program.prpSettings.AnalysisWindowSize;
			CaptureMode _capMode = Program.prpSettings.CaptureMode;

			uint _capturedFlows = 0;
			uint _capturedPackets = 0;
			if (Program.prpSettings.AnalysisWindowTime > 0)
			{	
				_thrAWT = new Thread(AWTWork);
				_thrAWT.Start();
			}

			try {
				while (true) {
					#region CaptureMode == Packets
					if (_capMode == CaptureMode.Packets)
					{
						#region AnalysisLevel != AnalysisLevel.InterFlow
						if (_analysisLevel != AnalysisLevel.InterFlow)
						{
							PacketDotNet.Packet _currPacket = null;
							lock (_packetsQueue) {
								if (_packetsQueue.Count > 0) {
									_currPacket = _packetsQueue.Dequeue();
									if (_currPacket == null) {
										foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
											module.TerminateThread();
										return;
									}
								}
							}
							if (_currPacket != null) {
								lock (_awPacketsQueue)
									_awPacketsQueue.Enqueue(_currPacket);
								if (_aws > 0) {
									foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
										module.PacketIn(_currPacket, _aws);

									if (++_capturedPackets >= _aws) {
										_currPacket = _awPacketsQueue.Dequeue();
										foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
										{
											module.Report();
											module.PacketOut(_currPacket, _aws);
										}
									}
								}
							} 
							else
								_wh.WaitOne();
						}
						#endregion
						#region AnalysisLevel == AnalysisLevel.InterFlow
						else
						{
							NetOdysseyModule.clsFlow _currChildFlow = null;
							lock (_flowsQueue)
							{
								if (_flowsQueue.Count > 0)
								{
									_currChildFlow = _flowsQueue.Dequeue();
									if (_currChildFlow == null)
									{
										foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
											module.TerminateThread();

										return;
									}
								}
							}
							if (_currChildFlow != null)
							{
								lock (_awFlowsQueue)
									_awFlowsQueue.Enqueue(_currChildFlow);
								if (_aws > 0)
								{
									foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
									{
										module.FlowIn((NetOdysseyModule.Flow) _currChildFlow, _aws);
									}

									if (++_capturedFlows >= _aws)
									{
										_currChildFlow = _awFlowsQueue.Dequeue();
										foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
										{
											module.Report();
											module.FlowOut((NetOdysseyModule.Flow) _currChildFlow, _aws);
										}
									}
								}
							}
							else
								_wh.WaitOne();
						}
						#endregion
					}
					#endregion
					#region CaptureMode == Statistics
					if (_capMode == CaptureMode.Statistics) {
						ulong _currBCTU;
						_currBCTU = ulong.MaxValue;
						lock (_bctuQueue) {
							if (_bctuQueue.Count > 0) {
								_currBCTU = _bctuQueue.Dequeue();
								if (_currBCTU == ulong.MaxValue) {
									foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
										module.TerminateThread();

									return;
								}
							}
						}
						if (_currBCTU != ulong.MaxValue) {
							lock (_awBCTUQueue)
								_awBCTUQueue.Enqueue(_currBCTU);
							if (_aws > 0) {
								foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
									module.BCTUIn(_currBCTU, _aws);

								if (++_capturedPackets >= _aws) {
									_currBCTU = _awBCTUQueue.Dequeue();
									foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
									{
										module.Report();
										module.BCTUOut(_currBCTU, _aws);
									}
								}
							}
						} else 
							_wh.WaitOne();
					}
					#endregion
				}
			} catch (Exception ex) {
				clsMessages.PrintError("Exception in Analysis Window thread: " + ex.Message);
			}
		}

		/// <summary>
		/// The work method for the AWT thread.
		/// </summary>
		void AWTWork()
		{
			TimeSpan _nextAWT = TimeSpan.FromMilliseconds(Program.prpSettings.AnalysisWindowTime);
			while (true) {
				System.Threading.Thread.Sleep(_nextAWT);
				if (_analysisLevel != AnalysisLevel.InterFlow)
					lock (_awPacketsQueue) {
						foreach (PacketDotNet.Packet _packet in _awPacketsQueue)
							foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
								module.AnalyzePacketIn(_packet, _awPacketsQueue.Count);
					
							_awPacketsQueue.Clear();
					}
				else
					lock (_awFlowsQueue)
					{
						foreach (NetOdysseyModule.clsFlow flow in _awFlowsQueue)
							foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
								module.AnalyzeFlowIn(flow, _awFlowsQueue.Count);

						_awFlowsQueue.Clear();
					}

				foreach (NetOdysseyModule.NetOdysseyModuleBase module in Program.prpModulesDictionary[_modulesDictionaryKey])
				{
					module.Report();
					module.ClearPacketsFlows();
				}

				if (!_thrAnalysisWindow.IsAlive) return;
			}
		}
	}
}
