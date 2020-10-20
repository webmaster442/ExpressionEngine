using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
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
