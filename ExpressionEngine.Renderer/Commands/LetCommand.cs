using ExpressionEngine.Renderer.Infrastructure;

namespace ExpressionEngine.Renderer.Commands
{
    internal class LetCommand : RendererCommandBase
    {
        public LetCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            arguments.GuardArgumentCount(2);
            if (NumberParser.ParseNumber(arguments[1], out INumber value))
            {
                State[arguments[0]] = value;
            }
            else
            {
                ParseAsExpression(arguments);
            }
        }

        private void ParseAsExpression(Arguments arguments)
        {
            var parser = new ExpressionParser();
            var expression = parser.Parse(arguments[1], State);
            expression = expression?.Simplify();

            if (expression?.IsConstantExpression() == true)
            {
                State[arguments[0]] = expression.Evaluate();
            }
            else
            {
                State.SetExpression(arguments[0], expression);
            }
        }
    }
}
