//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Commands;
using ExpressionEngine.Renderer.Infrastructure;
using ExpressionEngine.Renderer.Internals;
using ExpressionEngine.Renderer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpressionEngine.Renderer
{
    public class ExpressionRenderer
    {
        private readonly IWriter _outputWriter;
        private readonly IState _state;
        private readonly Dictionary<string, RendererCommandBase> _commands;

        public ExpressionRenderer(IWriter outputWriter)
        {
            _outputWriter = outputWriter;
            _state = new State();
            _commands = new Dictionary<string, RendererCommandBase>();
            ConfigureCommands();
        }

        public void RegisterCommand<T>() where T: RendererCommandBase
        {
            try
            {
                var command = typeof(T);
                if (Activator.CreateInstance(command, _outputWriter, _state) is RendererCommandBase instance)
                {
                    _commands.Add(GetName(command), instance);
                }
            }
            catch (Exception ex)
            {
                throw new CommandException("RegisterCommand failed", ex);
            }
        }

        public void Run(string commands)
        {
            using (var stringReader = new StringReader(commands))
            {
                string? line;
                do
                {
                    line = stringReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }

                    string[] tokens = line.Tokenize();
                    if (tokens.Length == 0)
                        throw new CommandException(Resources.ErrorEmptyCommand);

                    if (!_commands.ContainsKey(tokens[0]))
                        throw new CommandException(Resources.ErrorUnknownCommand, tokens[0]);

                    try
                    {
                        _commands[tokens[0]].Execute(new Arguments(tokens.Skip(1)));
                    }
                    catch (Exception ex) when (ex is ExpressionEngineException
                                      || ex is CannotDifferentiateException)
                    {
                        throw new CommandException("Error", ex);
                    }

                }
                while (line != null);
            }
        }

        private void ConfigureCommands()
        {
            var assembly = typeof(ExpressionRenderer).Assembly;
            var commands = assembly
                            .GetTypes()
                            .Where(x => x.BaseType == typeof(RendererCommandBase));

            foreach (var cmd in commands)
            {
                if (Activator.CreateInstance(cmd, _outputWriter, _state) is RendererCommandBase instance)
                {
                    _commands.Add(GetName(cmd), instance);
                }
            }
        }

        private static string GetName(Type cmd)
        {
            return cmd.Name.ToLower().Replace("command", "");
        }
    }
}
