using ExpressionEngine.BigNumber;
using NUnit.Framework;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class BigFloatTests
    {
        [TestCase(1, 2, "0.5")]
        [TestCase(2, 4, "0.5")]
        [TestCase(1, 3, "0.3333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333")]
        public void TestToString(long a, long b, string expected)
        {
            var result = new BigFloat(a, b).ToString();
            Assert.AreEqual(expected, result);
        }
    }
}

