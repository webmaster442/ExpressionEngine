using ExpressionEngine.Numbers;
using NUnit.Framework;
using System.Globalization;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class InternalMathTests
    {
        [TestCase("2", 2, "1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727")]
        [TestCase("3", 2, "1.7320508075688772935274463415058723669428052538103806280558069794519330169088000370811461867572485756")]
        [TestCase("2", 1, "2")]
        [TestCase("1", 2, "1")]
        public void TestRoot(string input, int root, string expected)
        {
            var number = BigFloat.Parse(input, CultureInfo.InvariantCulture);
            var op = InternalMath.Root(number, root).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase("2", 2, "4")]
        [TestCase("1", 2, "1")]
        [TestCase("2", 1, "2")]
        [TestCase("1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727", 2, "2")]
        [TestCase("1.7320508075688772935274463415058723669428052538103806280558069794519330169088000370811461867572485756", 2, "3")]
        public void TestPow(string input, int exponent, string expected)
        {
            var number = BigFloat.Parse(input, CultureInfo.InvariantCulture);
            var op = InternalMath.Pow(number, exponent).ToString();
            Assert.AreEqual(expected, op);
        }

        [TestCase(2L, 4L, 4L)]
        [TestCase(10L, 4L, 20L)]
        [TestCase(7919L, 7907L, 62615533L)]
        public void TestLcm(long a, long b, long expected)
        {
            var result = InternalMath.Lcm(a, b);
            Assert.AreEqual(expected, (long)result);
        }

        [TestCase(7919L, 257L, 1L)]
        [TestCase(18014398241046527L, 4398042316799L, 1L)]
        public void TestGcd(long a, long b, long expected)
        {
            var result = InternalMath.Gcd(a, b);
            Assert.AreEqual(expected, (long)result);
        }

        [Test]
        public void TestLn()
        {
            var result = InternalMath.Ln(BigMath.E);
            Assert.AreEqual(BigFloat.One, result);
        }
    }
}
