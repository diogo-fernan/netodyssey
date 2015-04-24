using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace EntropyOnTheFly
{
    class NetAnalyzerEntropyBCTUModule : NetOdysseyModuleBase, INetOdysseyBCTUAnalyzerModule {
        int _startPacket = 1;
        int _endPacket = 0;
        double _entropy = 0;

        Dictionary<int, int> _occurences = new Dictionary<int, int>();

        public override string ModuleStart()
        {
            //string _moduleStart = "startPacket;endPacket;lenghtEntropy" + Environment.NewLine;
            //Console.Write(_moduleStart);
            //Console.Write("Entropy on the fly started..");
            return "";
			//return _moduleStart;
        }

        public override string ModuleEnd()
        {
            //Console.Write("Entropy on the fly terminated.");
            return "";
        }

        public override void AnalyzeBCTUIn(ulong bctu, int WindowSize) {                       
            int _bctu = (int)bctu;
            double ws = (double)WindowSize; // make it a double, in order to avoid int division
            
            _endPacket++;            
            lock (_occurences)
            {
                if (_occurences.ContainsKey(_bctu)) {
                    // If this packet size already exists, remove its previous weight and add the new one to the entropy
                    _entropy -= (_occurences[_bctu] / ws) * Math.Log(ws / _occurences[_bctu]);
                    _occurences[_bctu]++;
                    _entropy += (_occurences[_bctu] / ws) * Math.Log(ws / _occurences[_bctu]);
                }
                else {
                    // If this packet size didn't exist already, add it to the entropy
                    _occurences.Add(_bctu, 1);
                    _entropy += (1 / ws) * Math.Log(ws);                    
                }
            }
        }

        public override void AnalyzeBCTUOut(ulong bctu, int WindowSize) {  
            int _bctu = (int)bctu;
            double ws = (double)WindowSize; // make it a double, in order to avoid int division
            
            _startPacket++;            
            lock (_occurences)
            {
                if (_occurences[_bctu] == 1) {
                    // If this is the last occurence of this packet size, remove it from the entropy
                    _occurences.Remove(_bctu);
                    _entropy -= (1 / ws) * Math.Log(ws);
                }
                else { 
                    // If this packet size still exists in the window, update its value
                    _entropy -= (_occurences[_bctu] / ws) * Math.Log(ws / _occurences[_bctu]);
                    _occurences[_bctu]--;
                    _entropy += (_occurences[_bctu] / ws) * Math.Log(ws / _occurences[_bctu]);
                }
            }
        }

        public override void Clear() {
            _entropy = 0;
            lock (_occurences)
            {
                _occurences.Clear();
            }
        }

        public override string ReportAnalysis() {
            string _report;                        
            
            //_report = _startPacket + ";" + _endPacket + ";" + _entropy + Environment.NewLine;
			//_report = _endPacket + "	" + _entropy + Environment.NewLine; // for gnuplot
			_report = _entropy + ","; // for R
            //Console.Write(_report);
            return _report;
        }
  }  
}