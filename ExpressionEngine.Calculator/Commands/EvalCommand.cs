//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator.Commands
{
    internal class EvalCommand : CommandBase
    {
        public EvalCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "eval";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCount(1);

            if (currentState.TryGetExpression(arguments[0], out var storedExpression)
                && storedExpression != null)
            {
                Console.WriteLine("{0}", storedExpression.Evaluate());
                return;
            }

            var parser = new ExpressionParser();
            IExpression? expression = parser.Parse(arguments[0], currentState);
            if (expression != null)
            {
                Console.WriteLine("{0}", expression.Evaluate());
            }

        }
    }
}
