//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal static class ResultFormatter
    {
        public static string ToString(double d, bool programmer)
        {
            if (programmer)
                return Programmer(d);
            else
                return d.ToString(CultureInfo.InvariantCulture);
        }

        private static string Programmer(double d)
        {
            bool isInteger = (d % 1) == 0;

            if (!isInteger)
                return PrintDouble(d);
            else
                return PrintInteger(Convert.ToInt64(d));

        }

        private static string PrintInteger(long v)
        {
            int bits = GetBits(v);
            string hex = Convert.ToString(v, 16).PadLeft(bits / 4, '0');
            string oct = Convert.ToString(v, 8).PadLeft(bits / 3, '0');
            string bin = Convert.ToString(v, 2).PadLeft(bits, '0');
            return $"DEC: {v}\r\nHEX: {hex}\r\nOCT: {oct}\r\nBIN: {bin}";
        }

        private static string PrintDouble(double d)
        {
            var bytes = BitConverter.GetBytes(d).Select(b => Convert.ToString(b, 16));

            return $"DEC: {d}\r\nHEX: {string.Join("", bytes)}";
        }

        private static int GetBits(long v)
        {
            v = Math.Abs(v);

            if (v > int.MaxValue) return 64;
            else if (v > short.MaxValue) return 32;
            else if (v > byte.MaxValue) return 16;
            else return 8;
        }
    }
}
