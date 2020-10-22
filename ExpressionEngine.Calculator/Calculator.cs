//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using System;
using System.Linq;

namespace ExpressionEngine.Calculator
{
    internal class Calculator
    {
        private State _currentState;
        private IConsole _console;

        private CommandBase[] Commands;

        public Calculator(IConsole console)
        {
            _console = console;
            _currentState = new State();
            ConfigureCommands();
        }

        public void Run()
        {
            string? command = null;
            do
            {
                Prompt();
                string[] tokens = _console.ReadTokens();
                command = tokens.Length > 0 ? tokens[0] : string.Empty;

                if (string.IsNullOrEmpty(command)) continue;

                CommandBase? cmd = Commands
                    .Where(c => string.Equals(c.Name, command, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (cmd == null)
                {
                    _console.WriteLine("Unknown Command: {0}", tokens[0]);
                }
                else
                {
                    try
                    {
                        cmd.Execute(_currentState, tokens.Skip(1).ToArray());
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            while (!string.Equals(command, "exit", StringComparison.OrdinalIgnoreCase));
        }

        private void Prompt()
        {
            _console.Write("{0} >", _currentState.AngleMode);
        }

        private void ConfigureCommands()
        {
            Commands = new CommandBase[]
            {

            };
        }
    }
}
