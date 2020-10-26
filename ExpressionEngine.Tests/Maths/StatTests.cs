//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Maths;
using NUnit.Framework;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class StatTests
    {
        private double[] data;
        private StatisticResult result;

        [SetUp]
        public void Setup()
        {
            data = new double[]
            {
                6, 6, 7, 13, 15, 20, 21, 22, 23, 27, 28, 29, 31, 36, 40, 41, 46,
                47, 48, 49, 52, 55, 56, 57, 58, 61, 63, 65, 67, 74, 82, 98
            };
            result = Statistics.Statistic(data);
        }

        [Test]
        public void TestMinimum()
        {
            Assert.AreEqual(6, result.Minimum);
        }

        [Test]
        public void TestMaximum()
        {
            Assert.AreEqual(98, result.Maximum);
        }

        [Test]
        public void TestSum()
        {
            Assert.AreEqual(1343, result.Sum);
        }

        [Test]
        public void TestMedian()
        {
            Assert.AreEqual(43.5, result.Median);
        }

        [Test]
        public void TestMode()
        {
            Assert.AreEqual(6, result.Mode);
        }

        [Test]
        public void TestRange()
        {
            Assert.AreEqual(92, result.Range);
        }

        [Test]
        public void TestVariance()
        {
            Assert.AreEqual(535.90221774193549, result.Variance);
        }

        [Test]
        public void TestStdDev()
        {
            Assert.AreEqual(23.149561934125998, result.StandardDeviation);
        }

        [Test]
        public void TestAverage()
        {
            Assert.AreEqual(41.96875, result.Average);
        }

        [Test]
        public void TestRootMeanSquare()
        {
            Assert.AreEqual(47.754908124715307, result.RootMeanSquare);
        }

        	
    }
}
