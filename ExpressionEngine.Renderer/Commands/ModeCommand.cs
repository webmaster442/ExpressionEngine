//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Infrastructure;
using ExpressionEngine.Renderer.Properties;

namespace ExpressionEngine.Renderer.Commands
{
    public class ModeCommand : RendererCommandBase
    {
        public ModeCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            if (arguments.Count == 0)
            {
                Writer.WriteLine(ExpressionParser.AngleMode);
            }
            if (arguments.TryParse<AngleMode>(0, out AngleMode result))
            {
                ExpressionParser.AngleMode = result;
            }
            else
            {
                throw new CommandException(Resources.ErrorUnknownMode);
            }
        }
    }
}
