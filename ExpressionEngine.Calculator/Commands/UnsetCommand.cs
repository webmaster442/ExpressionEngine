//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class UnsetCommand : CommandBase
    {
        public UnsetCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "unset";

        public override void Execute(State currentState, Arguments arguments)
        {
            if (arguments.Count == 0)
            {
                currentState.Clear();
            }
            else
            {
                currentState.Clear(arguments[0]);
            }
        }
    }
}
