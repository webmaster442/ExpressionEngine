﻿using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class EvaluateTests
    {
        private ExpressionParser _sut;
        private Variables _variables;

        [SetUp]
        public void Setup()
        {
            _sut = new ExpressionParser();
            _variables = new Variables();
        }

        [TestCase("1\t+1", 2)]
        [TestCase("1 +1", 2)]
        [TestCase("1\r+1", 2)]
        [TestCase("1\n+1", 2)]
        [TestCase("1\r\n+1", 2)]
        [TestCase("2^10", 1024)]
        [TestCase("1/2+1/4", 0.75d)]
        [TestCase("(1/2)*2+(4*3)", 13d)]
        [TestCase("1+-2", -1d)]
        [TestCase("3*pi", 9.4247779607693797153879301498385d)]
        [TestCase("3*PI", 9.4247779607693797153879301498385d)]
        [TestCase("sin(90)", 1d)]
        [TestCase("cos(90)", 0d)]
        [TestCase("tan(90)", double.PositiveInfinity)]
        [TestCase("tan(180)", 0)]
        [TestCase("tan(270)", double.PositiveInfinity)]
        [TestCase("tan(360)", 0)]
        [TestCase("ctg(0)", double.PositiveInfinity)]
        [TestCase("ctg(90)", 0)]
        [TestCase("ctg(180)", double.NegativeInfinity)]
        [TestCase("ctg(270)", 0)]
        [TestCase("ctg(360)", double.PositiveInfinity)]
        [TestCase("root(2, 2)", 1.4142135623730950488)]
        [TestCase("ln(e)", 1)]
        [TestCase("ln(100)", 4.6051701859880913680359829093687)]
        [TestCase("log(1024,2)", 10)]
        public void TestEvaluator(string expression, double expected)
        {
            IExpression parsed = _sut.Parse(expression, _variables);

            double result = parsed.Evaluate();

            Assert.AreEqual(expected, result, 1E-15);
        }

        [TestCase("1:2")]
        [TestCase("1Ö2")]
        [TestCase("1|2")]
        [TestCase("1=2")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        [TestCase(" ")]
        [TestCase("\r\n")]
        [TestCase("\r")]
        [TestCase("\n")]
        [TestCase("sin(99ö)")]
        [TestCase("root(99,)")]
        [TestCase("root(,99)")]
        public void TestInvalidTokens(string expression)
        {
            Assert.Throws<ExpressionEngineException>(() =>
            {
                IExpression parsed = _sut.Parse(expression, _variables);
            });
        }
    }
}
