using EdgeWorks.Statistics.Statistics;
using NUnit.Framework;
using System.Collections.Generic;

namespace EdgeWorks.Statistics.UnitTests
{
    public class StatisticItemTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 160)]
        public void SumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Sum);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 4)]
        public void CountTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Count);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 40)]
        public void ArithmeticMeanTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.ArithmeticMean);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 37.5)]
        public void MedianTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Median);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 15)]
        public void MinimumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Minimum);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 70)]
        public void MaxmimumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Maxmimum);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 55)]
        public void RangeTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Range);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 462.5)]
        public void VarianceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.Variance);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 21.5058)]
        public void StandardDeviationTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.StandardDeviation);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 616.667)]
        public void SampleVarianceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.SampleVariance);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 24.8328)]
        public void SampleStandardDeviationTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.SampleStandardDeviation);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 20)]
        public void LowerQuartileTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.LowerQuartile);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 60)]
        public void HigherQuartileTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.HigherQuartile);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 40)]
        public void QuartileDistanceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(result, statisticItem.QuartileDistance);
        }
    }
}