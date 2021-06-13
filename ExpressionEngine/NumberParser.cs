//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExpressionEngine
{
    public class NumberParser
    {
        private static readonly HashSet<char> NumberTokens = new HashSet<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'x', 'b', '.', '_', '-', '+'
        };

        public static bool ParseNumber(string number, out double d)
        {
            number = number.ToLower().Replace("_", "");
            if (!number.All(c => NumberTokens.Contains(c)))
            {
                d = double.NaN;
                return false;
            }

            if (number.StartsWith("0b"))
                return ParseInt(number[2..], 2, out d);
            else if (number.StartsWith("0x"))
                return ParseInt(number[2..], 16, out d);
            else if (number.StartsWith("0"))
                return ParseInt(number[1..], 8, out d);

           return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out d);
        }

        private static bool ParseInt(string number, int system, out double d)
        {
            try
            {
                long result = Convert.ToInt64(number, system);
                d = result;
                return true;
            }
            catch (Exception)
            {
                d = double.NaN;
                return false;
            }
        }
    }
}
