//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using NUnit.Framework;
using System.Linq;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class LogicFunctionsTest
    {
        [TestCase(8, 2, 3)]
        [TestCase(10, 2, 1, 3)]
        [TestCase(11, 2, 0, 1, 3)]
        [TestCase(12, 2, 2, 3)]
        [TestCase(13, 2, 0, 2, 3)]
        [TestCase(14, 2, 1, 2, 3)]
        [TestCase(15, 2, 0, 1, 2, 3)]
        public void TestGetMinterms(int function, int variables, params int[] expected)
        {
            int[] result = LogicFunctions.GetMinterms(function, variables).ToArray();
            Assert.AreEqual(expected, result);
        }

        [TestCase(8, 2, 0, 1, 2)]
        [TestCase(10, 2, 0, 2)]
        [TestCase(11, 2, 2)]
        [TestCase(12, 2, 0, 1)]
        [TestCase(13, 2, 1)]
        [TestCase(14, 2, 0)]
        [TestCase(15, 2)]
        public void TestGetMaxterms(int function, int variables, params int[] expected)
        {
            int[] result = LogicFunctions.GetMaxterms(function, variables).ToArray();
            Assert.AreEqual(expected, result);
        }

    }
}
