//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace ExpressionEngine.Calculator.Infrastructure
{
    interface IConsole
    {
        int CurrentWidth { get; }

        string Prompt { get; set; }

        Func<string, int, string[]>? AutocompleteLookup { get; set; }

        string[] ReadTokens();
        void Write(string format, params object[] arguments);
        void WriteLine(string format, params object[] arguments) =>
            Write(format + "\r\n", arguments);

        void WriteLine();

        void Clear();
    }
}
