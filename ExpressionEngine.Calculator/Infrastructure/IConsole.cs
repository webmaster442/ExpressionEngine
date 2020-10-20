using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionEngine.Calculator.Infrastructure
{
    interface IConsole
    {
        string[] ReadTokens();
        void Write(string format, params object[] arguments);
        void WriteLine(string format, params object[] arguments) =>
            Write(format + "\r\n", arguments);
    }
}
