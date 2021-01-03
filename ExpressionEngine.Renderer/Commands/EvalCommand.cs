//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;
using ExpressionEngine.Renderer.Properties;

namespace ExpressionEngine.Renderer.Commands
{
    internal class EvalCommand : RendererCommandBase
    {
        public EvalCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            IExpression? expression;
            if (State.IsExpression(arguments[0]))
            {
                expression = State.GetExpression(arguments[0]);
            }
            else
            {
                ExpressionParser parser = new ExpressionParser();
                expression = parser.Parse(arguments[0], State);
            }
            INumber? result = expression?.Evaluate();
            if (result == null)
            {
                throw new CommandException(Resources.ErrorEvaluate, expression?.ToString() ?? "no expression");
            }
            Writer.WriteLine(result);
            State.Ans = result;
        }
    }
}
