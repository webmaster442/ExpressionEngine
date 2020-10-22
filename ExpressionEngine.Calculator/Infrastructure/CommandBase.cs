//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal abstract class CommandBase
    {
        protected IConsole Console { get; }

        public CommandBase(IConsole console)
        {
            Console = console;
        }

        public abstract string Name { get; }

        public abstract void Execute(State currentState, string[] arguments);
    }
}
