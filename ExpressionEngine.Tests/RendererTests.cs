//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer;
using Moq;
using NUnit.Framework;

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public class RendererTests
    {
        private ExpressionRenderer _sut;
        private Mock<IWriter> _writerMock;

        [SetUp]
        public void Setup()
        {
            _writerMock = new Mock<IWriter>();
            _sut = new ExpressionRenderer(_writerMock.Object);
        }

        [TestCase("let x 3.14", true)]
        [TestCase("let x 0xff", true)]
        [TestCase("let x 0b_1111_1111", true)]
        [TestCase("let x 01247", true)]
        [TestCase("let x 3+2", true)]
        [TestCase("let y x*2", true)]
        [TestCase("let", false)]
        [TestCase("let pi 3", false)]
        [TestCase("let d qf ewfg", false)]
        public void TestLetCommand(string command, bool valid)
        {
            if (valid)
            {
                Assert.DoesNotThrow(() =>
                {
                    _sut.Run(command);
                });
            }
            else
            {
                Assert.Throws<CommandException>(() =>
                {
                    _sut.Run(command);
                });
            }
        }

    }
}
