//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;
using ExpressionEngine.Renderer.Properties;

namespace ExpressionEngine.Renderer.Commands
{
    public class DeriveCommand : RendererCommandBase
    {
        public DeriveCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            arguments.GuardArgumentCount(2);

            IExpression? expression = State.GetExpression(arguments[0]);

            if (expression != null)
            {
                expression = expression.Differentiate(arguments[1])?.Simplify();
                State.SetExpression(arguments[0], expression);
            }
            else
                throw new CommandException(Resources.ErrorUnknownVariable, arguments[0]);

        }
    }
}
