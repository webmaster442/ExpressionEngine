﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Moq;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class DerivateTests
    {
        private ExpressionParser _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ExpressionParser();
        }

        //Constants
        [TestCase("3", "0")]
        [TestCase("99", "0")]
        [TestCase("360", "0")]
        //1st order
        [TestCase("x", "1")]
        [TestCase("3x", "3")]
        [TestCase("3x+55", "3")]
        //2nd order
        [TestCase("x^2", "(2 * x)")]
        [TestCase("x^2+3x", "((2 * x) + 3)")]
        [TestCase("x^2+3x+22", "((2 * x) + 3)")]
        [TestCase("x^3", "(3 * (x ^ 2))")]
        //inverse
        [TestCase("1/x", "(-1 / (x ^ 2))")]
        //exponent
        [TestCase("2^x", "(0.6931471805599453 * (2 ^ x))")]
        //trigonometry
        [TestCase("sin(x)", "cos(x)")]
        [TestCase("cos(x)", "(-sin(x))")]
        [TestCase("tan(x)", "(cos(x) ^ -2)")]
        [TestCase("ctg(x)", "(-(sin(x) ^ -2))")]
        //Root
        [TestCase("root(x,2)", "(0.5 * (x ^ -0.5))")]
        //Logarithms
        [TestCase("ln(x)", "(1 / x)")]
        [TestCase("Log(x, e)", "(1 / x)")]
        [TestCase("Log(x, 4)", "(0.72134752044448171348 * (1 / x))")]
        public void TestDerivatives(string expression, string expected)
        {

            IExpression derived = _sut.Parse(expression, Mocks.CreateVariableMock()).Differentiate("x").Simplify();
            Assert.AreEqual(expected, derived.ToString());
        }
    }
}
