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

        public bool TryGetArgument(int index, out string value)
        {
            value = string.Empty;
            if (index > -1 && index <= _tokens.Length - 1)
            {
                value = _tokens[index];
                return true;
            }
            return false;
        }

        public string Raw
        {
            get => string.Join(' ', _tokens);
        }

        public void GuardArgumentCount(int count)
        {
            if (_tokens.Length != count)
                throw new CalculatorException(Resources.ErrorArgumentCount, count.ToString(), _tokens.Length.ToString());

            bool isNull = _tokens.Any(t => string.IsNullOrEmpty(t));
            if (isNull)
                throw new CalculatorException(Resources.ErrorArgumentCount, count.ToString(), _tokens.Length.ToString());

        }
    }
}
