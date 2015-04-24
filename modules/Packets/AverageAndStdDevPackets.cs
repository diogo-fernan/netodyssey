using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace AverageAndStdDev
{
    class NetOdysseyAvgStdDevPacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule {
        
		int _packetLenght;		
        int _currentCount = 0;
        double _sum = 0;
        double _sumOfSquares = 0;
		double _average = 0;
        double _sigma = 0;

        public override string ModuleStart()
        {
            return "average; stdDev" + Environment.NewLine;            
        }

        public override string ModuleEnd()
        {
            return "";
        }

        public override void AnalyzePacketIn(PacketDotNet.Packet Packet,
											 int WindowSize) {
            _packetLenght = Packet.BytesHighPerformance.Length;            
            _currentCount++;
            _sum += _packetLenght;
            _sumOfSquares += _packetLenght * _packetLenght;            
        }

        public override void AnalyzePacketOut(PacketDotNet.Packet Packet,
											  int WindowSize) {
            _packetLenght = Packet.BytesHighPerformance.Length;
            _currentCount--;
            _sum -= _packetLenght;
            _sumOfSquares -= _packetLenght * _packetLenght;
        }

        public override void Clear() {
            _currentCount = 0;
            _sum = 0;
            _sumOfSquares = 0;
        }

        public override string ReportAnalysis() {                        
            if (_currentCount > 0) 
                _average = _sum / _currentCount;
			else
				_average = 0;
				
            if (_currentCount > 1)
                _sigma = Math.Sqrt(
					(_sumOfSquares / _currentCount) - 
					(_average * _average)
				);
			else
				_sigma = 0;			
            return _average + "; " + _sigma + Environment.NewLine;
        }
	}  
}