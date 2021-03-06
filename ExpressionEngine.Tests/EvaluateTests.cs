﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class EvaluateTests: TestBase
    {
        private ExpressionParser _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ExpressionParser();
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
        [TestCase("ctg(360)", double.NegativeInfinity)]
        [TestCase("root(2, 2)", 1.4142135623730950488)]
        [TestCase("ln(e)", 1)]
        [TestCase("ln(100)", 4.6051701859880913680359829093687)]
        [TestCase("log(1024,2)", 10)]
        [TestCase("factorial(5)", 120)]
        [TestCase("0&0", 0)]
        [TestCase("0&1", 0)]
        [TestCase("1&0", 0)]
        [TestCase("1&1", 1)]
        [TestCase("0|0", 0)]
        [TestCase("0|1", 1)]
        [TestCase("1|0", 1)]
        [TestCase("1|1", 1)]
        [TestCase("!0", 1)]
        [TestCase("!1", 0)]
        [TestCase("!(!1)", 1)]
        [TestCase("!0&!0", 1)]
        [TestCase("false", 0)]
        [TestCase("true", 1)]
        [TestCase("sin(y)", 1)]
        [TestCase("y^2", 8100)]
        [TestCase("y-2", 88)]
        [TestCase("y+2", 92)]
        [TestCase("y*2", 180)]
        [TestCase("y/2", 45)]
        public void TestEvaluator(string expression, double expected)
        {
            IExpression parsed = _sut.Parse(expression, Mocks.CreateVariableMock());

            Number result = parsed.Evaluate() as Number;

            AreEqual(expected, result, 1E-6);
        }

        [TestCase("1:2")]
        [TestCase("1Ö2")]
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
                IExpression parsed = _sut.Parse(expression, Mocks.CreateVariableMock());
            });
        }

        [TestCase("(((a & b) & c) & (((!a) & (!b)) & (!c)))", 0, 7)]
        public void TestParseMinterms(string expected, params int[] minterms)
        {
            IExpression parsed = _sut.ParseMinterms(minterms, Mocks.CreateVariableMock(), 3);

            Assert.AreEqual(expected, parsed.ToString());
        }
    }
}
