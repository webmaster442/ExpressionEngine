//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using NUnit.Framework;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class TrigonometryTests: TestBase
    {
        [TestCase(0, 0)]
        [TestCase(45, 0.70710678118654752440084436210485)]
        [TestCase(90, 1)]
        [TestCase(180, 0)]
        [TestCase(270, -1)]
        [TestCase(315, -0.70710678118654752440084436210485)]
        [TestCase(360, 0)]
        public void TestSin(double input, double expected)
        {
            NumberMath.AngleMode = AngleMode.Deg;
            Number result = NumberMath.Sin(input);
            AreEqual(expected, result, 1E-12);
        }

        [TestCase(0, 1)]
        [TestCase(45, 0.70710678118654752440084436210485)]
        [TestCase(90, 0)]
        [TestCase(180, -1)]
        [TestCase(270, 0)]
        [TestCase(315, 0.70710678118654752440084436210485)]
        [TestCase(360, 1)]
        public void TestCos(double input, double expected)
        {
            NumberMath.AngleMode = AngleMode.Deg;
            Number result = NumberMath.Cos(input);
            AreEqual(expected, result, 1E-12);
        }
    }
}
