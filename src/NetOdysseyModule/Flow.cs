using System;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace NetOdysseyModule
{
	public class Flow
	{
		protected uint _flowSizeInBytes;
		protected uint _flowSizeInPackets;
		protected uint _minimumSize;
		protected uint _maximumSize;
		protected TimeSpan _flowDuration;

		public IPAddress _srcIPAddress;
		public IPAddress _dstIPAddress;
		public ushort _srcPort;
		public ushort _dstPort;
		public TransportProtocol _transportProtocol;

		/// <summary>
		/// Gets the flow size in bytes.
		/// </summary>
		public uint SizeInBytes
		{
			get { return _flowSizeInBytes; }
		}

		/// <summary>
		/// Gets the flow size in packets.
		/// </summary>
		public uint SizeInPackets
		{
			get { return _flowSizeInPackets; }
		}

		/// <summary>
		/// Gets the maximum packet size of the flow.
		/// </summary>
		public uint MaximumSize
		{
			get { return _maximumSize; }
		}

		/// <summary>
		/// Gets the minimum packet size of the flow.
		/// </summary>
		public uint MinimumSize
		{
			get { return _minimumSize; }
		}

		/// <summary>
		/// Gets the flow duration of the flow.
		/// </summary>
		public TimeSpan FlowDuration
		{
			get { return _flowDuration; }
		}

		/// <summary>
		/// Gets the average of the sizes of the packets of the flow.
		/// </summary>
		public double AverageOfSizesOfPackets
		{
			get { return (double) (_flowSizeInBytes / _flowSizeInPackets); }
		}

		/// <summary>
		/// Gets the range of the flow. This is equivalent to the maximum packet size minus the minimum packet size of the flow.
		/// </summary>
		public uint Range
		{
			get { return _maximumSize - _minimumSize; }
		}
	}

	/// <summary>
	/// The transport protocol enumerator.
	/// </summary>
	public enum TransportProtocol
	{
		TCP,
		UDP
	}
}