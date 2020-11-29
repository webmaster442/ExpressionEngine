//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

namespace ExpressionEngine.Numbers
{
    public static class BigMath
    {
        public static readonly BigFloat Pi =   BigFloat.Parse("3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679", CultureInfo.InvariantCulture);
        public static readonly BigFloat E =    BigFloat.Parse("2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274", CultureInfo.InvariantCulture);

        public static BigFloat Abs(BigFloat value)
        {
            BigInteger num = BigInteger.Abs(value.Numerator);
            return new BigFloat(num, value.Denominator);
        }
        public static BigFloat Ceiling(BigFloat value)
        {
            BigInteger numerator = value.Numerator;
            BigInteger denominator = value.Denominator;
            if (numerator < 0)
                numerator -= BigInteger.Remainder(numerator, denominator);
            else
                numerator += denominator - BigInteger.Remainder(numerator, denominator);

            return new BigFloat(numerator, denominator);
        }

        public static BigFloat Floor(BigFloat value)
        {
            return Ceiling(value) - BigFloat.One;
        }

        public static BigFloat Truncate(BigFloat value)
        {
            BigInteger numerator = value.Numerator;
            numerator -= BigInteger.Remainder(numerator, value.Denominator);
            return new BigFloat(numerator, value.Denominator);
        }

        public static BigFloat Round(BigFloat value, int digits)
        {
            int i;
            BigFloat b, e, j, m;
            b = value;
            for (i = 0; b >= 1; ++i)
                b /= new BigFloat(10);

            int targetDigits = digits +1 - i;
            b = value;
            b *= InternalMath.Pow(new BigFloat(10), targetDigits);
            e = b + new BigFloat(1, 2);
            if (e == Ceiling(b))
            {
                BigInteger f = Ceiling(b).Numerator;
                BigInteger h = f - new BigInteger(2);
                if (h % 2 != 0)
                {
                    e -= BigFloat.One;
                }
            }
            j = Floor(e);
            m = InternalMath.Pow(new BigFloat(10), targetDigits);
            return j / m;
        }
    }
}
