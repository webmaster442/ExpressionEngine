//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine;
using NUnit.Framework;
using System.Globalization;

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
        [TestCase("0xff", 255)]
        [TestCase("0xf_f", 255)]
        [TestCase("0xFF", 255)]
        [TestCase("0xF_F", 255)]
        [TestCase("0255", 173)]
        [TestCase("0_255", 173)]
        [TestCase("0b1010", 10)]
        [TestCase("0b10_10", 10)]
        public void TestParseNumber(string input, double expected)
        {
            bool result = NumberParser.ParseNumber(input, out INumber number);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.ToString(CultureInfo.InvariantCulture), number.ToString());
        }
    }
}
