//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;

namespace ExpressionEngine.Calculator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConsole console = new ConsoleConsole();

            var calculator = new Calculator(console);
            calculator.Run();
        }
    }
}
