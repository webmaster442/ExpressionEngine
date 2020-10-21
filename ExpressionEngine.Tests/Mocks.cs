using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionEngine.Tests
{
    internal class Mocks
    {
        public static IVariables CreateVariableMock()
        {
            Mock<IVariables> mock = new Mock<IVariables>(MockBehavior.Strict);

            mock.Setup(x => x.IsConstant("x")).Returns(false);
            mock.Setup(x => x.IsConstant("e")).Returns(true);
            mock.Setup(x => x.IsConstant("pi")).Returns(true);

            mock.Setup(x => x.GetValue("e")).Returns(Math.E);
            mock.Setup(x => x.GetValue("pi")).Returns(Math.PI);

            return mock.Object;
        }
    }
}
