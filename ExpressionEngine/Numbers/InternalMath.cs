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
			for (int i = 0; i < 15; i++)
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
	}
}

