using EdgeWorks.Statistics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgeWorks.Statistics.Statistics
{
    public class StatisticItem
    {
        private IList<double> _sample;

        public StatisticItem(IList<double> sample)
        {
            _sample = sample;
        }

        public double Sum 
        {
            get
            {
                return _sample.Sum();
            }
        }

        public int Count
        {
            get
            {
                return _sample.Count();
            }
        }

        public double ArithmeticMean
        {
            get
            {
                return _sample.Average();
            }
        }

        public double Median
        {
            get
            {
                return _sample.Median<double>();
            }
        }

        public double Minimum
        {
            get
            {
                return _sample.Min();
            }
        }

        public double Maxmimum
        {
            get
            {
                return _sample.Max();
            }
        }

        public double Range
        {
            get
            {
                return Maxmimum - Minimum;
            }
        }

        public double Variance
        {
            get
            {
                return default;
            }
        }

        public double StandardDeviation
        {
            get
            {
                return Math.Sqrt(_sample.Select(val => (val - ArithmeticMean) * (val - ArithmeticMean)).Sum() / Count);
            }
        }

        public double SampleVariance
        {
            get
            {
                return default;
            }
        }

        public double SampleStandardDeviation
        {
            get
            {
                return default;
            }
        }

        public double LowerQuartile
        {
            get
            {
                return default;
            }
        }

        public double HigherQuartile
        {
            get
            {
                return default;
            }
        }

        public double QuartileDistance
        {
            get
            {
                return HigherQuartile - LowerQuartile;
            }
        }


    }
}
