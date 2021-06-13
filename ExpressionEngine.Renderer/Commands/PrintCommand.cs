//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;

namespace ExpressionEngine.Renderer.Commands
{
    internal class PrintCommand : RendererCommandBase
    {
        public PrintCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            arguments.GuardArgumentCount(1);

            IExpression? expression = State.GetExpression(arguments[0]);
            if (expression != null)
            {
                Writer.WriteLine(expression);
            }
            else
            {
                double number = State[arguments[0]];
                Writer.WriteLine(number);
            }
        }
    }
}
