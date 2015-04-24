using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Mode
{
    class NetAnalyzerModePacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {
		int _packetLength;
        Dictionary<int, int> _occurences = new Dictionary<int, int>();

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Mode Started" + Environment.NewLine;			
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {            
            return "Mode Ended";
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
            if (_occurences.ContainsKey(_packetLength))
                _occurences[_packetLength]++;
            else
                _occurences.Add(_packetLength, 1);  
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
            if (_occurences[_packetLength] == 1)
                _occurences.Remove(_packetLength);
            else
                _occurences[_packetLength]--;
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear() 
        {
			_occurences.Clear();
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis() 
        {
            List<int> _modes = new List<int>();
            int _minimum = 65536;
            int _maximum = 0;

			foreach (KeyValuePair<int, int> _keyValuePair in _occurences)
                if (_keyValuePair.Value > _maximum)
                    _maximum = _keyValuePair.Value;

            foreach (KeyValuePair<int, int> _keyValuePair in _occurences)
                if (_keyValuePair.Value == _maximum)
                    _modes.Add(_keyValuePair.Key);

            foreach (int mode in _modes)
                if (mode < _minimum)
                    _minimum = mode;

            return _minimum + Environment.NewLine;
        }
	}  
}