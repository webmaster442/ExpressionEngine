using ExpressionEngine.Calculator.Infrastructure;
using System;

namespace ExpressionEngine.Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConsole console = new ConsoleConsole();

            var calculator = new Calculator(console);
            calculator.Run();
        }
    }
}
