using ExpressionEngine.Calculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionEngine.Calculator.Commands
{
    internal class ExitCommand : CommandBase
    {
        public ExitCommand(IConsole console) : base(console)
        {
        }

        public override string Name => "exit";

        public override void Execute(State currentState, Arguments arguments)
        {
            currentState.CanRun = false;
        }
    }
}
