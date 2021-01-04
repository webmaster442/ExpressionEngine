//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;

namespace ExpressionEngine.Renderer.Commands
{
    internal class UnsetCommand : RendererCommandBase
    {
        public UnsetCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            arguments.GuardArgumentCountMax(1);

            if (arguments.Count ==0 )
            {
                State.Clear();
            }
            else
            {
                State.Clear(arguments[0]);
            }
        }
    }
}
