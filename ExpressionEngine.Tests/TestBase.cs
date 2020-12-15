using ExpressionEngine.Numbers;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    public abstract class TestBase
    {
        public static void AreEqual(Number expected, Number result, double delta = 0.0)
        {
            if (NumberAlgorithms.IsSpecialState(expected) 
                && NumberAlgorithms.IsSpecialState(result)
                && expected.State != result.State)
            {
                Assert.Fail("Expected: {0}, result: {1}", expected, result);
            }
            else
            {
                Assert.Pass();
            }


            var diff = NumberMath.Abs(expected - result) < delta;
            if (!diff)
            {
                Assert.Fail("Expected: {0}, result: {1}", expected, result);
            }

            Assert.Pass();

        }
    }
}
