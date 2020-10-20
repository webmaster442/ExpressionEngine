//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator
{
    internal class ConsoleConsole : IConsole
    {
        public string[] ReadTokens()
        {
            throw new System.NotImplementedException();
        }

        public void Write(string format, params object[] arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}