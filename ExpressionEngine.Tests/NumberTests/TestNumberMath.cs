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
        private void AreEqual(Number expected, Number result, Number diff)
        {
            if (result.State == NumberState.Value 
                && expected.State == NumberState.Value)
            {
                bool assert = NumberMath.Abs(expected - result) < diff;
                Assert.IsTrue(assert);
            }
            else
            {
                Assert.IsTrue(expected.State == result.State);
            }
        }

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

        [TestCase(0.0, 0)]
        [TestCase(45.0, Math.PI / 4)]
        [TestCase(90.0, Math.PI / 2)]
        [TestCase(135.0, (Math.PI * 3) / 4)]
        [TestCase(180.0, Math.PI)]
        [TestCase(270.0, (Math.PI * 3) / 2)]
        [TestCase(360.0, Math.PI * 2)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestDegToRad(double deg, double expectedRad)
        {
            Number result = NumberMath.DegToRad(deg);
            AreEqual(expectedRad, result, 1E-9);
        }

        [TestCase(0, 0)]
        [TestCase(Math.PI / 4, 45)]
        [TestCase(Math.PI / 2, 90)]
        [TestCase((Math.PI * 3) / 4, 135)]
        [TestCase(Math.PI, 180)]
        [TestCase((Math.PI * 3) / 2, 270)]
        [TestCase(Math.PI * 2, 360)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestRadToDeg(double rad, double expectedDeg)
        {
            Number result = NumberMath.RadToDeg(rad);
            AreEqual(expectedDeg, result, 1E-9);
        }

        [TestCase(0, 0)]
        [TestCase(45, 50)]
        [TestCase(90, 100)]
        [TestCase(135, 150)]
        [TestCase(180, 200)]
        [TestCase(270, 300)]
        [TestCase(360, 400)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestDegToGrad(double deg, double expectedGrad)
        {
            Number result = NumberMath.DegToGrad(deg);
            AreEqual(expectedGrad, result, 1E-9);
        }

        [TestCase(0, 0)]
        [TestCase(50, 45)]
        [TestCase(100, 90)]
        [TestCase(150, 135)]
        [TestCase(200, 180)]
        [TestCase(300, 270)]
        [TestCase(400, 360)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestGradToDeg(double grad, double expectedDeg)
        {
            Number result = NumberMath.GradToDeg(grad);
            AreEqual(expectedDeg, result, 1E-9);
        }

        [TestCase(0, 0)]
        [TestCase(50, Math.PI / 4)]
        [TestCase(100, Math.PI / 2)]
        [TestCase(150, (Math.PI * 3) / 4)]
        [TestCase(200, Math.PI)]
        [TestCase(300, (Math.PI * 3) / 2)]
        [TestCase(400, Math.PI * 2)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestGradToRad(double grad, double expectedRad)
        {
            Number result = NumberMath.GradToRad(grad);
            AreEqual(expectedRad, result, 1E-9);
        }

        [TestCase(0, 0)]
        [TestCase(Math.PI / 4, 50)]
        [TestCase(Math.PI / 2, 100)]
        [TestCase((Math.PI * 3) / 4, 150)]
        [TestCase(Math.PI, 200)]
        [TestCase((Math.PI * 3) / 2, 300)]
        [TestCase(Math.PI * 2, 400)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void TestRadToGrad(double rad, double expectedGrad)
        {
            Number result = NumberMath.RadToGrad(rad);
            AreEqual(expectedGrad, result, 1E-9);
        }

        [TestCase(-1, double.NaN)]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(5, 120)]
        [TestCase(50, 3.0414093201713376E64)]
        [TestCase(170, 7.257415615307994E306)]
        [TestCase(171, double.PositiveInfinity)]
        public void TestFactorial(int n, double expected)
        {
            Number result = NumberMath.Factorial(n);
            AreEqual(expected, result, 1E-9);
        }
    }
}
