using ExpressionEngine.Calculator.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public string this[int index]
        {
            get => _tokens[index];
        }

        public int Count
        {
            get => _tokens.Length;
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

        
        public bool TryParse<T>(int index, [MaybeNull] out T result)
        {
            result = default;

            if (index < 0 || index > _tokens.Length - 1)
            {
                return false;
            }

            if (typeof(T).IsEnum)
            {
                bool convert = Enum.TryParse(typeof(T), _tokens[index], true, out var obj);
                if (convert && obj != null)
                {
                    result = (T)obj;
                }
                return convert;
            }
            else
            {
                try
                {
                    result = (T)Convert.ChangeType(_tokens[index], typeof(T));
                    return true;
                }
                catch (Exception)
                {
                    result = default;
                    return false;
                }
            }
        }
    }
}
