//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressionEngine.Calculator
{
    internal class Calculator
    {
        private State _currentState;
        private IConsole _console;

        private List<CommandBase> Commands;

        public Calculator(IConsole console)
        {
            _console = console;
            _currentState = new State();
            Commands = new List<CommandBase>();
            ConfigureCommands();
        }

        public void Run()
        {
            string? command = null;
            while (_currentState.CanRun)
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
                        cmd.Execute(_currentState, new Arguments(tokens.Skip(1)));
                    }
                    catch (Exception ex) when (ex is ExpressionEngineException
                                            || ex is CannotDifferentiateException
                                            || ex is CalculatorException)
                    {
                        _console.WriteLine("Error: {0}", ex.Message);
                    }
                    catch (Exception e)
                    {
                        _console.WriteLine("Unhandled exception in command: {0}", e.Message);
#if DEBUG
                        Debugger.Break();
#endif
                    }
                }
                Console.WriteLine();
            }
        }

        private void Prompt()
        {
            _console.Write("{0} >", _currentState.AngleMode);
        }

        private void ConfigureCommands()
        {
            var assembly = typeof(Calculator).Assembly;
            var commands = assembly
                            .GetTypes()
                            .Where(x => x.BaseType == typeof(CommandBase));

            foreach (var cmd in commands)
            {
                if (Activator.CreateInstance(cmd, _console) is CommandBase instance)
                {
                    Commands.Add(instance);
                }
            }
        }
    }
}
