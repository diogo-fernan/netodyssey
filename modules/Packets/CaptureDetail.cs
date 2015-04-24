using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace Capture
{
    class capture : NetOdysseyModuleBase, INetOdysseyModule {
        string _report = "";
        public override string ModuleStart()
        {
            DateTime now = DateTime.Now;
            string _moduleStart = "# Capture started at " + now.ToShortDateString() + " " + now.ToShortTimeString() + Environment.NewLine;
            Console.Write(_moduleStart);
            return _moduleStart;
        }

        public override string ModuleEnd()
        {
            DateTime now = DateTime.Now;
            string _moduleEnd = "# Capture ended at " + now.ToShortDateString() + " " + now.ToShortTimeString() + Environment.NewLine;
            Console.Write(_moduleEnd);
            return _moduleEnd;
        }

        public override void AnalyzePacketIn(SharpPcap.Packets.Packet Packet)
        {
            lock (_report)
                _report += Packet.PcapHeader.Seconds + "." + Packet.PcapHeader.MicroSeconds + ";" + Packet.PcapHeader.CaptureLength + Environment.NewLine;
            Console.WriteLine(Packet.PcapHeader.Seconds + "." + Packet.PcapHeader.MicroSeconds + ";" + Packet.PcapHeader.CaptureLength);
        }

        public override void AnalyzePacketOut(SharpPcap.Packets.Packet Packet)
        {            
        }

        public override void Clear()
        {
        }

        public override string ReportAnalysis()
        {
            string _return = "";
            lock (_report)
            {
                _return = _report;
                _report = "";
            }
            return _return;
        }
    }
}
