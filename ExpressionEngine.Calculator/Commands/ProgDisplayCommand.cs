//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class ProgDisplayCommand : CommandBase
    {
        public ProgDisplayCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "progdisplay";

        public override void Execute(State currentState, Arguments arguments)
        {
            currentState.Programmer = !currentState.Programmer;
        }
    }
}
