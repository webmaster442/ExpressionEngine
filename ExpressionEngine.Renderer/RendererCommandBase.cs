//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;

namespace ExpressionEngine.Renderer.Commands
{
    public abstract class RendererCommandBase
    {
        protected IWriter Writer { get; }
        protected IState State { get; }

        public RendererCommandBase(IWriter writer, IState state)
        {
            Writer = writer;
            State = state;
        }

        protected IExpression? ParseExpression(string expression)
        {
            ExpressionParser parser = new ExpressionParser();
            return parser.Parse(expression, State);
        }

        public abstract void Execute(Arguments arguments);
    }
}
