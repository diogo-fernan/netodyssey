using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace FlowAverage
{
	class NetOdysseyFlowAvgModule :
		  NetOdysseyModuleBase, INetOdysseyFlowAnalyzerModule
	{
		uint _flowLenght;
		int _currentCount = 0;
		double _sum = 0;
		double _average = 0;

        /// <summary>
        /// The first method invoked on a module.
        /// </summary>
        /// <returns>A string printed in the first line of the report file.</returns>
		public override string ModuleStart()
		{
			return "Flow Average Started" + Environment.NewLine;
		}

        /// <summary>
        /// The last method invoked on a module.
        /// </summary>
        /// <returns>A string printed in the last line of the report file.</returns>
		public override string ModuleEnd()
		{
			return "Flow Average Ended";
		}

        /// <summary>
        /// The method invoked when analyzing an flow entering the analysis window.
        /// </summary>
        /// <param name="inFlow">The flow to analyze.</param>
        /// <param name="WindowSize">The size of the window.</param>
		public override void AnalyzeFlowIn(Flow inFlow,
											 int WindowSize)
		{
            _flowLenght = inFlow.SizeInBytes;
			_currentCount++;
			_sum += (double) _flowLenght;
		}

        /// <summary>
        /// The method invoked when analyzing a flow leaving the analysis window.
        /// </summary>
        /// <param name="inFlow">The flow to analyze.</param>
        /// <param name="WindowSize">The size of the window.</param>
        public override void AnalyzeFlowOut(Flow inFlow,
											  int WindowSize)
		{
			_flowLenght = inFlow.SizeInBytes;
			_currentCount--;
			_sum -= (double) _flowLenght;
		}

        /// <summary>
        /// The method invoked when using the Analysis Window Time (AWT) used to clear all variables.
        /// </summary>
		public override void Clear()
		{
			_currentCount = 0;
			_sum = 0;
		}

        /// <summary>
        /// The method invoked when a report is needed to be produced.
        /// </summary>
        /// <returns>A string containing the results of the observation.</returns>
		public override string ReportAnalysis()
		{
			if (_currentCount > 0)
				_average = _sum / _currentCount;
			else
				_average = 0;
				
			return _average + Environment.NewLine;
		}
	}
}