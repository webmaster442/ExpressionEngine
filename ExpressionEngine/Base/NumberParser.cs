//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpressionEngine.Base
{
    internal class NumberParser
    {
        public static readonly HashSet<char> NumberTokens = new HashSet<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'h', 'b', '.', '_', '-', '+'
        };

        public static bool IsNumberToken(char c)
        {
            return NumberTokens.Contains(c);
        }

        public static double ParseNumber(string number)
        {
            number = number.ToLower().Replace("_", "");
            if (!number.All(c => IsNumberToken(c)))
            {
                throw new ExpressionEngineException(Resources.NumberParseError);
            }

            if (number.StartsWith("0b"))
                return ParseInt(number[2..], 2);
            else if (number.StartsWith("0x"))
                return ParseInt(number[2..], 16);
            else if (number.StartsWith("0"))
                return ParseInt(number[1..], 8);

            try
            {
                return double.Parse(number, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new ExpressionEngineException(Resources.NumberParseError, e);
            }


        }

        private static long ParseInt(string number, int system)
        {
            try
            {
                return Convert.ToInt64(number, system);
            }
            catch (Exception e)
            {
                throw new ExpressionEngineException(Resources.NumberParseError, e);
            }
        }
    }
}
