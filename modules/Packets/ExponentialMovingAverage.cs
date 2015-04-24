using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace ExponentialMovingAverage
{
    class NetOdysseyExponentialMovingAveragePacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {
		int _packetLength;		
        int _currentCount = 0;
		int _weight = 1;						
        double _sum = 0.0;
		double _exponentialMovingAverage = 0.0;

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Exponential Moving Average Started" + Environment.NewLine;
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Exponential Moving Average Ended";
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
			_sum += _packetLength;
            _currentCount++;

			if(_currentCount > WindowSize)
				_exponentialMovingAverage = 
                    ((_exponentialMovingAverage * (WindowSize - _weight)) / WindowSize)
                    + ((_packetLength / WindowSize) * _weight);
			else if (_currentCount == WindowSize)
				_exponentialMovingAverage = _sum / WindowSize;
        }

        /// <summary>
        /// This method is invoked when a packet leaves the analysis window.
        /// </summary>
        /// <param name="Packet">The packet to be analyzed.</param>
        /// <param name="WindowSize">The size of the analysis window.</param>
        public override void AnalyzePacketOut(PacketDotNet.Packet Packet,
											  int WindowSize) 
        {
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear() 
        {
			_exponentialMovingAverage = 0.0;
            _sum = 0.0;
            _currentCount = 0;
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis() 
        {
			return _exponentialMovingAverage + Environment.NewLine;
        }
	}  
}