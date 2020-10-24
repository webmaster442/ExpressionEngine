using Moq;
using System;

namespace ExpressionEngine.Tests
{
    internal static class Mocks
    {
        private static double XValue = 0;

        public static IVariables CreateVariableMock()
        {
            Mock<IVariables> mock = new Mock<IVariables>(MockBehavior.Strict);

            mock.Setup(x => x.IsConstant("x")).Returns(false);
            mock.Setup(x => x.IsConstant("e")).Returns(true);
            mock.Setup(x => x.IsConstant("pi")).Returns(true);

            mock.Setup(x => x.GetValue("e")).Returns(Math.E);
            mock.Setup(x => x.GetValue("pi")).Returns(Math.PI);

            mock.Setup(x => x.GetValue("y")).Returns(90.0);

            mock.Setup(x => x.GetValue("x")).Returns((string input) => XValue);
            mock.SetupSet(x => x["x"] = It.IsAny<double>()).Callback((string name, double value) => 
            {
                XValue = value;
            });

            return mock.Object;
        }
    }
}
