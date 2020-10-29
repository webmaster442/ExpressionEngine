//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionEngine.Maths
{
    /// <summary>
    /// Number system conversion
    /// </summary>
    public static class NumberSystemConverter
    {
        private static int GetVaueFromSymbol(char symbol)
        {
            if (char.IsNumber(symbol))
                return symbol - (int)'0';
            else if (char.IsUpper(symbol))
                return (symbol - (int)'A') + 10;
            else
                return (symbol - (int)'a') + 10;
        }

        private static char ValueToSymbol(int value)
        {
            if (value > -1 && value < 10)
                return value.ToString()[0];
            else
            {
                return (char)((int)'a' + (value - 10));
            }
        }

        private static string ToString(Stack<char> symbols)
        {
            StringBuilder sb = new StringBuilder();
            while (symbols.Count > 0)
            {
                sb.Append(char.ToUpper(symbols.Pop()));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert a number in base 10 to a target system
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="targetSystem">target number system</param>
        /// <returns>value in system</returns>
        public static string ToSystem(long value, int targetSystem)
        {
            if (targetSystem < 2 || targetSystem > 36)
                throw new ExpressionEngineException(string.Format(Resources.NumberSystemRangeError, nameof(targetSystem)));

            Stack<char> symbols = new Stack<char>();
            while (value > 0)
            {
                int digit = (int)(value % targetSystem);
                symbols.Push(ValueToSymbol(digit));
                value /= targetSystem;
            }

            return ToString(symbols);
        }

        /// <summary>
        /// Converts a number from a given system to base 10
        /// </summary>
        /// <param name="input">input number</param>
        /// <param name="sourceSystem">number system</param>
        /// <returns></returns>
        public static long FromSystem(string input, int sourceSystem)
        {
            if (sourceSystem < 2 || sourceSystem > 36)
                throw new ExpressionEngineException(string.Format(Resources.NumberSystemRangeError,  nameof(sourceSystem)));

            int exponent = 0;
            long value = 0;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                value += GetVaueFromSymbol(input[i]) * (long)Math.Pow(sourceSystem, exponent);
                ++exponent;
            }

            return value;
        }
    }
}
