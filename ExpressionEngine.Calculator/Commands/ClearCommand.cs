//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class ClearCommand : CommandBase
    {
        public ClearCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "clear";

        public override void Execute(State currentState, Arguments arguments)
        {
            Console.Clear();
        }
    }
}
