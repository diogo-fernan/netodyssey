using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace AutoCorrelations
{
    class NetOdysseyACFBCTUModule : NetOdysseyModuleBase, INetOdysseyBCTUAnalyzerModule
    {
        int _Kcount; // check method ModuleStart for K generation
        List<int> _Ks = new List<int>();

        List<Queue<int>> _awPackets = new List<Queue<int>>();
        List<Queue<int>> _XiPackets = new List<Queue<int>>();
        List<Queue<int>> _nextKPackets = new List<Queue<int>>();
        List<List<int>> _XikPackets = new List<List<int>>();
        List<Queue<int>> _XiXikProducts = new List<Queue<int>>();

        int _bctu = 0;
        int _startPacket = 1;
        int _endPacket = 0;
        int _currentCount = 0;
        int _K = 0;

        List<int> _XSum = new List<int>();
        List<int> _XiSum = new List<int>();
        List<int> _XiXikSum = new List<int>();
        List<int> _XiXikProductsSum = new List<int>();
        List<int> _XikSum = new List<int>();
        List<int> _XiXikProduct = new List<int>();
        List<int> _XSquareSum = new List<int>();
        List<int> _aux = new List<int>();

        List<double> _XMean = new List<double>();
        List<double> _EX2 = new List<double>();
        List<double> _VarX = new List<double>();
        List<double> _ac = new List<double>();

        bool _errFlag = false;
        string _report;

        public override string ModuleStart()
        {
            string _Kstring = "";
            for (_K = 2; _K <= 2000; _K *= 2)
            {
                _Ks.Add(_K);
                _Kstring += _K + ",";
            }
            _Kstring = _Kstring.Remove(_Kstring.Length-1);

            _Kcount = _Ks.Count;

            for (_K = 0; _K < _Kcount; _K++)
            {                
                _awPackets.Add(new Queue<int>());
                _XiPackets.Add(new Queue<int>());
                _nextKPackets.Add(new Queue<int>());
                _XikPackets.Add(new List<int>());
                _XiXikProducts.Add(new Queue<int>());

                _XSum.Add(0);
                _XiSum.Add(0);
                _XiXikSum.Add(0);
                _XiXikProductsSum.Add(0);
                _XikSum.Add(0);
                _XiXikProduct.Add(0);
                _XSquareSum.Add(0);
                _aux.Add(0);

                _XMean.Add(0);
                _EX2.Add(0);
                _VarX.Add(0);
                _ac.Add(0);
            }
            Console.WriteLine("Hurst _Ks=[{0}] (total:{1}) started", _Kstring , _Kcount);
            return "";
        }

        public override string ModuleEnd()
        {
            return "";
        }

        public override void AnalyzeBCTUIn(ulong bctu, int WindowSize) 
        {
            _currentCount++;
            _endPacket++;

            if (_Ks[_Kcount - 1] >= WindowSize)
            {
                _errFlag = true;
                return;
            }

            _bctu = (int)bctu;
            for (_K = 0; _K < _Kcount; _K++)
            {
                //Console.WriteLine("here1.1 _K={0}", _K);
                _awPackets[_K].Enqueue(_bctu);
                //Console.WriteLine("here1.2 _K={0}", _K);
                _XSum[_K] += _bctu;
                //Console.WriteLine("here1.3 _K={0}", _K);
                _XSquareSum[_K] += _bctu * _bctu;
                //Console.WriteLine("here1.4 _K={0}", _K);
                if (_currentCount <= WindowSize - _Ks[_K])
                {
                    //Console.WriteLine("here2.1 _K={0}", _K);
                    _XiPackets[_K].Enqueue(_bctu);
                    //Console.WriteLine("here2.2 _K={0}", _K);
                    _XiSum[_K] += _bctu;
                    //Console.WriteLine("here2.3 _K={0}", _K);
                }
                else
                {                    
                    _nextKPackets[_K].Enqueue(_bctu);
                }
                if (_currentCount > _Ks[_K])
                {                    
                    _XikPackets[_K].Add(_bctu);
                    _XikSum[_K] += _bctu;

                    _XiXikProduct[_K] = _awPackets[_K].Dequeue() * _bctu;
                    _XiXikProductsSum[_K] += _XiXikProduct[_K];
                    _XiXikProducts[_K].Enqueue(_XiXikProduct[_K]);
                }                
            }
        }

        public override void AnalyzeBCTUOut(ulong bctu, int WindowSize)
        {
            _bctu = (int)bctu;
            _startPacket++;
            _currentCount--;

            if (_errFlag) return;

            // We assume that the whole analysis window is full (this is the expected behavior)            

            for (_K = 0; _K < _Kcount; _K++)
            {                
                _XSum[_K] -= _bctu;
                _XSquareSum[_K] -= _bctu * _bctu;

                _aux[_K] = _XiPackets[_K].Dequeue();
                _XiSum[_K] -= _aux[_K];

                _aux[_K] = _nextKPackets[_K].Dequeue();
                _XiPackets[_K].Enqueue(_aux[_K]);
                _XiSum[_K] += _aux[_K];

                _aux[_K] = _XikPackets[_K][0];
                _XikSum[_K] -= _aux[_K];
                _XiXikProductsSum[_K] -= _XiXikProducts[_K].Dequeue(); ;
                _XikPackets[_K].RemoveAt(0);
            }
        }

        public override void Clear()
        {
            _currentCount = 0;
            _XSum.Clear();
            _XSquareSum.Clear();

            _XiSum.Clear();
            _XiXikSum.Clear();
            _XiXikProductsSum.Clear();
            _XikSum.Clear();
            _XiXikProduct.Clear();

            _awPackets = new List<Queue<int>>(_Kcount);
            _XiPackets = new List<Queue<int>>(_Kcount);
            _XikPackets = new List<List<int>>(_Kcount);
            _XiXikProducts = new List<Queue<int>>(_Kcount);

            _errFlag = false;
        }

        public override string ReportAnalysis()
        {
            if (_errFlag) return "!there are Ks greater than analysis window;" + Environment.NewLine;
            _report = "";
            for (_K = 0; _K < _Kcount; _K++)
            {

                if (_currentCount > 0)
                    _XMean[_K] = (double)(_XSum[_K]) / _currentCount;
                else
                    _XMean[_K] = 0;

                _ac[_K] = (_XiXikProductsSum[_K] - 
                        (_XMean[_K] * _XiSum[_K]) - 
                        (_XMean[_K] * _XikSum[_K]) + 
                        (_currentCount - (_Ks[_K])) * (_XMean[_K] * _XMean[_K])) /
                        (_XSquareSum[_K] - _currentCount * (_XMean[_K] * _XMean[_K]));

                _report += "A(K=" + (_Ks[_K]) + ")=" + _ac[_K] + ";" + Environment.NewLine;
            }
            //_report = _endPacket + "    " + _ac + Environment.NewLine; // gnuplot
            //_report = "EndPacket: " + _endPacket + " _EX2: " + _EX2 + " _EXK: " + _EXK + " _VarX:" + _VarX + " _ac: " + _ac + Environment.NewLine;            
            //Console.WriteLine("_XMean:{0} _XiXikProductsSum:{1} _XiSum:{2} _XikSum:{3} _XSquareSum:{4}", _XMean, _XiXikProductsSum, _XiSum, _XikSum, _XSquareSum);
            //Console.WriteLine("ReportAnalysis() _ac:{0}", _ac);			
            return _report + ";;" + Environment.NewLine;
        }
    }
}