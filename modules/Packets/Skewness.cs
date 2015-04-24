using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Skewness
{
    class NetOdysseySkewnessPacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule 
    {
		int _packetLength;		
        int _currentCount = 0;
        double _sum = 0.0;
        double _sumOfCubes = 0.0;
        double _sumOfSquares = 0.0;
        double _average = 0.0;
        double _sigma = 0.0;
		double _skewness = 0.0;

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Skewness Started" + Environment.NewLine;            
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Skewness Ended";
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
            _sum += _packetLength;
            _sumOfSquares += _packetLength * _packetLength;
            _sumOfCubes += Math.Pow(_packetLength, 3);
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
            _currentCount--;
            _sum -= _packetLength;
            _sumOfSquares -= _packetLength * _packetLength;
            _sumOfCubes -= Math.Pow(_packetLength, 3);
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear() 
        {
            _currentCount = 0;
            _sum = 0.0;
            _sumOfSquares = 0.0;
            _sumOfCubes = 0.0;
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis()
        {
            _average = _sum / _currentCount;
            _sigma = Math.Sqrt(
                            (_sumOfSquares / _currentCount) -
                            (_average * _average)
                            );

            if (_sigma == 0)
                return 0.0 + Environment.NewLine;
           
            _skewness = (_sumOfCubes - 
                        (3 * _average * _sumOfSquares) + 
                        (3 * Math.Pow(_average, 2) * _sum) - 
                        (_currentCount * Math.Pow(_average, 3))) / 
                        (Math.Pow(_sigma, 3) * _currentCount);
           
            return _skewness + Environment.NewLine;
        }
	}  
}