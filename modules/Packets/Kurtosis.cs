using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Kurtosis
{
    class NetOdysseyKurtosisModule :
          NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule
    {
        int _packetLength;
        int _currentCount = 0;
        double _sum = 0.0;
        double _average = 0.0;
        double _sumOfSquares = 0.0;
        double _sumOfCubes = 0.0;
        double _sumOf4 = 0.0;
        double _kurtosisnumerator = 0.0;
        double _kurtosisdenominator = 0.0;
        double _kurtosis = 0.0;

        /// <summary>
        /// This method is invoked when the analysis starts.
        /// </summary>
        /// <returns>A string printed to the begin of the report file.</returns>
        public override string ModuleStart()
        {
            return "Kurtosis Started" + Environment.NewLine;
        }

        /// <summary>
        /// This method is invoked when the analysis ends.
        /// </summary>
        /// <returns>A string printed to the end of the report file.</returns>
        public override string ModuleEnd()
        {
            return "Kurtosis Ended";
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
            _sumOfSquares += Math.Pow(_packetLength, 2);
            _sumOfCubes += Math.Pow(_packetLength, 3);
            _sumOf4 += Math.Pow(_packetLength, 4);
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
            _sumOfSquares -= Math.Pow(_packetLength, 2);
            _sumOfCubes -= Math.Pow(_packetLength, 3);
            _sumOf4 -= Math.Pow(_packetLength, 4);
        }

        /// <summary>
        /// This method is invoked by the Analysis Window Time (AWT).
        /// </summary>
        public override void Clear()
        {
            _currentCount = 0;
            _sum = 0.0;
            _average = 0.0;
            _sumOfSquares = 0.0;
            _sumOfCubes = 0.0;
            _sumOf4 = 0.0;
        }

        /// <summary>
        /// This method is invoked when a report is needed to be conceived.
        /// </summary>
        /// <returns>A string containing the results of the module.</returns>
        public override string ReportAnalysis()
        {   
            _average = _sum / _currentCount;
            _kurtosisnumerator = (_sumOf4 - 
                4 * _sumOfCubes * _average +
                6 * _sumOfSquares * Math.Pow(_average, 2) - 
                4 * _sum * Math.Pow(_average, 3) + 
                Math.Pow(_average, 4) * _currentCount) / _currentCount;
            _kurtosisdenominator = (_sumOfSquares -
                2 * _sum * _average +
                Math.Pow(_average, 2) * _currentCount) / _currentCount;
            _kurtosis = (_kurtosisnumerator /  Math.Pow(_kurtosisdenominator, 2)) - 3;

            return _kurtosis + Environment.NewLine;
        }
    }
}