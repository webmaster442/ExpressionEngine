//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Maths;
using NUnit.Framework;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class NumberSystemConverterTests
    {
        [TestCase(10, 16, "A")]
        [TestCase(16, 16, "10")]
        [TestCase(255, 16, "FF")]
        [TestCase(256, 2, "100000000")]
        public void TestToSystem(long input, int system, string expected)
        {
            string result = NumberSystemConverter.ToSystem(input, system);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1010", 2, 10)]
        [TestCase("ff", 16, 255)]
        [TestCase("FF", 16, 255)]
        [TestCase("777", 8, 511)]
        public void TestFromSystem(string input, int system, long expected)
        {
            long resut = NumberSystemConverter.FromSystem(input, system);
            Assert.AreEqual(expected, resut);
        }
    }
}
