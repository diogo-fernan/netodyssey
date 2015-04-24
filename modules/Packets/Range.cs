using System;
using System.Threading;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Range
{
    class NetAnalyzerRangePacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {
        int _packetLength;
        int _max = 0;
        int _min = 65536;
        List<int> _packets = new List<int>();

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Range Started" + Environment.NewLine;			
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Range Ended";
        }

        /// <summary>
        /// This method is invoked when a packet enters the analysis window.
        /// </summary>
        /// <param name="Packet">The packet to be analyzed.</param>
        /// <param name="WindowSize">The size of the analysis window.</param>
        public override void AnalyzePacketIn(PacketDotNet.Packet Packet,
											 int WindowSize) 
        {
            _packetLength = Packet.BytesHighPerformance.Length;
            if (_packetLength < _min)
                _min = _packetLength;
            if (_packetLength > _max)
                _max = _packetLength;
        }

        /// <summary>
        /// This method is invoked when a packet leaves the analysis window.
        /// </summary>
        /// <param name="Packet">The packet to be analyzed.</param>
        /// <param name="WindowSize">The size of the analysis window.</param>
        public override void AnalyzePacketOut(PacketDotNet.Packet Packet,
											  int WindowSize) 
        {
            _packetLength = Packet.BytesHighPerformance.Length;
            _packets.Remove(_packetLength);
            if (_packetLength == _max)
                _max = 0;
            if (_packetLength == _min)
                _min = 65536;
            foreach (int _size in _packets)
            {
                if (_size > _max)
                    _max = _size;
                if (_size < _min)
                    _min = _size;
            }
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear() 
        {
			_packets.Clear();
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis() 
        {
            return (_max - _min) + Environment.NewLine;
        }
	}  
}