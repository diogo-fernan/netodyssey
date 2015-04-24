using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace NetOdyssey {
	class clsCapturer : IDisposable, NetOdysseyHealthReporter.IHealthReporter {
		#region IDisposable Members
		public void Dispose() {
			try {
				if (prpIsCaptureLive) {
					prpLiveDevice.StopCaptureTimeout = new TimeSpan(0, 0, 15);
					prpLiveDevice.Close();
					if (prpLiveDevice.DumpOpened) {
						prpLiveDevice.DumpFlush();
						prpLiveDevice.DumpClose();
					}
				} else {
					prpOfflineDevice.Close();
					if (prpOfflineDevice.DumpOpened) {
						prpOfflineDevice.DumpFlush();
						prpOfflineDevice.DumpClose();
					}
				}
				// _thrCapturer.Abort();
			} catch {}
			clsMessages.PrintCapturerThreadStopped();
		}
		#endregion

		#region IHealthReporter Members
		public string HealthReport() {
			string healthReport = "No health report from clsCapturer";
			if (Program.prpSettings.CaptureMode == CaptureMode.Packets) {
				lock (_capturedPacketsFlowsLock)
					healthReport = "Packets captured: " + _capturedPackets;
				if (_packets_StatisticsToCapture > 0)
					healthReport += "/" + _packets_StatisticsToCapture;
				lock (_capturedPacketsFlowsLock)
					healthReport += " Flows captured: " + (_capturedfFlows + 1);
			}
			if (Program.prpSettings.CaptureMode == CaptureMode.Statistics) {
				lock (_generatedStatisticsLock)
					healthReport = "Statistics generated: " + _generatedStatistics;
				if (_packets_StatisticsToCapture > 0)
					healthReport += "/" + _packets_StatisticsToCapture;
			}
			if (prpIsCaptureLive && prpLiveDevice.Opened)
				healthReport += Environment.NewLine + prpLiveDevice.Statistics().ToString() + Environment.NewLine;
			return healthReport;
		}
		#endregion

		SharpPcap.LivePcapDevice _liveDevice;
		SharpPcap.OfflinePcapDevice _offlineDevice;

		Thread _thrCapturer;
		Thread _thrCapturerStop;

		clsHealthMonitor _healthMonitor;

		Dictionary<int, clsAnalysisWindow> _dicAnalysisWindows = new Dictionary<int, clsAnalysisWindow>();
		Dictionary<int, NetOdysseyModule.clsTrafficSubset> _dicTrafficSubsets = new Dictionary<int, NetOdysseyModule.clsTrafficSubset>();
		Dictionary<int, NetOdysseyModule.clsFlow> _dicFlows = new Dictionary<int, NetOdysseyModule.clsFlow>();
		Queue<int> _keysToRemove = new Queue<int>();

		uint _packets_StatisticsToCapture = Program.prpSettings.Packets_StatisticsToCapture;
		uint _bctu = Program.prpSettings.BitCountPerTimeUnit;
		uint _capturedPackets;
		uint _generatedStatistics;
		object _capturedPacketsFlowsLock = new object();
		object _generatedStatisticsLock = new object();

		DateTime _captureStart;
		TimeSpan _captureDuration;

		/// <summary>
		/// Gets the number that the capture mode has reached.
		/// </summary>
		public uint prpCapturedPackets_Statistics {
			get { return (Program.prpSettings.CaptureMode == CaptureMode.Packets) ? _capturedPackets : _generatedStatistics; }
		}

		/// <summary>
		/// Gets the offline capture device.
		/// </summary>
		public SharpPcap.OfflinePcapDevice prpOfflineDevice {
			get { return _offlineDevice; }
		}

		/// <summary>
		/// Gets the live capture device.
		/// </summary>
		public SharpPcap.LivePcapDevice prpLiveDevice {
			get { return _liveDevice; }
		}

		/// <summary>
		/// Gets true if the capture is live, false otherwise.
		/// </summary>
		public bool prpIsCaptureLive {
			get { return _liveDevice != null; }
		}

		/// <summary>
		/// Gets the capture duration.
		/// </summary>
		public TimeSpan prpCaptureDuration {
			get { return _captureDuration; }
		}

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="inCaptureDevice">The device to capture from. This may be online or offline.</param>
		public clsCapturer(SharpPcap.PcapDevice inCaptureDevice) {
			if (inCaptureDevice is SharpPcap.LivePcapDevice)
				_liveDevice = inCaptureDevice as SharpPcap.LivePcapDevice;
			else if (inCaptureDevice is SharpPcap.OfflinePcapDevice)
				_offlineDevice = inCaptureDevice as SharpPcap.OfflinePcapDevice;
			else
				throw new Exception("Invalid capture device.");

			_thrCapturer = new Thread(Work);
			_thrCapturerStop = new Thread(WorkthrCapturerStop);
		}

		/// <summary>
		/// Starts the capture.
		/// </summary>
		/// <param name="inHealthMonitor">The health monitor object.</param>
		public void Start(clsHealthMonitor inHealthMonitor)
		{
			if (inHealthMonitor == null)
				throw new Exception("inHealthMonitor must not be null");
			if (_thrCapturer.IsAlive)
				throw new Exception("Capturer Thread is already alive");

			_healthMonitor = inHealthMonitor;
			_capturedPackets = 0;

			// _thrCapturer.Priority = ThreadPriority.Highest;

			_thrCapturer.Start();
			_healthMonitor.Start();
		}

		/// <summary>
		/// Stops the capture.
		/// </summary>
		public void Stop()
		{
			if (Program.prpSettings.AnalysisSettings == AnalysisSettings.AllTraffic &
				Program.prpSettings.AnalysisLevel == AnalysisLevel.InterFlow)
			{
				foreach (KeyValuePair<int, NetOdysseyModule.clsFlow> keyValuePair in _dicFlows)
				{
					_dicAnalysisWindows[0].EnqueueFlow(keyValuePair.Value);
				}
			}
			else if ((Program.prpSettings.AnalysisSettings == AnalysisSettings.PerSourceIP ||
					Program.prpSettings.AnalysisSettings == AnalysisSettings.PerApplication) &&
					Program.prpSettings.AnalysisLevel == AnalysisLevel.InterFlow)
			{
				foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
				{
					foreach (KeyValuePair<int, NetOdysseyModule.clsFlow> keyValuePairWithin in keyValuePair.Value.FlowsDictionary)
					{
						_dicAnalysisWindows[keyValuePair.Key].EnqueueFlow(keyValuePairWithin.Value);
					}
				}
			}

			clsMessages.PrintCapturerThreadStopping();
			_healthMonitor.Stop();
			_thrCapturer.Abort();

			foreach (KeyValuePair<int, clsAnalysisWindow> keyValuePair in _dicAnalysisWindows)
				keyValuePair.Value.Stop();

			this.Dispose();
		}

		/// <summary>
		/// The work method for the capturer stop thread. It is used to obtain the duration of the capture.
		/// </summary>
		void WorkthrCapturerStop()
		{
			try {
				Thread.Sleep((int) Program.prpSettings.SecondsToCapture * 1000);
				_captureDuration = DateTime.Now - _captureStart;
				clsMessages.PrintAllDone();
				clsSettings._wh.Set();
			} catch { }
		}
		
		/// <summary>
		/// The work method for the capturer thread. It is used to capture packets or statistics from the device.
		/// </summary>
		void Work()
		{
			PacketDotNet.RawPacket _capturedPacket;
			PacketDotNet.Packet _currPacket;
			PacketDotNet.IpPacket _ipPacket;
			PacketDotNet.TcpPacket _tcpPacket;
			PacketDotNet.UdpPacket _udpPacket;

			string _srcIPAddress;
			string _dstIPAddress;
			ushort _srcPort;
			ushort _dstPort;

			NetOdysseyModule.TransportProtocol _transportProtocol;

			bool _exists = false;

			_captureStart = DateTime.Now;
			if (Program.prpSettings.SecondsToCapture > 0) {
				_thrCapturerStop.Start();
			}
			#region CaptureMode == Packets
			if (Program.prpSettings.CaptureMode == CaptureMode.Packets) {
				try {
					if (prpIsCaptureLive) {
						prpLiveDevice.Open(SharpPcap.DeviceMode.Promiscuous);
						if (Program.prpSettings.TcpDumpFilter != "")
							prpLiveDevice.SetFilter(Program.prpSettings.TcpDumpFilter);
						if (Program.prpSettings.DumpCapture)
							prpLiveDevice.DumpOpen(Program.prpSettings.prpDumpFile);
					} 
					else
						prpOfflineDevice.Open();
					while (true) {
						if (prpIsCaptureLive)
							_capturedPacket = prpLiveDevice.GetNextPacket();
						else
							_capturedPacket = prpOfflineDevice.GetNextPacket();
						if (_capturedPacket == null) continue;

						_currPacket = PacketDotNet.Packet.ParsePacket(_capturedPacket);

						switch (Program.prpSettings.AnalysisSettings)
						{
							case AnalysisSettings.AllTraffic:
								switch (Program.prpSettings.AnalysisLevel)
								{
									case AnalysisLevel.PacketByPacket:
										#region PacketByPacket
										if (_capturedfFlows > -1)
											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										else
										{
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											NewAnalysisWindow();
											NewModulesList();
											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										}
										break;
										#endregion
									case AnalysisLevel.IntraFlow:
										#region IntraFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;
										
										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										_srcIPAddress = _ipPacket.SourceAddress.ToString();
										_dstIPAddress = _ipPacket.DestinationAddress.ToString();

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = (PacketDotNet.TcpPacket) PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;
										#endregion

										if (_transportProtocol != Program.prpSettings.TransportProtocol)
											continue;

										#region Resolve IP Addresses and Ports
										if (Program.prpSettings.SourceIPAddress.ToString().Equals("0.0.0.0"))
											_srcIPAddress = "0.0.0.0";
										if (Program.prpSettings.DestinationIPAddress.ToString().Equals("0.0.0.0"))
											_dstIPAddress = "0.0.0.0";
										if (Program.prpSettings.SourcePort == 0)
											_srcPort = 0;
										if (Program.prpSettings.DestinationPort == 0)
											_dstPort = 0;
										#endregion

										if (!_exists)
										{
											#region Unidirectional
											if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
											{
												if (!_srcIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) ||
													!_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) ||
													!(_srcPort == Program.prpSettings.SourcePort) ||
													!(_dstPort == Program.prpSettings.DestinationPort))
												{
													continue;
												}
											}
											#endregion
											#region Bi-Directional
											else
											{
												if ((!_srcIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) ||
													!_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) ||
													!(_srcPort == Program.prpSettings.SourcePort) ||
													!(_dstPort == Program.prpSettings.DestinationPort)) &&
													(!_srcIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) ||
													!_dstIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) ||
													!(_srcPort == Program.prpSettings.DestinationPort) ||
													!(_dstPort == Program.prpSettings.SourcePort)))
												{
													continue;
												}
											}
											#endregion

											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											NewAnalysisWindow();
											NewModulesList();
											Program.prpHealthMonitor.addModule(_dicAnalysisWindows[0]);
											foreach (KeyValuePair<int, List<NetOdysseyModule.NetOdysseyModuleBase>> keyValuePair in Program.prpModulesDictionary)
											{
												foreach (NetOdysseyModule.NetOdysseyModuleBase module in keyValuePair.Value)
												{
													Program.prpHealthMonitor.addModule(module);
												}
											}
										}
										
										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (_srcIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) &&
												_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) &&
												_srcPort == Program.prpSettings.SourcePort &&
												_dstPort == Program.prpSettings.DestinationPort)
											{
												_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
											}
											else
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if ((_srcIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) &&
												_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) &&
												_srcPort == Program.prpSettings.SourcePort &&
												_dstPort == Program.prpSettings.DestinationPort) ||
												(_srcIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) &&
												_dstIPAddress.Equals(Program.prpSettings.SourceIPAddress.ToString()) &&
												_srcPort == Program.prpSettings.DestinationPort &&
												_dstPort == Program.prpSettings.SourcePort))
											{
												_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
											}
											else
											{
												continue;
											}
										}
										#endregion
										break;
										#endregion
									case AnalysisLevel.InterFlow:
										#region InterFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;

										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = (PacketDotNet.TcpPacket) PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = _tcpPacket.SourcePort;
											_dstPort = _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = _udpPacket.SourcePort;
											_dstPort = _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;
										#endregion

										if (_exists)
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsFlow> keyValuePair in _dicFlows)
											{
												if (_currPacket.Timeval.Date < keyValuePair.Value.FlowTimeoutValue)
												{
													#region Unidirectional
													if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
													{
														if (_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress) &&
															_ipPacket.DestinationAddress.Equals(keyValuePair.Value._dstIPAddress) &&
															(_srcPort == keyValuePair.Value._srcPort) &&
															(_dstPort == keyValuePair.Value._dstPort) &&
															(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
															keyValuePair.Value._transportProtocol.Equals(_transportProtocol) :
															true))
														{
															keyValuePair.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
															_exists = true;
														}
													}
													#endregion
													#region Bi-Directional
													else
													{
														if (((_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress) &&
															_ipPacket.DestinationAddress.Equals(keyValuePair.Value._dstIPAddress) &&
															_srcPort == keyValuePair.Value._srcPort &&
															_dstPort == keyValuePair.Value._dstPort) ||
															(_ipPacket.SourceAddress.Equals(keyValuePair.Value._dstIPAddress) &&
															_ipPacket.DestinationAddress.Equals(keyValuePair.Value._srcIPAddress) &&
															_srcPort == keyValuePair.Value._dstPort &&
															_dstPort == keyValuePair.Value._srcPort)) &&
															(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
																keyValuePair.Value._transportProtocol.Equals(_transportProtocol) :
																true))
														{
															keyValuePair.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
															_exists = true;
														}
													}
													#endregion
												}
												else
												{
													_keysToRemove.Enqueue(keyValuePair.Key);
													_dicAnalysisWindows[0].EnqueueFlow(keyValuePair.Value);
												}						
											}

											foreach (int key in _keysToRemove)
												_dicFlows.Remove(key);
											_keysToRemove.Clear();
										}
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											if (_dicAnalysisWindows.Count == 0)
											{
												NewAnalysisWindow();
												NewModulesList();
												Program.prpHealthMonitor.addModule(_dicAnalysisWindows[0]);
												foreach (KeyValuePair<int, List<NetOdysseyModule.NetOdysseyModuleBase>> keyValuePair in Program.prpModulesDictionary)
												{
													foreach (NetOdysseyModule.NetOdysseyModuleBase module in keyValuePair.Value)
													{
														Program.prpHealthMonitor.addModule(module);
													}
												}
											}

											_dicFlows.Add(_capturedfFlows,
												new NetOdysseyModule.clsFlow(_ipPacket.SourceAddress,
																				_ipPacket.DestinationAddress,
																				_srcPort,
																				_dstPort,
																				_transportProtocol));
											_dicFlows[_capturedfFlows].EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
										}
										break;
										#endregion
								}
								break;
							case AnalysisSettings.PerSourceIP:
								switch (Program.prpSettings.AnalysisLevel)
								{
									case AnalysisLevel.PacketByPacket:
										#region PacketByPacket
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;

										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) &&
												!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress))
											{
												continue;
											}
										}
										#endregion
										#endregion

										if (_exists) 
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												#region Unidirectional
												if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
												{
													if (keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress))
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
												#region Bi-Directional
												else 
												{
													if ((keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) || 
														keyValuePair.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress)))
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
											}
										}
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}

											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
												   new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, false));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													   new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, false));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										}
										break;
										#endregion
									case AnalysisLevel.IntraFlow:
										#region IntraFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;

										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										_srcIPAddress = _ipPacket.SourceAddress.ToString();
										_dstIPAddress = _ipPacket.DestinationAddress.ToString();

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) ||
												!(_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_dstPort == Program.prpSettings.DestinationPort))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if ((!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) ||
												!(_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_dstPort == Program.prpSettings.DestinationPort)) &&
												(!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress) ||
												!(_srcIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_srcPort == Program.prpSettings.DestinationPort)))
											{
												continue;
											}
										}
										#endregion
										#endregion

										if (_transportProtocol != Program.prpSettings.TransportProtocol)
											continue;

										#region Resolve IP Addresses and Ports
										if (Program.prpSettings.DestinationIPAddress.ToString() == "0.0.0.0")
											_dstIPAddress = "0.0.0.0";
										if (Program.prpSettings.DestinationPort == 0)
											_dstPort = 0;
										#endregion

										if (_exists) 
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												#region Unidirectional
												if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
												{
													if (keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
														_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) &&
														_dstPort == Program.prpSettings.DestinationPort)
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
												#region Bi-Directional
												else 
												{
													if ((keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
														_ipPacket.DestinationAddress.Equals(Program.prpSettings.DestinationIPAddress) &&
														_dstPort == Program.prpSettings.DestinationPort) || 
														(keyValuePair.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress) &&
														_ipPacket.SourceAddress.Equals(Program.prpSettings.DestinationIPAddress) &&
														_srcPort == Program.prpSettings.DestinationPort))
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
											}
										}
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											
											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, false));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
														new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, false));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										}
										break;
										#endregion
									case AnalysisLevel.InterFlow:
										#region InterFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;
										
										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) &&
												!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress))
											{
												continue;
											}
										}
										#endregion

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;
										#endregion

										if (_exists)
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												if ((Program.prpSettings.FlowDirection == FlowDirection.Unidirectional &&
													_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress)) ||
													(Program.prpSettings.FlowDirection == FlowDirection.Bidirectional &&
													(_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress) ||
													_ipPacket.DestinationAddress.Equals(keyValuePair.Value._srcIPAddress))))
												{
													foreach (KeyValuePair<int, NetOdysseyModule.clsFlow> keyValuePairWithin in keyValuePair.Value.FlowsDictionary)
													{
														if (_currPacket.Timeval.Date < keyValuePairWithin.Value.FlowTimeoutValue)
														{
															#region Unidirectional
															if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
															{
																if (keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._srcPort == _srcPort &&
																	keyValuePairWithin.Value._dstPort == _dstPort &&
																	(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
																	keyValuePairWithin.Value._transportProtocol.Equals(_transportProtocol) :
																	true))
																{
																	keyValuePairWithin.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
																	_exists = true;
																}
															}
															#endregion
															#region Bi-Directional
															else
															{
																if (((keyValuePairWithin.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
																	keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._srcPort == _srcPort &&
																	keyValuePairWithin.Value._dstPort == _dstPort) ||
																	(keyValuePairWithin.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.SourceAddress) &&
																	keyValuePairWithin.Value._srcPort == _dstPort &&
																	keyValuePairWithin.Value._dstPort == _srcPort)) &&
																	(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
																	keyValuePairWithin.Value._transportProtocol.Equals(_transportProtocol) :
																	true))
																{
																	keyValuePairWithin.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
																	_exists = true;
																}
															}
															#endregion
														}
														else
														{
															_dicAnalysisWindows[keyValuePair.Key].EnqueueFlow(keyValuePairWithin.Value);
															_keysToRemove.Enqueue(keyValuePairWithin.Key);
														}
													}
													foreach (int key in _keysToRemove)
														keyValuePair.Value.RemoveFlow(key);
													_keysToRemove.Clear();
													#region A Child Flow Does Not Exist Within a Traffic Subset
													if (!_exists)
													{
														_exists = true;
														keyValuePair.Value.AddFlow(
															new NetOdysseyModule.clsFlow(_ipPacket.SourceAddress,
																						_ipPacket.DestinationAddress,
																						_srcPort,
																						_dstPort,
																						_transportProtocol));
														keyValuePair.Value.EnqueuePacketToLastFlow(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
													}
													#endregion
													break;
												}
											}
										}
										#region A Traffic Subset Does Not Exist
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											
											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, true));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, true));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicTrafficSubsets[_capturedfFlows].AddFlow(
												new NetOdysseyModule.clsFlow(_ipPacket.SourceAddress,
																				_ipPacket.DestinationAddress,
																				_srcPort,
																				_dstPort,
																				_transportProtocol));
											_dicTrafficSubsets[_capturedfFlows].EnqueuePacketToLastFlow(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
										}
										#endregion
										break;
										#endregion
								}
								break;
							case AnalysisSettings.PerApplication:
								switch (Program.prpSettings.AnalysisLevel)
								{
									case AnalysisLevel.PacketByPacket:
										#region PacketByPacket
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;

										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) &&
												!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress))
											{
												continue;
											}
										}
										#endregion

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
										}
										else	// Other transport protocol
											continue;
										#endregion

										if (_exists) 
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												#region Unidirectional
												if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
												{
													if (keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
														keyValuePair.Value._srcPort == _srcPort)
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
												#region Bi-Directional
												else 
												{
													if ((keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress)  && 
														keyValuePair.Value._srcPort == _srcPort) || 
														(keyValuePair.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress) &&
														keyValuePair.Value._srcPort == _dstPort))
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
											}
										}
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}

											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
												   new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, _srcPort, false));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													   new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, _dstPort, false));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										}
										break;
										#endregion
									case AnalysisLevel.IntraFlow:
										#region IntraFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;

										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										_srcIPAddress = _ipPacket.SourceAddress.ToString();
										_dstIPAddress = _ipPacket.DestinationAddress.ToString();

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) ||
												!(_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_dstPort == Program.prpSettings.DestinationPort))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if ((!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) ||
												!(_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_dstPort == Program.prpSettings.DestinationPort)) &&
												(!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress) ||
												!(_srcIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString())) ||
												!(_srcPort == Program.prpSettings.DestinationPort)))
											{
												continue;
											}
										}
										#endregion
										#endregion

										if (_transportProtocol != Program.prpSettings.TransportProtocol)
											continue;

										#region Resolve IP Addresses and Ports
										if (Program.prpSettings.DestinationIPAddress.ToString() == "0.0.0.0")
											_dstIPAddress = "0.0.0.0";
										if (Program.prpSettings.DestinationPort == 0)
											_dstPort = 0;
										#endregion

										if (_exists) 
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												#region Unidirectional
												if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
												{
													if (keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
														keyValuePair.Value._srcPort == _srcPort &&
														_dstIPAddress.Equals(Program.prpSettings.DestinationIPAddress.ToString()) &&
														_dstPort == Program.prpSettings.DestinationPort)
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
												#region Bi-Directional
												else 
												{
													if ((keyValuePair.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
														keyValuePair.Value._srcPort == _srcPort &&
														_ipPacket.DestinationAddress.Equals(Program.prpSettings.DestinationIPAddress) &&
														_dstPort == Program.prpSettings.DestinationPort) || 
														(keyValuePair.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress) &&
														keyValuePair.Value._srcPort == _dstPort &&
														_ipPacket.SourceAddress.Equals(Program.prpSettings.DestinationIPAddress) &&
														_srcPort == Program.prpSettings.DestinationPort))
													{
														_dicAnalysisWindows[keyValuePair.Key].Enqueue(_currPacket);
														_exists = true;
													}
												}
												#endregion
											}
										}
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											
											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, _srcPort, false));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
														new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, _dstPort, false));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicAnalysisWindows[_capturedfFlows].Enqueue(_currPacket);
										}
										break;
										#endregion
									case AnalysisLevel.InterFlow:
										#region InterFlow
										if (!(_currPacket.PayloadPacket is PacketDotNet.IpPacket))
											continue;
										
										#region Resolve Packet
										_ipPacket = PacketDotNet.IpPacket.GetEncapsulated(_currPacket);

										#region Unidirectional
										if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												continue;
											}
										}
										#endregion
										#region Bi-Directional
										else
										{
											if (!clsIPAddressRange.isInRange(_ipPacket.SourceAddress) &&
												!clsIPAddressRange.isInRange(_ipPacket.DestinationAddress))
											{
												continue;
											}
										}
										#endregion

										if (_ipPacket.PayloadPacket is PacketDotNet.TcpPacket)
										{
											_tcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _tcpPacket.SourcePort;
											_dstPort = (ushort) _tcpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.TCP;
										}
										else if (_ipPacket.PayloadPacket is PacketDotNet.UdpPacket)
										{
											_udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(_currPacket);
											_srcPort = (ushort) _udpPacket.SourcePort;
											_dstPort = (ushort) _udpPacket.DestinationPort;
											_transportProtocol = NetOdysseyModule.TransportProtocol.UDP;
										}
										else	// Other transport protocol
											continue;
										#endregion

										if (_exists)
										{
											_exists = false;
											foreach (KeyValuePair<int, NetOdysseyModule.clsTrafficSubset> keyValuePair in _dicTrafficSubsets)
											{
												if ((Program.prpSettings.FlowDirection == FlowDirection.Unidirectional &&
													_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress) &&
													_srcPort == keyValuePair.Value._srcPort) ||
													(Program.prpSettings.FlowDirection == FlowDirection.Bidirectional &&
													((_ipPacket.SourceAddress.Equals(keyValuePair.Value._srcIPAddress) && 
													_srcPort == keyValuePair.Value._srcPort) ||
													(_ipPacket.DestinationAddress.Equals(keyValuePair.Value._srcIPAddress) &&
													_dstPort == keyValuePair.Value._srcPort))))
												{
													foreach (KeyValuePair<int, NetOdysseyModule.clsFlow> keyValuePairWithin in keyValuePair.Value.FlowsDictionary)
													{
														if (_currPacket.Timeval.Date < keyValuePairWithin.Value.FlowTimeoutValue)
														{
															#region Unidirectional
															if (Program.prpSettings.FlowDirection == FlowDirection.Unidirectional)
															{
																if (keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._dstPort == _dstPort &&
																	(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
																	keyValuePairWithin.Value._transportProtocol.Equals(_transportProtocol) :
																	true))
																{
																	keyValuePairWithin.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
																	_exists = true;
																}
															}
															#endregion
															#region Bi-Directional
															else
															{
																if (((keyValuePairWithin.Value._srcIPAddress.Equals(_ipPacket.SourceAddress) &&
																	keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._dstPort == _dstPort) ||
																	(keyValuePairWithin.Value._srcIPAddress.Equals(_ipPacket.DestinationAddress) &&
																	keyValuePairWithin.Value._dstIPAddress.Equals(_ipPacket.SourceAddress) &&
																	keyValuePairWithin.Value._dstPort == _srcPort)) &&
																	(Program.prpSettings.DistinguishFlowsByTransportProtocol ?
																	keyValuePairWithin.Value._transportProtocol.Equals(_transportProtocol) :
																	true))
																{
																	keyValuePairWithin.Value.EnqueuePacket(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
																	_exists = true;
																}
															}
															#endregion
														}
														else
														{
															_dicAnalysisWindows[keyValuePair.Key].EnqueueFlow(keyValuePairWithin.Value);
															_keysToRemove.Enqueue(keyValuePairWithin.Key);
														}
													}
													foreach (int key in _keysToRemove)
														keyValuePair.Value.RemoveFlow(key);
													_keysToRemove.Clear();
													#region A Child Flow Does Not Exist Within a Traffic Subset
													if (!_exists)
													{
														_exists = true;
														keyValuePair.Value.AddFlow(
															new NetOdysseyModule.clsFlow(_ipPacket.SourceAddress,
																						_ipPacket.DestinationAddress,
																						_srcPort,
																						_dstPort,
																						_transportProtocol));
														keyValuePair.Value.EnqueuePacketToLastFlow(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
													}
													#endregion
													break;
												}
											}
										}
										#region A Traffic Subset Does Not Exist
										if (!_exists)
										{
											_exists = true;
											lock (_capturedPacketsFlowsLock)
											{
												_capturedfFlows++;
											}
											
											if (clsIPAddressRange.isInRange(_ipPacket.SourceAddress))
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.SourceAddress, _srcPort, true));
											}
											else
											{
												_dicTrafficSubsets.Add(_capturedfFlows,
													new NetOdysseyModule.clsTrafficSubset(_ipPacket.DestinationAddress, _dstPort, true));
											}

											NewAnalysisWindow();
											NewModulesList();

											_dicTrafficSubsets[_capturedfFlows].AddFlow(
												new NetOdysseyModule.clsFlow(_ipPacket.SourceAddress,
																				_ipPacket.DestinationAddress,
																				_srcPort,
																				_dstPort,
																				_transportProtocol));
											_dicTrafficSubsets[_capturedfFlows].EnqueuePacketToLastFlow(_currPacket, Convert.ToDouble(Program.prpSettings.FlowTimeout));
										}
										#endregion
										break;
										#endregion
								}
								break;
						}
						if (prpIsCaptureLive) {
							if (prpLiveDevice.DumpOpened) 
								prpLiveDevice.Dump(_capturedPacket);
						} else {
							if (prpOfflineDevice.DumpOpened)
								prpOfflineDevice.Dump(_capturedPacket);
						}
						lock (_capturedPacketsFlowsLock) {
							++_capturedPackets;
							if (_packets_StatisticsToCapture > 0 && _capturedPackets >= _packets_StatisticsToCapture) {
								_captureDuration = DateTime.Now - _captureStart;
								clsMessages.PrintAllDone();
								clsSettings._wh.Set();
								break;
							}
						}

					}
				} catch (ThreadAbortException) { } catch (Exception e) {
					clsMessages.PrintError("Capturer thread exception: " + e.Message);
					_healthMonitor.Stop();

					foreach (KeyValuePair<int, clsAnalysisWindow> keyValuePair in _dicAnalysisWindows)
						keyValuePair.Value.Stop();
				}
			}
			#endregion
			#region CaptureMode == Statistics
			else if (Program.prpSettings.CaptureMode == CaptureMode.Statistics) {
				prpLiveDevice.OnPcapStatistics += new SharpPcap.StatisticsModeEventHandler(prpLiveDevice_OnPcapStatistics);
				prpLiveDevice.Open(SharpPcap.DeviceMode.Promiscuous, (int) Program.prpSettings.BitCountPerTimeUnit);
				if (Program.prpSettings.TcpDumpFilter != "")
					prpLiveDevice.SetFilter(Program.prpSettings.TcpDumpFilter);
				prpLiveDevice.Mode = SharpPcap.CaptureMode.Statistics;
				prpLiveDevice.StartCapture();
			}
			#endregion
		}

		/// <summary>
		/// Adds a new analysis window to the analysis window dictionary and starts its inherent thread.
		/// </summary>
		void NewAnalysisWindow()
		{
			_dicAnalysisWindows.Add(_capturedfFlows, new clsAnalysisWindow(_capturedfFlows));
			_dicAnalysisWindows[_capturedfFlows].Start();
		}

		/// <summary>
		/// Adds a new list of modules to the modules dictionary and starts their inherent thread.
		/// </summary>
		void NewModulesList()
		{
			Program.prpModulesDictionary.Add(_capturedfFlows, new List<NetOdysseyModule.NetOdysseyModuleBase>()); 

			Type _moduleType = null;
			string _moduleName = null;
			string _directory = null;

			// The foreach loop does not guarantee sequencial and incremental order
			for (int i = 0; i < Program.prpModulesTypeList.Count; i++)
			{
				_moduleType = Program.prpModulesTypeList[i];
				_moduleName = Program.prpModulesNameList[i];

				NetOdysseyModule.NetOdysseyModuleBase moduleInstance = (NetOdysseyModule.NetOdysseyModuleBase) Activator.CreateInstance(_moduleType);

				Program.prpModulesDictionary[_capturedfFlows].Add(moduleInstance);
				if (Program.prpSettings.CreateAFolderPerFlow && 
					Program.prpSettings.AnalysisSettings != AnalysisSettings.AllTraffic && 
					Program.prpSettings.AnalysisSettings != AnalysisSettings.BitCountPerTimeUnit)
				{
					_directory = Program.prpSettings.ReportsFolder + "\\" + _capturedfFlows + "_" + _dicTrafficSubsets[_capturedfFlows]._srcIPAddress + "\\";
					if (!Directory.Exists(_directory))
					{
						Directory.CreateDirectory(_directory);
					}
					Program.prpModulesDictionary[_capturedfFlows][i].prpReportFolder = Program.prpSettings.ReportsFolder + "\\" + _capturedfFlows + "_" + _dicTrafficSubsets[_capturedfFlows]._srcIPAddress + "\\";
				}
				else
				{
					Program.prpModulesDictionary[_capturedfFlows][i].prpReportFolder = Program.prpSettings.ReportsFolder;
				}
				Program.prpModulesDictionary[_capturedfFlows][i].prpModuleName += _moduleName + ".flow" + _capturedfFlows;
				Program.prpModulesDictionary[_capturedfFlows][i].Start();
			}
		}

		ulong _oldSec = 0;
		ulong _oldUsec = 0;
		ulong _delay = 0;
		ulong _bps = 0;

		/// <summary>
		/// The SharpPcap statistics mode event handler for the statistics capture mode.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The SharpPcap statistics mode event arguments.</param>
		void prpLiveDevice_OnPcapStatistics(object sender, SharpPcap.StatisticsModeEventArgs e)
		{
			_delay = (e.Statistics.Timeval.Seconds - _oldSec) * _bctu * 1000 - _oldUsec + e.Statistics.Timeval.MicroSeconds;
			_bps = ((ulong) e.Statistics.RecievedBytes * 8 * _bctu * 1000) / _delay;

			_dicAnalysisWindows[0].Enqueue(_bps);

			_oldSec = e.Statistics.Timeval.Seconds;
			_oldUsec = e.Statistics.Timeval.MicroSeconds;
			lock (_generatedStatisticsLock) {
				++_generatedStatistics;                
				if (_packets_StatisticsToCapture > 0 && _generatedStatistics >= _packets_StatisticsToCapture) {                    
					_captureDuration = DateTime.Now - _captureStart;
					clsMessages.PrintAllDone();
					clsSettings._wh.Set();
					prpLiveDevice.StopCapture();
					//Stop();
				}
			}
		}

		#region Captured Flows
			// -1 by default
			int _capturedfFlows = -1;
			/// <summary>
			/// Gets the number of captured flows.
			/// </summary>
			[Browsable(false)]
			public int prpCapturedFlows 
			{
				get { return _capturedfFlows; }
			}
		#endregion
	}
}