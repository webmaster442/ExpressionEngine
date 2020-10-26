//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using System.Linq;

namespace ExpressionEngine.Calculator.Commands
{
    internal class CmdListCommand : CommandBase
    {
        public CmdListCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "cmdlist";

        public override void Execute(State currentState, Arguments arguments)
        {
            const int textWidth = 20;
            int i = 0;
            int columns = (Console.CurrentWidth - 1) / textWidth;
            foreach (var cmd in Host.Commands.OrderBy(x => x))
            {
                if (i % columns == 0)
                {
                    Console.Write("\r\n");
                }
                Console.Write("{0,-" + textWidth + "} ", cmd);
                ++i;
            }
        }
    }
}
