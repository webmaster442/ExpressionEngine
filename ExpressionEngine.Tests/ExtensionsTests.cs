﻿using ExpressionEngine.Maths;
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

        [TestCase("x^2", 0, 8, 170.67)]
        [TestCase("x^3", 0, 8, 1024.00)]
        [TestCase("e^x", 0, 5, 147.41)]
        public void TestIntegrate(string expression, double from, double to, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Rad;

            ExpressionParser parser = new ExpressionParser();
            IExpression expr = parser.Parse(expression, Mocks.CreateVariableMock());

            double result = expr.Integrate("x", from, to);
            Assert.AreEqual(expected, result, 1E-2);
        }

        [TestCase("sin(x)", AngleMode.Rad, 0, 3.14159265359, 2.0)]
        [TestCase("cos(x)", AngleMode.Rad, 0, 3.14159265359, 0.0)]
        [TestCase("sin(x)", AngleMode.Rad, 0, 6.28318530718, 0.0)]
        [TestCase("cos(x)", AngleMode.Rad, 0, 6.28318530718, 0.0)]
        [TestCase("sin(x)", AngleMode.Deg, 0, 180.0, 2.0)]
        [TestCase("cos(x)", AngleMode.Deg, 0, 180.0, 0.0)]
        [TestCase("sin(x)", AngleMode.Deg, 0, 360.0, 0.0)]
        [TestCase("cos(x)", AngleMode.Deg, 0, 360.0, 0.0)]

        public void TestTrigonometricIntegrate(string expression, AngleMode mode, double from, double to, double expected)
        {
            Trigonometry.AngleMode = mode;

            ExpressionParser parser = new ExpressionParser();
            IExpression expr = parser.Parse(expression, Mocks.CreateVariableMock());

            double result = expr.Integrate("x", from, to);
            Assert.AreEqual(expected, result, 1E-2);
        }
    }
}
