using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeWorks.Statistics.Extensions
{
    public static class MathExtensions
    {

        public static double GetMedian(this IList<double> sample)
        {
            double[] sortedPNumbers = sample.ToArray();
            Array.Sort(sortedPNumbers);

            int size = sortedPNumbers.Length;
            int mid = size / 2;
            return (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
        }

    }
}
