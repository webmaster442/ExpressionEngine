//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Numerics;

namespace ExpressionEngine.Numbers
{
    public class NumberMath
    {
        public static Number Abs(Number value)
        {
            BigInteger num = BigInteger.Abs(value.Numerator);
            return new Number(num, value.Denominator);
        }

        public static Number Ceiling(Number value)
        {
            BigInteger numerator = value.Numerator;
            BigInteger denominator = value.Denominator;
            if (numerator < 0)
                numerator -= BigInteger.Remainder(numerator, denominator);
            else
                numerator += denominator - BigInteger.Remainder(numerator, denominator);

            return new Number(numerator, denominator);
        }

        public static Number Floor(Number value)
        {
            return Ceiling(value) - Number.One;
        }

        public static Number Truncate(Number value)
        {
            BigInteger numerator = value.Numerator;
            numerator -= BigInteger.Remainder(numerator, value.Denominator);
            return new Number(numerator, value.Denominator);
        }

        public static Number Round(Number value, int digits)
        {
            int i;
            Number b, e, j, m;
            b = value;
            for (i = 0; b >= Number.One; ++i)
                b /= new Number(10);

            int targetDigits = digits + 1 - i;
            b = value;
            b *= NumberAlgorithms.Pow(new Number(10), targetDigits);
            e = b + new Number(1, 2);
            if (e == Ceiling(b))
            {
                BigInteger f = Ceiling(b).Numerator;
                BigInteger h = f - new BigInteger(2);
                if (h % 2 != 0)
                {
                    e -= Number.One;
                }
            }
            j = Floor(e);
            m = NumberAlgorithms.Pow(new Number(10), targetDigits);
            return j / m;
        }
    }
}
