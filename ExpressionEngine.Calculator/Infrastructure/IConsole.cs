//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Calculator.Infrastructure
{
    interface IConsole
    {
        string[] ReadTokens();
        void Write(string format, params object[] arguments);
        void WriteLine(string format, params object[] arguments) =>
            Write(format + "\r\n", arguments);

        void Clear();
    }
}
