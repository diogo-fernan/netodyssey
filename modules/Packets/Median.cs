using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Median
{
    class NetAnalyzerMedianPacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {
		int _packetLength;
        List<int> _occurrences = new List<int>();

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Median Started" + Environment.NewLine;			
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Median Ended";
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
            _occurrences.Add(_packetLength);
            _occurrences.Sort(delegate(int a, int b) { return a.CompareTo(b); });
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
            _occurrences.Remove(_packetLength);
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear()
        {
            _occurrences.Clear();
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis() 
        {
            int _median;

            if ((_occurrences.Count % 2) == 0)
                _median = (_occurrences[_occurrences.Count / 2] + 
                    _occurrences[(_occurrences.Count / 2) + 1]) / 2;
            else
                _median = _occurrences[_occurrences.Count / 2];
           
            return _median + Environment.NewLine;
        }
	}  
}