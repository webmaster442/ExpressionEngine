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
            _sut.Run("let test 3");
            _sut.Run("let expr test*2");
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

        [Test]
        public void TestLetCopyVar()
        {
            _sut.Run("let x 88");
            _sut.Run("let copy x");

            Assert.AreEqual("88", _sut.State["copy"].ToString());
        }

        [Test]
        public void TestLetCopyExpression()
        {
            _sut.Run("let asd sin(x)*2");
            _sut.Run("let asdcopy asd");

            Assert.AreEqual("(sin(x) * 2)", _sut.State.GetExpression("asdcopy").ToString());
        }

        [TestCase("eval 33+22", "55")]
        [TestCase("eval expr", "6")]
        public void TestEvalCommand(string command, string expected)
        {
            _sut.Run(command);
            _writerMock.Verify(x => x.WriteLine(It.IsAny<object>()), Times.Once);
            Assert.AreEqual(expected, _sut.State.Ans.ToString());
        }

        [TestCase("deg")]
        [TestCase("rad")]
        [TestCase("grad")]
        public void TestValidModes(string mode)
        {
            Assert.DoesNotThrow(() =>
            {
                _sut.Run("mode " + mode);
            });
        }

        [TestCase("")]
        [TestCase("asd")]
        [TestCase("32")]
        public void TestInvalidModes(string mode)
        {
            Assert.Throws<CommandException>(() =>
            {
                _sut.Run("mode " + mode);
            });
        }


        [Test]
        public void TestUnsetAll()
        {
            Assert.AreEqual(4, _sut.State.Count);

            _sut.Run("unset");

            Assert.AreEqual(2, _sut.State.Count);
        }

        [Test]
        public void TestUnsetX()
        {
            _sut.Run("let x 1");

            Assert.AreEqual(5, _sut.State.Count);

            _sut.Run("unset x");

            Assert.AreEqual(4, _sut.State.Count);
        }
    }
}
