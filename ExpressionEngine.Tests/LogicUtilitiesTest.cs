//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.LogicExpressions;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class LogicUtilitiesTest
    {
        [TestCase(0, "00000000")]
        [TestCase(15, "00001111")]
        [TestCase(255, "11111111")]
        public void TestGetBinaryValue(int input, string expected)
        {
            string result = Utilities.GetBinaryValue(input, 8);
            Assert.AreEqual(expected, result);
        }

        [TestCase("00", "00000")]
        [TestCase("00000", "00000")]
        [TestCase("00000", "00")]
        public void TestGetBallanced(string input1, string input2)
        {
            string cpy1 = input1;
            string cpy2 = input2;

            Utilities.GetBalanced(ref cpy1, ref cpy2);

            Assert.AreEqual(cpy1, cpy2);
        }

        [TestCase("00000", "00000", 0)]
        [TestCase("00001", "00000", 1)]
        [TestCase("00011", "00000", 2)]
        [TestCase("11100", "00000", 3)]
        [TestCase("00000", "11111", 5)]
        public void TestGetDifferences(string input1, string input2, int expected)
        {
            int result = Utilities.GetDifferences(input1, input2);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1111", 4)]
        [TestCase("0111", 3)]
        [TestCase("0110", 2)]
        public void TestGetOneCount(string input, int expected)
        {
            int result = Utilities.GetOneCount(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("0000", "(!A&!B&!C&!D)")]
        [TestCase("1111", "(A&B&C&D)")]
        [TestCase("11", "(A&B)")]
        public void TestGetMintermExpressionMsbA(string input, string expected)
        {
            string result = Utilities.GetMintermExpression(input, true);
            Assert.AreEqual(expected, result);
        }

        [TestCase("0000", "(!D&!C&!B&!A)")]
        [TestCase("1111", "(D&C&B&A)")]
        [TestCase("11", "(B&A)")]
        public void TestGetMintermExpression(string input, string expected)
        {
            string result = Utilities.GetMintermExpression(input, false);
            Assert.AreEqual(expected, result);
        }
    }
}
