//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ExpressionEngine.Tests.NumberTests
{
    [TestFixture]
    public class TestNumberMath
    {
        private void CallMethodUnderTest(string input, string expected, Func<Number, Number> method)
        {
            var number = Number.Parse(input, CultureInfo.InvariantCulture);
            var op = method.Invoke(number).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase("-0.5", "0.5")]
        [TestCase("0.5", "0.5")]
        public void TestAbs(string input, string expected)
        {
            CallMethodUnderTest(input, expected, NumberMath.Abs);
        }


        [TestCase("7.03", "7")]
        [TestCase("7.64", "7")]
        [TestCase("0.12", "0")]
        [TestCase("-0.12", "-1")]
        [TestCase("-7.1", "-8")]
        [TestCase("-7.6", "-8")]
        public void TestFloor(string input, string expected)
        {
            CallMethodUnderTest(input, expected, NumberMath.Floor);
        }

        [TestCase("7.03", "8")]
        [TestCase("7.64", "8")]
        [TestCase("0.12", "1")]
        [TestCase("-0.12", "0")]
        [TestCase("-7.1", "-7")]
        [TestCase("-7.6", "-7")]
        public void TestCeiling(string input, string expected)
        {
            CallMethodUnderTest(input, expected, NumberMath.Ceiling);
        }

        [TestCase("32.7865", "32")]
        [TestCase("-32.9012", "-32")]
        public void TestTruncate(string input, string expected)
        {
            CallMethodUnderTest(input, expected, NumberMath.Truncate);
        }

        [TestCase("2.12344", 3, "2.123")]
        [TestCase("3.44", 1, "3.4")]
        [TestCase("3.45", 1, "3.4")]
        [TestCase("3.46", 1, "3.5")]
        public void TestRound(string input, int decimals, string expected)
        {
            var number = Number.Parse(input, CultureInfo.InvariantCulture);
            var op = NumberMath.Round(number, decimals).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase("2", 2, "1.414213562373095048801")]
        [TestCase("3", 2, "1.732050807568877293527")]
        [TestCase("2", 1, "2")]
        [TestCase("1", 2, "1")]
        public void TestRoot(string input, int root, string expected)
        {
            var number = Number.Parse(input, CultureInfo.InvariantCulture);
            var op = NumberAlgorithms.Root(number, root).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase("2", 2, "4")]
        [TestCase("1", 2, "1")]
        [TestCase("2", 1, "2")]
        [TestCase("1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727", 2, "2")]
        [TestCase("1.7320508075688772935274463415058723669428052538103806280558069794519330169088000370811461867572485756", 2, "3")]
        public void TestPow(string input, int exponent, string expected)
        {
            var number = Number.Parse(input, CultureInfo.InvariantCulture);
            var op = NumberAlgorithms.Pow(number, exponent).ToString();
            Assert.AreEqual(expected, op);
        }
    }
}
