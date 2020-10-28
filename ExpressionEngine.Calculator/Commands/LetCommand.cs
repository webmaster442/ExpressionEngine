//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class LetCommand : CommandBase
    {
        public LetCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "let";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCount(2);

            if (NumberParser.ParseNumber(arguments[1], out double value))
            {
                currentState[arguments[0]] = value;
            }
            else
            {
                var parser = new ExpressionParser();
                var expression = parser.Parse(arguments[1], currentState);
                expression = expression?.Simplify();

                if (expression?.IsConstantExpression() == true)
                {
                    currentState[arguments[0]] = expression.Evaluate();
                }
                else
                {
                    currentState.SetExpression(arguments[0], expression);
                }
            }
        }
    }
}
