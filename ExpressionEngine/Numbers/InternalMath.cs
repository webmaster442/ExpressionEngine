//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

namespace ExpressionEngine.Numbers
{
    internal static class InternalMath
    {
		private static readonly BigFloat Ln2 = BigFloat.Parse("0.693147180559945309417232121458176568075500134360255254120680009493393621969694715605863326996418687", CultureInfo.InvariantCulture);

		private  static bool ShouldRound(BigInteger a)
        {
			return a.ToString().Length > (BigFloat.Precision - 1);
        }

		public static BigInteger Gcd(BigInteger a, BigInteger b)
		{
			return BigInteger.GreatestCommonDivisor(a, b);
		}

		public static BigInteger Lcm(BigInteger a, BigInteger b)
		{
			return (a * b) / Gcd(a, b);
		}

		public static BigFloat Root(BigFloat value, int desiredRoot)
		{
			if (desiredRoot == 1)
				return value;

			BigFloat x = value;
			BigFloat root = BigFloat.Zero;

			BigFloat desiredroot = new BigFloat(1, desiredRoot);
			for (int i = 0; i < 20; i++)
			{
				root = desiredroot * (x + (value / x));
				x = root;
			}

			return root;
		}

		public static BigFloat Pow(BigFloat value, int exponent)
		{
			BigInteger numerator = value.Numerator;
			BigInteger denominator = value.Denominator;
			if (numerator.IsZero)
			{
				return value;
			}
			else if (exponent < 0)
			{
				BigInteger savedNumerator = value.Numerator;
				numerator = BigInteger.Pow(denominator, -exponent);
				denominator = BigInteger.Pow(savedNumerator, -exponent);
			}
			else
			{
				numerator = BigInteger.Pow(numerator, exponent);
				denominator = BigInteger.Pow(denominator, exponent);
			}

			BigFloat res = new BigFloat(numerator, denominator);

			if (ShouldRound(numerator) || ShouldRound(denominator))
            {
				return BigMath.Round(res, (BigFloat.Precision - 2));
            }

			return res;
		}

		public static BigFloat Ln(BigFloat x)
		{
			if (x >= new BigFloat(2))
			{
				return BigMath.Round(Ln(x / new BigFloat(2)) + Ln2, BigFloat.Precision - 3);
			}
			// validate 0 < x < 2
			BigFloat @base = x - BigFloat.One;        // Base of the numerator; exponent will be explicit
			BigFloat den = BigFloat.One;              // Denominator of the nth term
			BigFloat sign = BigFloat.One;             // Used to swap the sign of each term
			BigFloat term = @base;       // First term
			BigFloat result = term;     // Kick it off

			for (int i = 0; i < 214; ++i)
			{
				den += BigFloat.One;
				sign *= BigFloat.MinusOne;
				term *= @base;
				result += sign * term / den;
			}

			return result;
		}
	}
}

