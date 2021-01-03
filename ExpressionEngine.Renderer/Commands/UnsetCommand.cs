using ExpressionEngine.Renderer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Renderer.Commands
{
    internal class UnsetCommand : RendererCommandBase
    {
        public UnsetCommand(IWriter writer, IState state) : base(writer, state)
        {
        }

        public override void Execute(Arguments arguments)
        {
            if (arguments.Count ==0 )
            {
                State.Clear();
            }
        }
    }
}
