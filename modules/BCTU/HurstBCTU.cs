using System;
using System.Collections.Generic;
using NetOdysseyModule;

namespace HurstParameter
{
    class NetOdysseyHurstBCTUModule : NetOdysseyModuleBase, INetOdysseyBCTUAnalyzerModule
    {
        int _Kcount; // check method ModuleStart for K generation
        List<int> _Ks = new List<int>();

        List<Queue<int>> _awBCTU = new List<Queue<int>>();
        List<Queue<int>> _XiBCTU = new List<Queue<int>>();
        List<Queue<int>> _nextKBCTU = new List<Queue<int>>();
        List<List<int>> _XikBCTU = new List<List<int>>();
        List<Queue<int>> _XiXikProducts = new List<Queue<int>>();

        int _bctu = 0;
        int _startPacket = 1;
        int _endPacket = 0;
        int _currentCount = 0;
        int _K = 0;

        // Hurst parameter variables
        double _slope;
        double _hurst;
        double _dYMean;
        double _dXMean;
        double _dAggXX;
        double _dAggXY;
        double _log2K;

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
            for (_K = 2; _K <=200; _K *= 2)
            {
                _Ks.Add(_K);
                _Kstring += _K + ",";
            }
            _Kstring = _Kstring.Remove(_Kstring.Length-1);

            _Kcount = _Ks.Count;

            for (_K = 0; _K < _Kcount; _K++)
            {                
                _awBCTU.Add(new Queue<int>());
                _XiBCTU.Add(new Queue<int>());
                _nextKBCTU.Add(new Queue<int>());
                _XikBCTU.Add(new List<int>());
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

            _bctu = (int)bctu;
            //Console.WriteLine("AnalyzeBCTUIn: {0}", _bctu);

            if (_Ks[_Kcount - 1] >= WindowSize)
            {
                _errFlag = true;
                return;
            }
            
            for (_K = 0; _K < _Kcount; _K++)
            {
                //Console.WriteLine("here1.1 _K={0}", _K);
                _awBCTU[_K].Enqueue(_bctu);
                //Console.WriteLine("here1.2 _K={0}", _K);
                _XSum[_K] += _bctu;
                //Console.WriteLine("here1.3 _K={0}", _K);
                _XSquareSum[_K] += _bctu * _bctu;
                //Console.WriteLine("here1.4 _K={0}", _K);
                if (_currentCount <= WindowSize - _Ks[_K])
                {
                    //Console.WriteLine("here2.1 _K={0}", _K);
                    _XiBCTU[_K].Enqueue(_bctu);
                    //Console.WriteLine("here2.2 _K={0}", _K);
                    _XiSum[_K] += _bctu;
                    //Console.WriteLine("here2.3 _K={0}", _K);
                }
                else
                {                    
                    _nextKBCTU[_K].Enqueue(_bctu);
                }
                if (_currentCount > _Ks[_K])
                {                    
                    _XikBCTU[_K].Add(_bctu);
                    _XikSum[_K] += _bctu;

                    _XiXikProduct[_K] = _awBCTU[_K].Dequeue() * _bctu;
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

                _aux[_K] = _XiBCTU[_K].Dequeue();
                _XiSum[_K] -= _aux[_K];

                _aux[_K] = _nextKBCTU[_K].Dequeue();
                _XiBCTU[_K].Enqueue(_aux[_K]);
                _XiSum[_K] += _aux[_K];

                _aux[_K] = _XikBCTU[_K][0];
                _XikSum[_K] -= _aux[_K];
                _XiXikProductsSum[_K] -= _XiXikProducts[_K].Dequeue(); ;
                _XikBCTU[_K].RemoveAt(0);
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

            _awBCTU = new List<Queue<int>>(_Kcount);
            _XiBCTU = new List<Queue<int>>(_Kcount);
            _XikBCTU = new List<List<int>>(_Kcount);
            _XiXikProducts = new List<Queue<int>>(_Kcount);

            _errFlag = false;
        }

        public override string ReportAnalysis()
        {
            if (_errFlag) {
                Console.WriteLine("!there are Ks greater than analysis window;");
                return "!there are Ks greater than analysis window;" + Environment.NewLine; 
            }
            _report = "";
            _dYMean = 0;
            _dXMean = 0;
            _dAggXX = 0;
            _dAggXY = 0;

            for (_K = 0; _K < _Kcount; _K++)
            {

                if (_currentCount > 0)
                    _XMean[_K] = (double)(_XSum[_K]) / _currentCount;
                else
                    _XMean[_K] = 0;

                _ac[_K] = Math.Log(((_XiXikProductsSum[_K] - (_XMean[_K] * _XiSum[_K]) - (_XMean[_K] * _XikSum[_K]) + (_currentCount - (_Ks[_K])) * (_XMean[_K] * _XMean[_K])) /
                      (_XSquareSum[_K] - _currentCount * (_XMean[_K] * _XMean[_K]))) + 2 , 2);

                _log2K = Math.Log(_Ks[_K], 2);

                if (_Kcount > 1)
                {                    
                    _dYMean += _ac[_K] / (double) _Kcount;
                    _dXMean += _log2K / (double) _Kcount;
                    _dAggXX += _log2K * _log2K;
                    _dAggXY += _log2K * _ac[_K];

                    _slope = (_dAggXY - (double) _Kcount * _dXMean * _dYMean) / (_dAggXX - (double) _Kcount * _dXMean * _dXMean);
                }

                _report += "log2(K=" + (_Ks[_K]) + ")=;" + _log2K + "; log2(A(K=" + (_Ks[_K]) + "))=" + _ac[_K] + ";" + Environment.NewLine;
            }

            if (_Kcount > 1) {
                _hurst = (_slope + 1) / 2.0;
                _report += "_slope=;" + _slope + "; _hurst=;" + _hurst + ";" + Environment.NewLine;
            }

            return _report + ";;;" + Environment.NewLine;
        }
    }
}