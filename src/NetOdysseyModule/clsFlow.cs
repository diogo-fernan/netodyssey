using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ComponentModel;

namespace NetOdysseyModule
{
	public class clsFlow : Flow
	{
		private Queue<PacketDotNet.Packet> _packetsQueue;

		private PacketDotNet.Packet _lastPacket;

		private bool _flowEmpty;
		private DateTime _flowTimeoutValue;

		/// <summary>
		/// Constructor method. Used to instantiate a new flow.
		/// </summary>
		/// <param name="inSrcIPAddress">The source IP address of the flow.</param>
		/// <param name="inDstIPAddress">The destination IP address of the flow.</param>
		/// <param name="inSrcPort">The source port of the flow.</param>
		/// <param name="inDstPort">The source port of the flow.</param>
		/// <param name="inTransportProtocol">The transport protocol of the flow.</param>
		public clsFlow(IPAddress inSrcIPAddress, IPAddress inDstIPAddress, ushort inSrcPort, ushort inDstPort, TransportProtocol inTransportProtocol)
		{
			_flowSizeInBytes = 0;
			_flowSizeInPackets = 0;
			_minimumSize = 65535;
			_maximumSize = 0;

			_srcIPAddress = inSrcIPAddress;
			_dstIPAddress = inDstIPAddress;
			_srcPort = inSrcPort;
			_dstPort = inDstPort;
			_transportProtocol = inTransportProtocol;

			_flowDuration = new TimeSpan();

			_packetsQueue = new Queue<PacketDotNet.Packet>();
		}

		/// <summary>
		/// Enqueues a packet to the flow.
		/// </summary>
		/// <param name="inPacket">The packet to enqueue.</param>
		public void EnqueuePacket(PacketDotNet.Packet inPacket, double inFlowTimeout)
		{
			_packetsQueue.Enqueue(inPacket);

			uint _packetLength = (uint) inPacket.BytesHighPerformance.Length;

			_flowSizeInBytes += _packetLength;
			_flowSizeInPackets++;

			if (_packetLength > _maximumSize)
				_maximumSize = _packetLength;
			else if (_packetLength < _minimumSize)
				_minimumSize = _packetLength;

			_flowEmpty = false;

			if (_flowSizeInPackets > 1)
			{
				_flowDuration.Add(inPacket.Timeval.Date - _lastPacket.Timeval.Date);
			}
			_flowTimeoutValue = inPacket.Timeval.Date.AddSeconds(inFlowTimeout);

			_lastPacket = inPacket;
		}

		/// <summary>
		/// Dequeues and returns a packet from the flow.
		/// </summary>
		/// <returns></returns>
		public PacketDotNet.Packet DequeuePacket()
		{
			if (_packetsQueue.Count == 1)
				_flowEmpty = true;	
			return _packetsQueue.Dequeue();
		}

		/// <summary>
		/// Gets true if the flow is empty, false otherwise.
		/// </summary>
		public bool isEmpty
		{
			get { return _flowEmpty; }
		}

		/// <summary>
		/// Gets the timeout value of the flow.
		/// </summary>
		public DateTime FlowTimeoutValue
		{
			get { return _flowTimeoutValue; }
		}
	}
}
