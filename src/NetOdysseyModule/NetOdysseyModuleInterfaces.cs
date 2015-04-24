using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOdysseyModule
{
	public interface INetOdysseyFlowAnalyzerModule
	{
		string ModuleStart();
		string ModuleEnd();
		void AnalyzeFlowIn(Flow Flow, int WindowSize);
		void AnalyzeFlowOut(Flow Flow, int WindowSize);
		void Clear();
		string ReportAnalysis();
	}

	public interface INetOdysseyPacketAnalyzerModule
	{
		string ModuleStart();
		string ModuleEnd();
		void AnalyzePacketIn(PacketDotNet.Packet Packet, int WindowSize);        
		void AnalyzePacketOut(PacketDotNet.Packet Packet, int WindowSize);
		void Clear();
		string ReportAnalysis();        
	}

	public interface INetOdysseyBCTUAnalyzerModule
	{
		string ModuleStart();
		string ModuleEnd();
		void AnalyzeBCTUIn(ulong BCTU, int WindowSize);
		void AnalyzeBCTUOut(ulong BCTU, int WindowSize);
		void Clear();
		string ReportAnalysis();
	}
}
