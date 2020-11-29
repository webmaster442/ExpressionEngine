using ExpressionEngine.Numbers;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class BigMathTests
    {
        private void CallMethodUnderTest(string input, string expected, Func<BigFloat, BigFloat> method)
        {
            var number = BigFloat.Parse(input, CultureInfo.InvariantCulture);
            var op = method.Invoke(number).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase("-0.5", "0.5")]
        [TestCase("0.5", "0.5")]
        public void TestAbs(string input, string expected)
        {
            CallMethodUnderTest(input, expected, BigMath.Abs);
        }

        [TestCase("7.03", "7")]
        [TestCase("7.64", "7")]
        [TestCase("0.12", "0")]
        [TestCase("-0.12", "-1")]
        [TestCase("-7.1", "-8")]
        [TestCase("-7.6", "-8")]
        public void TestFloor(string input, string expected)
        {
            CallMethodUnderTest(input, expected, BigMath.Floor);
        }

        [TestCase("7.03", "8")]
        [TestCase("7.64", "8")]
        [TestCase("0.12", "1")]
        [TestCase("-0.12", "0")]
        [TestCase("-7.1", "-7")]
        [TestCase("-7.6", "-7")]
        public void TestCeiling(string input, string expected)
        {
            CallMethodUnderTest(input, expected, BigMath.Ceiling);
        }

        [TestCase("32.7865", "32")]
        [TestCase("-32.9012", "-32")]
        public void TestTruncate(string input, string expected)
        {
            CallMethodUnderTest(input, expected, BigMath.Truncate);
        }

        [TestCase("2.12344", 3, "2.123")]
        [TestCase("3.44", 1, "3.4")]
        [TestCase("3.45", 1, "3.4")]
        [TestCase("3.46", 1, "3.5")]
        public void TestRound(string input, int decimals, string expected)
        {
            var number = BigFloat.Parse(input, CultureInfo.InvariantCulture);
            var op = BigMath.Round(number, decimals).ToString();
            Assert.AreEqual(expected, op);
        }
    }
}
