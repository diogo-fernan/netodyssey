using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace EntropyOnTheFly
{
    class NetAnalyzerEntropyPacketsModule : 
		  NetOdysseyModuleBase, INetOdysseyPacketAnalyzerModule {
        
		int PacketLength;
		double _ws;
		double _entropy = 0;

        Dictionary<int, int> _occurences = new Dictionary<int, int>();

        public override string ModuleStart()
        {
            return "entropy" + Environment.NewLine;			
        }

        public override string ModuleEnd()
        {            
            return "";
        }

        public override void AnalyzePacketIn(PacketDotNet.Packet Packet,
											 int WindowSize) {                        
            PacketLength = Packet.BytesHighPerformance.Length;
            _ws = (double)WindowSize;
            lock (_occurences)
            {
                if (_occurences.ContainsKey(PacketLength)) {
                    // If this packet size already exists, 
					// remove its previous weight and add 
					// the new one to the entropy
                    _entropy -= (_occurences[PacketLength] / _ws) * 
								Math.Log(_ws / _occurences[PacketLength]);
                    _occurences[PacketLength]++;
                    _entropy += (_occurences[PacketLength] / _ws) * 
								Math.Log(_ws / _occurences[PacketLength]);
                }
                else {
                    // If this packet size didn't exist already,
					// add it to the entropy
                    _occurences.Add(PacketLength, 1);
                    _entropy += (1 / _ws) * Math.Log(_ws);                    
                }
            }
        }

        public override void AnalyzePacketOut(PacketDotNet.Packet Packet,
											  int WindowSize) {
            PacketLength = Packet.BytesHighPerformance.Length;
            _ws = (double)WindowSize;            
            lock (_occurences)
            {
                if (_occurences[PacketLength] == 1) {
                    // If this is the last occurence of this packet size,
					// remove it from the entropy
                    _occurences.Remove(PacketLength);
                    _entropy -= (1 / _ws) * Math.Log(_ws);
                }
                else { 
                    // If this packet size still exists in the window,
					// update its value
                    _entropy -= (_occurences[PacketLength] / _ws) * 
								Math.Log(_ws / _occurences[PacketLength]);
                    _occurences[PacketLength]--;
                    _entropy += (_occurences[PacketLength] / _ws) * 
								Math.Log(_ws / _occurences[PacketLength]);
                }
            }
        }

        public override void Clear() {
            _entropy = 0;
            lock (_occurences)
				_occurences.Clear();
        }

        public override string ReportAnalysis() {            
            return _entropy + ";" + Environment.NewLine;
        }
	}  
}