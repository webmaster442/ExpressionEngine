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

            return mock.Object;
        }
    }
}
