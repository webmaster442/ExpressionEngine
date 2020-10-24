using NUnit.Framework;
using System.Linq;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void TestFlatten()
        {
            ExpressionParser parser = new ExpressionParser();
            IExpression expression = parser.Parse("(x + y + z) * -x", Mocks.CreateVariableMock());

            var items = expression.Flatten().Select(x => x.GetType().Name).ToArray();

            var expected = new string[]
            {
                "MultiplyExpression",
                "NegateExpression",
                "VariableExpression",
                "AddExpression",
                "VariableExpression",
                "AddExpression",
                "VariableExpression",
                "VariableExpression",
            };

            Assert.AreEqual(expected, items);
        }

        [TestCase("x * y", false)]
        [TestCase("x + y", false)]
        [TestCase("x / y", false)]
        [TestCase("x ^ y", false)]
        [TestCase("-y", false)]
        [TestCase("!y", true)]
        [TestCase("x&y", true)]
        [TestCase("x|y", true)]
        [TestCase("(x|y)&(x|z)&!c", true)]
        public void TestLogicExpression(string expression, bool expected)
        {
            ExpressionParser parser = new ExpressionParser();
            IExpression expr = parser.Parse(expression, Mocks.CreateVariableMock());

            bool result = expr.IsLogicExpression();

            Assert.AreEqual(expected, result);
        }

        [TestCase("sin(x)", 0, 180, 2.0)]
        [TestCase("x^2", 0, 8, 170.67)]
        public void TestIntegrate(string expression, double from, double to, double expected)
        {
            ExpressionParser parser = new ExpressionParser();
            IExpression expr = parser.Parse(expression, Mocks.CreateVariableMock());

            double result = expr.Integrate("x", from, to);
            Assert.AreEqual(expected, result, 1E-6);
        }
    }
}
