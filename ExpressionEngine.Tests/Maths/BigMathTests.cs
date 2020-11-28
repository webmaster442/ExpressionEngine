using ExpressionEngine.BigNumber;
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

        [TestCase("2", 2, "1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727")]
        [TestCase("3", 2, "1.7320508075688772935274463415058723669428052538103806280558069794519330169088000370811461867572485756")]
        [TestCase("2", 1, "2")]
        [TestCase("1", 2, "1")]
        public void TestRoot(string input, int root, string expected)
        {
            var number = BigFloat.Parse(input, CultureInfo.InvariantCulture);
            var op = BigMath.Root(number, root).ToString();
            Assert.AreEqual(expected, op);
        }
    }
}
