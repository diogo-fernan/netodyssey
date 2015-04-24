using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NetOdysseyModule
{
	public class clsTrafficSubset
	{
		public IPAddress _srcIPAddress;
		public ushort _srcPort;

		private int _flowKey = -1;
		private Dictionary<int, clsFlow> _flowsDictionary;

		/// <summary>
		/// Constructor method. Used to instantiate a new traffic subset to the PerSourceIP.
		/// </summary>
		/// <param name="inSrcIPAddress">The source IP address of the traffic subset.</param>
		/// <param name="inFlow">An input flag argument to distinguish between the PacketByPacket and IntraFlow from the InterFlow.</param>
		public clsTrafficSubset(IPAddress inSrcIPAddress, bool inFlow)
		{
			_srcIPAddress = inSrcIPAddress;

			if (inFlow)
			{
				_flowsDictionary = new Dictionary<int, clsFlow>();
			}
		}

		/// <summary>
		/// Constructor method. Used to instantiate a new traffic subset to the PerApplication.
		/// </summary>
		/// <param name="inSrcIPAddress">The source IP address of the traffic subset.</param>
		/// <param name="inSrcPort">The source port of the traffic subset.</param>
		public clsTrafficSubset(IPAddress inSrcIPAddress, ushort inSrcPort, bool inFlow)
		{
			_srcIPAddress = inSrcIPAddress;
			_srcPort = inSrcPort;

			if (inFlow)
			{
				_flowsDictionary = new Dictionary<int, clsFlow>();
			}
		}

		/// <summary>
		/// Adds a new flow to the flow dictionary of this traffic subset.
		/// </summary>
		/// <param name="inFlow">The flow to add to the flow dictionary.</param>
		public void AddFlow(clsFlow inFlow)
		{
			_flowsDictionary.Add(++_flowKey, inFlow);
		}

		/// <summary>
		/// Removes a flow from the dictionary that is associated with a key of this traffic subset.
		/// </summary>
		/// <param name="inFlowKey">The key associated with the flow to remove.</param>
		public void RemoveFlow(int inFlowKey)
		{
			_flowsDictionary.Remove(inFlowKey);
		}

		/// <summary>
		/// Enqueues a packet to the last flow added to the flows dictionary of this traffic subset.
		/// </summary>
		/// <param name="inPacket">The packet to enqueue to the last flow.</param>
		/// <param name="inFlowTimeout">The flow timeout value.</param>
		public void EnqueuePacketToLastFlow(PacketDotNet.Packet inPacket, double inFlowTimeout)
		{
			_flowsDictionary[_flowKey].EnqueuePacket(inPacket, inFlowTimeout);
		}

		/// <summary>
		/// Gets the flow dictionary of this traffic subset.
		/// </summary>
		public Dictionary<int, clsFlow> FlowsDictionary
		{
			get { return _flowsDictionary; }
		}
	}
}
