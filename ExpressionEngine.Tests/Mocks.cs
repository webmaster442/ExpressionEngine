//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using Moq;

namespace ExpressionEngine.Tests
{
    internal static class Mocks
    {
        private static Number XValue = 0;
        private static Number AValue = 0;
        private static Number BValue = 0;

        public static IVariables CreateVariableMock()
        {
            Mock<IVariables> mock = new Mock<IVariables>(MockBehavior.Strict);

            mock.Setup(x => x.IsConstant("x")).Returns(false);
            mock.Setup(x => x.IsConstant("y")).Returns(false);
            mock.Setup(x => x.IsConstant("a")).Returns(false);
            mock.Setup(x => x.IsConstant("b")).Returns(false);
            mock.Setup(x => x.IsConstant("e")).Returns(true);
            mock.Setup(x => x.IsConstant("pi")).Returns(true);

            mock.Setup(x => x.GetValue("e")).Returns(NumberMath.E);
            mock.Setup(x => x.GetValue("pi")).Returns(NumberMath.Pi);
            mock.Setup(x => x.GetValue("y")).Returns(new Number(90));

            mock.Setup(x => x.GetValue("x")).Returns((string input) => XValue);
            mock.SetupSet(x => x["x"] = It.IsAny<INumber>()).Callback((string name, INumber value) => 
            {
                XValue = value as Number;
            });

            mock.Setup(x => x.GetValue("a")).Returns((string input) => AValue);
            mock.SetupSet(x => x["a"] = It.IsAny<INumber>()).Callback((string name, INumber value) =>
            {
                AValue = value as Number;
            });

            mock.Setup(x => x.GetValue("b")).Returns((string input) => BValue);
            mock.SetupSet(x => x["b"] = It.IsAny<INumber>()).Callback((string name, INumber value) =>
            {
                BValue = value as Number;
            });

            return mock.Object;
        }
    }
}
