//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class LetCommand : CommandBase
    {
        public LetCommand(IConsole console) : base(console)
        {
        }

        public override string Name => "let";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCount(2);

            var parser = new ExpressionParser();
            var expression = parser.Parse(arguments[1], currentState);

            expression = expression?.Simplify();

            if (expression?.IsConstantExpression() == true)
            {
                var value = expression.Evaluate();
                currentState[arguments[0]] = value;
            }
            else
            {
                currentState.SetExpression(arguments[0], expression);
            }
        }
    }
}
