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
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 45)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 49.5)]
        public void SumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(statisticItem.Sum, result);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 4)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 9)]
        public void CountTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(statisticItem.Count, result);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 40)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 5.5)]
        public void ArithmeticMeanTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.ArithmeticMean, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 37.5)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 5.5)]
        public void MedianTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.Median, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 15)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 1.1)]
        public void MinimumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(statisticItem.Minimum, result);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 70)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 9.9)]
        public void MaxmimumTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(statisticItem.Maxmimum, result);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 55)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 8)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 8.8)]
        public void RangeTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.AreEqual(statisticItem.Range, result);
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 462.5)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 6.6667)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 8.0667)]
        public void VarianceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.Variance, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 21.5058)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 2.5820)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 2.8402)]
        public void StandardDeviationTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.StandardDeviation, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 616.667)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 7.5)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 9.075)]
        public void SampleVarianceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.SampleVariance, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 24.8328)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 2.7386)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 3.0125)]
        public void SampleStandardDeviationTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.SampleStandardDeviation, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 20)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 3)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 3.3)]
        public void LowerQuartileTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.LowerQuartile, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 60)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 7)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 7.7)]
        public void HigherQuartileTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.HigherQuartile, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        [TestCase(new double[] { 15, 70, 25, 50 }, 40)]
        [TestCase(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4)]
        [TestCase(new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }, 4.4)]
        public void QuartileDistanceTest(IList<double> input, double result)
        {
            var statisticItem = new StatisticItem(input);
            Assert.That(statisticItem.QuartileDistance, Is.EqualTo(result).Within(0.001));
        }
    }
}