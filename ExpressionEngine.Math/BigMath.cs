using System;
using System.Globalization;
using System.Numerics;

namespace ExpressionEngine.BigNumber
{
    public static class BigMath
    {
        public static readonly BigFloat Pi = BigFloat.Parse("3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679", CultureInfo.InvariantCulture);
        public static readonly BigFloat E = BigFloat.Parse("2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274", CultureInfo.InvariantCulture);

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

        public static BigFloat Log10(BigFloat value)
        {
            var v = BigInteger.Log10(value.Numerator) - BigInteger.Log10(value.Denominator);
            return (BigFloat)v;
        }

        public static BigFloat Log(BigFloat value, double baseValue)
        {
            var v = BigInteger.Log(value.Numerator, baseValue) - BigInteger.Log(value.Denominator, baseValue);
            return (BigFloat)v;
        }

        public static BigFloat Root(BigFloat value, int n)
        {
            if (n == 1)
                return value;

            BigFloat x = value;
            BigFloat root = BigFloat.Zero;

            BigFloat desiredroot = new BigFloat(1, n);
            for (int i=0; i<15; i++)
            {
                root = desiredroot * (x + (value / x));
                x = root;
            }

            return root;
        }
    }
}
