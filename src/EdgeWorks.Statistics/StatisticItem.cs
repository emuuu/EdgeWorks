using EdgeWorks.Statistics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeWorks.Statistics.Statistics
{
    public class StatisticItem
    {
        private IList<double> _sample;
        private double _sum;
        private int _count;
        private double _arithmeticMean;
        private double _median;
        private double _minimum;
        private double _maximum;
        private double _range;
        private double _variance;
        private double _standardDeviation;
        private double _sampleVariance;
        private double _sampleStandardDeviation;
        private double _lowerQuartile;
        private double _higherQuartile;
        private double _quartileDistance;

        public StatisticItem(IList<double> sample)
        {
            _sample = sample;
        }

        public double Sum 
        {
            get
            {
                if (_sum == default)
                    _sum = _sample.Sum();
                return _sum;
            }
        }

        public int Count
        {
            get
            {
                if (_count == default)
                    _count = _sample.Count();
                return _count;
            }
        }

        public double ArithmeticMean
        {
            get
            {
                if (_arithmeticMean == default)
                    _arithmeticMean = _sample.Average();
                return _arithmeticMean;
            }
        }

        public double Median
        {
            get
            {
                if (_median == default)
                    _median = _sample.GetMedian();
                return _median;
            }
        }

        public double Minimum
        {
            get
            {
               if (_minimum == default)
                    _minimum = _sample.Min();
                return _minimum;
            }
        }

        public double Maxmimum
        {
            get
            {
                if (_maximum == default)
                    _maximum = _sample.Max();
                return _maximum;
            }
        }

        public double Range
        {
            get
            {
                if (_range == default)
                    _range = Maxmimum - Minimum;
                return _range;
            }
        }

        public double Variance
        {
            get
            {
                if (_variance == default)
                    _variance = _sample.Select(val => Math.Pow((val - ArithmeticMean), 2)).Sum() / Count;
                return _variance;
            }
        }

        public double StandardDeviation
        {
            get
            {
                if (_standardDeviation == default)
                    _standardDeviation = Math.Sqrt(Variance);
                return _standardDeviation;
            }
        }

        public double SampleVariance
        {
            get
            {
                if (_sampleVariance == default)
                    _sampleVariance = _sample.Select(val => Math.Pow((val - ArithmeticMean), 2)).Sum() / (Count - 1);
                return _sampleVariance;
            }
        }

        public double SampleStandardDeviation
        {
            get
            {
                if (_sampleStandardDeviation == default)
                    _sampleStandardDeviation = Math.Sqrt(SampleVariance);
                return _sampleStandardDeviation;
            }
        }

        public double LowerQuartile
        {
            get
            {
                if (_lowerQuartile == default)
                {
                    var lowerSample = new StatisticItem(_sample.Where(x => x <= Median).ToList());
                    _lowerQuartile = lowerSample.Median;
                }
                return _lowerQuartile;
            }
        }

        public double HigherQuartile
        {
            get
            {
                if (_higherQuartile == default)
                {
                    var higherSample = new StatisticItem(_sample.Where(x => x >= Median).ToList());
                    _higherQuartile = higherSample.Median;
                }
                return _higherQuartile;
            }
        }

        public double QuartileDistance
        {
            get
            {
                if (_quartileDistance == default)
                    _quartileDistance = HigherQuartile - LowerQuartile;

                return _quartileDistance;
            }
        }


    }
}
