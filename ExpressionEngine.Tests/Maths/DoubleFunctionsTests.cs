//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using NUnit.Framework;
using System;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class DoubleFunctionsTests: TestBase
    {
        [TestCase(0, 0)]
        [TestCase(45, Math.PI/4)]
        [TestCase(90, Math.PI/2)]
        [TestCase(135, (Math.PI*3)/4)]
        [TestCase(180, Math.PI)]
        [TestCase(270, (Math.PI*3)/2)]
        [TestCase(360, Math.PI*2)]
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
            AreEqual(expected, result);
        }
    }
}
