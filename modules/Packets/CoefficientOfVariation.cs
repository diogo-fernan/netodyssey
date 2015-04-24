using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace CoefficientofVariation
{
	class NetOdysseyCoefficientofVariationPacketsModule :
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule
	{
		int _packetLength;
		int _currentCount = 0;
		double _sum = 0.0;
		double _sumOfSquares = 0.0;
		double _average = 0.0;
		double _sigma = 0.0;

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
		public override string ModuleStart()
		{
			return "Coefficient of Variation Started" + Environment.NewLine;
		}

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
		public override string ModuleEnd()
		{
            return "Coefficient of Variation Ended";
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
		}

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
		public override void Clear()
		{
			_currentCount = 0;
			_sum = 0;
			_sumOfSquares = 0;
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
				
			return (_sigma / _average) + Environment.NewLine;
		}
	}
}