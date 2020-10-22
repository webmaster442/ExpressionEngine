//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal abstract class CommandBase
    {
        protected IConsole Console { get; }
        protected IHost Host { get; }

        public CommandBase(IConsole console, IHost host)
        {
            Console = console;
            Host = host;
        }

        public abstract string Name { get; }

        public abstract void Execute(State currentState, Arguments arguments);
    }
}
