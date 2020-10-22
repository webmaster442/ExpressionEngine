//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.Calculator.Properties;
using System.Globalization;

namespace ExpressionEngine.Calculator.Commands
{
    internal class HelpCommand : CommandBase
    {
        public HelpCommand(IConsole console) : base(console)
        {
        }

        public override string Name => "help";

        public override void Execute(State currentState, Arguments arguments)
        {
            if (arguments.TryGetArgument(0, out string command))
            {
                var cmd = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command);

                var helpText = Resources.ResourceManager.GetString($"Help{cmd}");
                if (helpText == null)
                    throw new CalculatorException(Resources.ErrorNoHelpFound, command);

                Console.WriteLine(helpText);

            }
            else
            {
                Console.WriteLine(Resources.HelpHelp);
            }
        }
    }
}
