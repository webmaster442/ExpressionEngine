//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class NumberParserTests
    {
        [TestCase("-11", -11.0)]
        [TestCase("11", 11.0)]
        [TestCase("11.11", 11.11)]
        [TestCase("1E9", 1E9)]
        [TestCase("-1E+9", -1E+9)]
        [TestCase("-1E-9", -1E-9)]
        [TestCase("0hff", 255)]
        [TestCase("0hf_f", 255)]
        [TestCase("0hFF", 255)]
        [TestCase("0hF_F", 255)]
        [TestCase("0255", 173)]
        [TestCase("0_255", 173)]
        [TestCase("0b1010", 10)]
        [TestCase("0b10_10", 10)]
        public void TestParseNumber(string input, double expected)
        {
            double result = NumberParser.ParseNumber(input);
            Assert.AreEqual(expected, result);
        }
    }
}
