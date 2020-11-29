using System.Numerics;

namespace ExpressionEngine.BigNumber
{
    internal static class InternalMath
    {
		public static BigInteger Gcd(BigInteger a, BigInteger b)
		{
            a = BigInteger.Abs(a);
			b = BigInteger.Abs(b);
			BigInteger result;
			if (a == BigInteger.One || b == BigInteger.One)
			{
				result = 1L;
			}
			else
			{
				while (true)
				{
					BigInteger num = a % b;
					if (num == BigInteger.Zero)
					{
						break;
					}
					a = b;
					b = num;
				}
				result = b;
			}
			return result;
		}

		public static BigInteger Lcm(BigInteger a, BigInteger b)
		{
			return (a * b) / Gcd(a, b);
		}

		public static BigFloat Root(BigFloat value, int n)
		{
			if (n == 1)
				return value;

			BigFloat x = value;
			BigFloat root = BigFloat.Zero;

			BigFloat desiredroot = new BigFloat(1, n);
			for (int i = 0; i < 15; i++)
			{
				root = desiredroot * (x + (value / x));
				x = root;
			}

			return root;
		}
	}
}
