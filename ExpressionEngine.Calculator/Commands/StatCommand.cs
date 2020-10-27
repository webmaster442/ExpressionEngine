//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.Maths;
using System.Collections.Generic;

namespace ExpressionEngine.Calculator.Commands
{
    internal class StatCommand : CommandBase
    {
        public StatCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "stat";

        public override void Execute(State currentState, Arguments arguments)
        {
            IEnumerable<double> numbers = arguments.Parse<double>(0, arguments.Count - 1);
            var result = Statistics.Statistic(numbers);
            Console.WriteLine(result.ToString());
        }
    }
}
