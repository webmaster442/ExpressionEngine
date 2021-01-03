//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using NUnit.Framework;
using System.Globalization;
using System.Numerics;

namespace ExpressionEngine.Tests.NumberTests
{
    [TestFixture]
    public class NumberTests
    {
        [TestCase("nan", "NaN")]
        [TestCase("Nan", "NaN")]
        [TestCase("NAn", "NaN")]
        [TestCase("∞", "∞")]
        [TestCase("-∞", "-∞")]
        [TestCase("11", "11")]
        [TestCase("-11", "-11")]
        [TestCase("11.12", "11.12")]
        [TestCase("0.3333", "0.3333")]
        [TestCase("11.12E6", "11120000")]
        [TestCase("11.12E+6", "11120000")]
        [TestCase("12E-6", "0.000012")]
        [TestCase("12.1E-6", "0.0000121")]
        [TestCase("-1E-6", "-0.000001")]
        [TestCase("-12.1E-6", "-0.0000121")]
        [TestCase("0.0000121", "0.0000121")]
        [TestCase("-0.25", "-0.25")]
        public void TestParse(string input, string expected)
        {
            Number n = Number.Parse(input, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, n.ToString());
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("asd", false)]
        [TestCase("11.11.11", false)]
        [TestCase("-11E11+11", false)]
        [TestCase("-23.24", true)]
        public void TestTryParse(string input, bool expected)
        {
            bool result = Number.TryParse(input, CultureInfo.InvariantCulture, out Number _);
            Assert.AreEqual(expected, result);
        }

        [TestCase(2, 6, 1, 4, 7, 12)]
        [TestCase(3, 10, 1, 5, 1, 2)]
        [TestCase(5, 16, 5, 12, 35, 48)]
        [TestCase(3, 8, 9, 16, 15, 16)]
        public void TestAdd(long n1, long d1, long n2, long d2, long expN, long expD)
        {
            Number a = new Number(n1, d1);
            Number b = new Number(n2, d2);

            Number result = a + b;

            Assert.AreEqual(new BigInteger(expN), result.Numerator);
            Assert.AreEqual(new BigInteger(expD), result.Denominator);
        }

        [TestCase(2, 6, 1, 4, 1, 12)]
        [TestCase(3, 10, 1, 5, 1, 10)]
        [TestCase(5, 16, 5, 12, -5, 48)]
        [TestCase(3, 8, 9, 16, -3, 16)]
        public void TestSubtract(long n1, long d1, long n2, long d2, long expN, long expD)
        {
            Number a = new Number(n1, d1);
            Number b = new Number(n2, d2);

            Number result = a - b;

            Assert.AreEqual(new BigInteger(expN), result.Numerator);
            Assert.AreEqual(new BigInteger(expD), result.Denominator);
        }

        [TestCase(2, 6, 1, 4, 1, 12)]
        [TestCase(3, 10, 1, 5, 3, 50)]
        [TestCase(5, 16, 5, 12, 25, 192)]
        [TestCase(3, 8, 9, 16, 27, 128)]
        public void TestMultiply(long n1, long d1, long n2, long d2, long expN, long expD)
        {
            Number a = new Number(n1, d1);
            Number b = new Number(n2, d2);

            Number result = a * b;

            Assert.AreEqual(new BigInteger(expN), result.Numerator);
            Assert.AreEqual(new BigInteger(expD), result.Denominator);
        }

        [TestCase(2, 6, 1, 4, 4, 3)]
        [TestCase(3, 10, 1, 5, 3, 2)]
        [TestCase(5, 16, 5, 12, 3, 4)]
        [TestCase(3, 8, 9, 16, 2, 3)]
        public void TestDivide(long n1, long d1, long n2, long d2, long expN, long expD)
        {
            Number a = new Number(n1, d1);
            Number b = new Number(n2, d2);

            Number result = a / b;

            Assert.AreEqual(new BigInteger(expN), result.Numerator);
            Assert.AreEqual(new BigInteger(expD), result.Denominator);
        }
    }
}
