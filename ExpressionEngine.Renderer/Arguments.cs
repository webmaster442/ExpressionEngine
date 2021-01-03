//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Properties;
//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ExpressionEngine.Renderer.Infrastructure
{
    public class Arguments
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
                throw new CommandException(Resources.ErrorArgumentCount, count.ToString(), _tokens.Length.ToString());

            bool isNull = _tokens.Any(t => string.IsNullOrEmpty(t));
            if (isNull)
                throw new CommandException(Resources.ErrorArgumentCount, count.ToString(), _tokens.Length.ToString());

        }

        public void GuardArgumentCountMin(int minimum)
        {
            if (_tokens.Length < minimum)
                throw new CommandException(Resources.ErrorArgumentCountMin, minimum.ToString(), _tokens.Length.ToString());

            for (int i = 0; i < minimum; i++)
            {
                if (string.IsNullOrEmpty(_tokens[i]))
                    throw new CommandException(Resources.ErrorArgumentCountMin, minimum.ToString(), _tokens.Length.ToString());
            }
        }

        public IEnumerable<T> Parse<T>(int start, int end)
        {
            if (end > -1)
            {
                for (int i = start; i <= end; i++)
                {
                    yield return Parse<T>(i);
                }
            }
        }

        public T Parse<T>(int index)
        {
            var call = TryParse<T>(index, out T local, false);
            return local!;
        }

        public bool TryParse<T>(int index, [MaybeNull] out T result)
        {
            return TryParse<T>(index, out result, true);
        }

        private bool TryParse<T>(int index, [MaybeNull] out T result, bool handle)
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
                if (handle)
                    return convert;
                else
                    throw new CommandException(Resources.ErrorParsing, typeof(T).Name);
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
                    if (handle)
                    {
                        result = default;
                        return false;
                    }
                    else
                        throw new CommandException(Resources.ErrorParsing, typeof(T).Name);
                }
            }
        }
    }
}
