//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.Calculator.Properties;
using System.Reflection;

namespace ExpressionEngine.Calculator.Commands
{
    internal class VersionCommand : CommandBase
    {
        public VersionCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "version";

        public override void Execute(State currentState, Arguments arguments)
        {
            var asm = typeof(Program).Assembly;
            var version = asm.GetName().Version;
            var builddate = asm.GetCustomAttribute<AssemblyBuildDateAttribute>();
            if (version != null)
                Console.WriteLine("Version:\t{0}", version);

            if (builddate != null)
                Console.WriteLine("Build:  \t{0}", builddate.BuildDate);
        }
    }
}
