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
	}
}
