using ExpressionEngine.Calculator.Properties;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal class Arguments
    {
        private string[] _tokens;

        public Arguments(IEnumerable<string> tokens)
        {
            _tokens = tokens.ToArray();
        }

        public string this [int index]
        {
            get => _tokens[index];
        }

        public void GuardArgumentCount(int count)
        {
            if (_tokens.Length != count)
                throw new CalculatorException(Resources.ErrorArgumentCount);

            bool isNull = _tokens.Any(t => string.IsNullOrEmpty(t));
            if (isNull)
                throw new CalculatorException(Resources.ErrorArgumentCount);

        }
    }
}
