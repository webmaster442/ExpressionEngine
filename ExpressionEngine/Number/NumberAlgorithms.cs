//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Numerics;

namespace ExpressionEngine.Numbers
{
    internal static class NumberAlgorithms
    {
        public static NumberState GetNumberState(double doubleValue)
        {
            if (double.IsNaN(doubleValue))
                return NumberState.NaN;
            else if (double.IsPositiveInfinity(doubleValue))
                return NumberState.PositiveInfinity;
            else if (double.IsNegativeInfinity(doubleValue))
                return NumberState.NegativeInfinity;
            else
                return NumberState.Value;
        }

        public static bool TryHandleSpecialCase(Number a, Number b, out Number result)
        {
            if (a.State == NumberState.NaN || b.State == NumberState.NaN)
            {
                result = new Number(NumberState.NaN);
                return true;
            }
            else if (a.State == NumberState.NegativeInfinity || b.State == NumberState.NegativeInfinity)
            {
                result = new Number(NumberState.NegativeInfinity);
                return true;
            }
            else if (a.State == NumberState.PositiveInfinity || b.State == NumberState.PositiveInfinity)
            {
                result = new Number(NumberState.PositiveInfinity);
                return true;
            }
            else
            {
                result = a;
                return false;
            }
        }

        public static bool TryHandleSpecialToString(Number a, out string result)
        {
            if (a.State == NumberState.NaN)
            {
                result = "NaN";
                return true;
            }
            else if (a.State == NumberState.NegativeInfinity)
            {
                result = "-∞";
                return true;
            }
            else if (a.State == NumberState.PositiveInfinity)
            {
                result = "∞";
                return true;
            }
            else
            {
                result = string.Empty;
                return false;
            }
        }

        public static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            return BigInteger.GreatestCommonDivisor(a, b);
        }

        private static bool ShouldRound(BigInteger a)
        {
            return a.ToString().Length > (Number.Precision - 1);
        }

        public static Number Root(Number value, int desiredRoot)
        {
            if (desiredRoot == 1)
                return value;

            Number x = value;
            Number root = Number.Zero;

            Number desiredroot = new Number(1, desiredRoot);
            for (int i = 0; i < 10; i++)
            {
                root = desiredroot * (x + (value / x));
                x = root;
            }

            return root;
        }

        public static Number Pow(Number value, int exponent)
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

            Number res = new Number(numerator, denominator);

            if (ShouldRound(numerator) || ShouldRound(denominator))
            {
                return NumberMath.Round(res, (Number.Precision - 2));
            }

            return res;
        }
    }
}
