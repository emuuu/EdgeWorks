using FluiTec.AppFx.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdgeWorks.Data.Statistics
{
    [EntityName("ItemStatistic")]
    public class ItemStatistic : IEntity<int>
    {
        public int Id { get; set; }

        public long ItemID { get; set; }

        public string Name { get; set; }

        public DateTime TimeStamp { get; set; }

        public double Sum { get; set; }

        public int Count { get; set; }

        public double ArithmeticMean { get; set; }

        public double Median { get; set; }

        public double Minimum { get; set; }

        public double Maxmimum { get; set; }

        public double Range { get; set; }

        public double Variance { get; set; }

        public double StandardDeviation { get; set; }

        public double SampleVariance { get; set; }

        public double SampleStandardDeviation { get; set; }

        public double LowerQuartile { get; set; }

        public double HigherQuartile { get; set; }

        public double QuartileDistance { get; set; }
    }
}
