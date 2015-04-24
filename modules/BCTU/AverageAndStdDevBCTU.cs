using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace AverageAndStdDev
{
    class NetOdysseyAvgStdDevBCTUModule : NetOdysseyModuleBase, INetOdysseyBCTUAnalyzerModule {
        int _startPacket = 1;
        int _endPacket = 0;
        int _currentCount = 0;
        int _sum = 0;
        int _sumOfSquares = 0;


        public override string ModuleStart()
        {
            string _moduleStart = "startPacket;endPacket;average;stdDev" + Environment.NewLine;
            //Console.Write("AverageAndSigma: " + _moduleStart);
            //return _moduleStart;
			return ""; // for gnuplot
        }

        public override string ModuleEnd()
        {
            return "";
        }

        public override void AnalyzeBCTUIn(ulong bctu, int WindowSize) {   
            int _bctu = (int)bctu;

            _endPacket++;
            _currentCount++;
            _sum += _bctu;
            _sumOfSquares += _bctu * _bctu;            
        }

        public override void AnalyzeBCTUOut(ulong bctu, int WindowSize) {   
            int _bctu = (int)bctu;

            _startPacket++;
            _currentCount--;
            _sum -= _bctu;
            _sumOfSquares -= _bctu * _bctu;
        }

        public override void Clear() {
            _currentCount = 0;
            _sum = 0;
            _sumOfSquares = 0;
        }

        public override string ReportAnalysis() {
            string _report;
            double _average = 0;
            double _sigma = 0;
            if (_currentCount > 0) 
                _average = (double)_sum / _currentCount;
            if (_currentCount > 1)
                _sigma = Math.Sqrt((_sumOfSquares / _currentCount) - Math.Pow(_average, 2));

            //_report = _startPacket + ";" + _endPacket + ";" + _average + ";" + _sigma + Environment.NewLine;
			//_report = _endPacket + "	" + _average + "	" + _sigma + Environment.NewLine; // for gnuplot
			_report = _average + "," + _sigma + Environment.NewLine; // for R
            //Console.Write("AverageAndSigma: " + _report);
            return _report;
        }
  }  
}