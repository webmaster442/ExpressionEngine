using ExpressionEngine.Calculator.Commands;
using ExpressionEngine.Calculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                string[] tokens = _console.ReadTokens();
                command = tokens.Length > 0 ? tokens[0] : string.Empty;

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
            while (string.Equals(command, "exit", StringComparison.OrdinalIgnoreCase));
        }


        private void ConfigureCommands()
        {
            Commands = new CommandBase[]
            {

            };
        }
    }
}
