//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
            else if (State.IsExpression(arguments[1]))
            {
                CopyExpression(arguments[0], arguments[1]);
            }
            else if (State.IsDefined(arguments[1]))
            {
                State[arguments[0]] = State[arguments[1]].Clone();
            }
            else
            {
                ParseAsExpression(arguments);
            }
        }

        private void CopyExpression(string target, string source)
        {
            ExpressionParser parser = new ExpressionParser();
            var txt = State.GetExpression(source)?.ToString() ?? "";
            IExpression? parsed = parser.Parse(txt, State);
            State.SetExpression(target, parsed);
        }

        private void ParseAsExpression(Arguments arguments)
        {
            var expression = ParseExpression(arguments[1]);
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
