//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.Maths;

namespace ExpressionEngine.Calculator.Commands
{
    internal class SysConvCommand : CommandBase
    {
        public SysConvCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "sysconv";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCount(3);

            int sourceSystem = arguments.Parse<int>(1);
            int targetSystem = arguments.Parse<int>(2);

            long source = NumberSystemConverter.FromSystem(arguments[0], sourceSystem);
            string result = NumberSystemConverter.ToSystem(source, targetSystem);

            Console.WriteLine(result);
        }
    }
}
