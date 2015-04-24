using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Capture
{
    class capture : NetOdysseyModuleBase, INetOdysseyModule {
        uint _startPacket = 1;
        uint _endPacket = 0;
        uint _tcpCount = 0;
        uint _udpCount = 0;
        uint _icmpCount = 0;
        uint _unknownCount = 0;

        public override string ModuleStart()
        {
            string _moduleStart = "startPacket;endPacket;tcpCount;udpCount;icmpCount;unknownCount" + Environment.NewLine;
            //Console.Write("TcpUdpIcmpCount: " + _moduleStart);
            return _moduleStart;
        }

        public override string ModuleEnd()
        {
            return "";
        }

        public override void AnalyzePacketIn(SharpPcap.Packets.Packet Packet, int WindowSize)
        {
            _endPacket++;

            Console.WriteLine("_endPacket: " + _endPacket + " WindowSize: " + WindowSize) ;

            if (Packet is SharpPcap.Packets.TCPPacket)
                _tcpCount++;
            else if (Packet is SharpPcap.Packets.UDPPacket)
                _udpCount++;
            else if (Packet is SharpPcap.Packets.ICMPPacket)
                _icmpCount++;
            else
                _unknownCount++;
        }

        public override void AnalyzePacketOut(SharpPcap.Packets.Packet Packet, int WindowSize)
        {
            _startPacket++;

            if (Packet is SharpPcap.Packets.TCPPacket)
                _tcpCount--;
            else if (Packet is SharpPcap.Packets.UDPPacket)
                _udpCount--;
            else if (Packet is SharpPcap.Packets.ICMPPacket)
                _icmpCount--;
            else
                _unknownCount--;
        }

        public override void Clear()
        {
            _tcpCount = 0;
            _udpCount = 0;
            _icmpCount = 0;
            _unknownCount = 0;
        }

        public override string ReportAnalysis()
        {            
            string _report = _startPacket + ";" + _endPacket + ";" + _tcpCount + ";" + _udpCount + ";" + _icmpCount + ";" + _unknownCount + Environment.NewLine;
            //Console.Write("TcpUdpIcmpCount: " + _report);
            return _report;
        }
    }
}
