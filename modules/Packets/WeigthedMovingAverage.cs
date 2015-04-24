using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace WeightedMovingAverage
{
    class NetOdysseyWeightedMovingAveragePacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {     
		int _packetLength;		
        int _currentCount = 0;
        int _denominator = 0;
        int _sequencialWeigth = 1;
        int _lastPacket;
        double _sum = 0.0;
        double _weightedMovingAverage = 0.0;

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Weighted Moving Average Started" + Environment.NewLine;            
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Weighted Moving Average Ended";
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
            _currentCount++;
            if (_currentCount > WindowSize)
            {
                _weightedMovingAverage = _weightedMovingAverage - 
                    (double)(_sum / _denominator) +
                    ((_packetLength * (double)WindowSize) / _denominator);
                _sum = _sum - _lastPacket + _packetLength;
            }
            else if (_currentCount == WindowSize)
            {
                _sum += _packetLength;
                for (int i = 1; i <= WindowSize; i++)
                {
                    _denominator += i;
                }
                _weightedMovingAverage += _packetLength * WindowSize;
                _weightedMovingAverage = _weightedMovingAverage / _denominator;
            }
            else if (_currentCount < WindowSize)
            {
                _sum += _packetLength;
                _weightedMovingAverage += _packetLength * _sequencialWeigth;
                _sequencialWeigth++;
            }
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
            _lastPacket = _packetLength;
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear() 
        {
            _sequencialWeigth = 0;
            _denominator = 0;
            _weightedMovingAverage = 0.0;
            _sum = 0.0;
            _currentCount = 0;
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis() 
        {
            return _weightedMovingAverage + Environment.NewLine;
        }
	}  
}