//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using ExpressionEngine.LogicExpressions;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine.Calculator.Commands
{
    internal class SimplifyCommand : CommandBase
    {
        private enum Mode
        {
            Minterm, Maxterm,
        }

        private enum BitOrder
        {
            LsbA, MsbA,
        }

        public SimplifyCommand(IConsole console, IHost host) : base(console, host)
        {
        }

        public override string Name => "simplify";

        public override void Execute(State currentState, Arguments arguments)
        {
            arguments.GuardArgumentCountMin(3);

            Mode m = arguments.Parse<Mode>(0);
            BitOrder bitOrder = arguments.Parse<BitOrder>(1);

            QuineMcCluskeyConfig config = new QuineMcCluskeyConfig
            {
                AIsLsb = bitOrder == BitOrder.LsbA,
                Negate = m == Mode.Maxterm
            };

            if (arguments.Count == 3)
            {
                HandleExpression(currentState, arguments[3], config);
            }
            else
            {
                IEnumerable<int>? terms = arguments.Parse<int>(3, arguments.Count);
                HandleTerms(currentState, terms, config);
            }
        }

        private void HandleExpression(State currentState, string expression, QuineMcCluskeyConfig config)
        {
            string result = string.Empty;

            if (currentState.TryGetExpression(expression, out IExpression? stored)
                && stored != null)
            {
                var minterms = stored.GetMinterms();
                var variables = LogicFunctions.GetVariableCount(minterms);
                result = QuineMcclusky.GetSimplified(minterms, Enumerable.Empty<int>(), variables, config);
            }
            else
            {
                ExpressionParser parser = new ExpressionParser();
                IExpression? parsed = parser.Parse(expression, currentState);
                if (parsed != null)
                {
                    parsed.GetMinterms();
                    var minterms = parsed.GetMinterms();
                    var variables = LogicFunctions.GetVariableCount(minterms);
                    result = QuineMcclusky.GetSimplified(minterms, Enumerable.Empty<int>(), variables, config);
                }
            }
            Console.WriteLine(result);
        }

        private void HandleTerms(State currentState, IEnumerable<int> terms, QuineMcCluskeyConfig config)
        {
            int variables = LogicFunctions.GetVariableCount(terms);
            string result = QuineMcclusky.GetSimplified(terms, Enumerable.Empty<int>(), variables, config);
            Console.WriteLine(result);
        }
    }
}
