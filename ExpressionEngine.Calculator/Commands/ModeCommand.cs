//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.Calculator.Properties;

namespace ExpressionEngine.Calculator.Commands
{
    internal class ModeCommand : CommandBase
    {
        public ModeCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "mode";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCount(1);

            if (arguments.TryParse<AngleMode>(0, out AngleMode parsed))
            {
                currentState.AngleMode = parsed;
            }
            else
                throw new CalculatorException(Resources.ErrorUnknownMode, arguments[0]);
        }
    }
}
