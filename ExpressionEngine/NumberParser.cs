//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace ExpressionEngine
{
    public static class NumberParser
    {
        private static readonly HashSet<char> NumberTokens = new HashSet<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'x', 'b', '.', '_', '-', '+'
        };

        public static bool ParseNumber(string number, out INumber d)
        {
            number = number.ToLower().Replace("_", "");
            if (!number.All(c => NumberTokens.Contains(c)))
            {
                d = new Number(NumberState.NaN);
                return false;
            }

            if (number.StartsWith("0b"))
                return ParseInt(number[2..], 2, out d);
            else if (number.StartsWith("0x"))
                return ParseInt(number[2..], 16, out d);
            else if (number.StartsWith("0"))
                return ParseInt(number[1..], 8, out d);

            bool result = Number.TryParse(number, CultureInfo.InvariantCulture, out Number n);
            d = n;
            return result;
        }

        private static bool ParseInt(string number, int system, out INumber d)
        {
            try
            {
                BigInteger result = ParseBigInteger(number, system);
                d = new Number(result);
                return true;
            }
            catch (Exception)
            {
                d = new Number(NumberState.NaN);
                return false;
            }
        }

        private static int ValueOf(char digit)
        {
            if (digit >= '0' && digit <= '9')
                return digit - '0';
            else if (digit >= 'a' && digit <= 'f')
                return (digit - 'a') + 10;
            else
                return (digit - 'A') + 10;
        }

        public static BigInteger ParseBigInteger(string value, int baseOfValue)
        {
            return value.Aggregate(new BigInteger(), (current, digit) => current * baseOfValue + ValueOf(digit));
        }
    }
}
