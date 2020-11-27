using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ExpressionEngine.BigNumber
{
    public sealed class BigFloat : IComparable<BigFloat?>, IEquatable<BigFloat?>
    {
        public const int Precision = 100;

        public BigInteger Numerator
        {
            get;
        }

        public BigInteger Denominator
        {
            get;
        }

        public int Sign
        {
            get
            {
                return (Numerator.Sign + Denominator.Sign) switch
                {
                    2 or -2 => 1,
                    0 => -1,
                    _ => 0,
                };
            }
        }

        public BigFloat(BigInteger numerator, BigInteger denominator)
        {
            bool flag = denominator == 0L;
            if (flag)
            {
                throw new ArgumentException($"{denominator} can't be 0");
            }
            BigInteger num = InternalMath.Gcd(numerator, denominator);
            if (denominator < BigInteger.Zero)
            {
                Numerator = -numerator / num;
                Denominator = -denominator / num;
            }
            else
            {
                Numerator = numerator / num;
                Denominator = denominator / num;
            }
        }

        public BigFloat(BigInteger number)
        {
            Numerator = number;
            Denominator = BigInteger.One;
        }

        public int CompareTo(BigFloat? other)
        {
            if (other == null)
                return -1;

            int result;
            if (Denominator == other.Denominator)
            {
                result = Numerator.CompareTo(other.Numerator);
            }
            else
            {
                if (Numerator == other.Numerator)
                {
                    result = other.Denominator.CompareTo(Denominator);
                }
                else
                {
                    BigInteger num = Numerator * other.Denominator;
                    BigInteger value = other.Numerator * Denominator;
                    result = num.CompareTo(value);
                }
            }
            return result;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BigFloat);
        }

        public bool Equals(BigFloat? other)
        {
            return other != null &&
                   Numerator.Equals(other.Numerator) &&
                   Denominator.Equals(other.Denominator);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public override string ToString()
        {
            BigInteger result = BigInteger.DivRem(Numerator, Denominator, out BigInteger remainder);

            if (remainder == BigInteger.Zero)
                return result.ToString();

            BigInteger decimals = (Numerator * BigInteger.Pow(10, Precision)) / Denominator;

            if (decimals == BigInteger.Zero)
                return result.ToString();

            Stack<char> digits = new Stack<char>();

            int i = Precision;
            while (i-- > 0 && decimals > 0)
            {
                char digit = (decimals % 10).ToString()[0];
                digits.Push(digit);
                decimals /= 10;
            }

            return $"{result}.{new string(digits.ToArray()).TrimEnd('0')}";

        }

        public static BigFloat operator *(BigFloat a, BigFloat b)
        {
            BigFloat fraction = new BigFloat(a.Numerator, b.Denominator);
            BigFloat fraction2 = new BigFloat(b.Numerator, a.Denominator);
            return new BigFloat(fraction.Numerator * fraction2.Numerator, fraction.Denominator * fraction2.Denominator);
        }

        public static BigFloat operator -(BigFloat a)
        {
            return new BigFloat(-a.Numerator, a.Denominator);
        }

        public static BigFloat operator +(BigFloat a, BigFloat b)
        {
            BigInteger num = InternalMath.Gcd(a.Denominator, b.Denominator);
            BigInteger numerator = a.Numerator * (b.Denominator / num) + b.Numerator * (a.Denominator / num);
            BigInteger denominator = a.Denominator * (b.Denominator / num);
            return new BigFloat(numerator, denominator);
        }

        public static BigFloat operator -(BigFloat a, BigFloat b)
        {
            return a + -b;
        }

        public static BigFloat operator /(BigFloat a, BigFloat b)
        {
            return a * new BigFloat(b.Denominator, b.Numerator);
        }

        public static bool operator <(BigFloat a, BigFloat b)
        {
            return -1 == a.CompareTo(b);
        }

        public static bool operator >(BigFloat a, BigFloat b)
        {
            return 1 == a.CompareTo(b);
        }

        public static bool operator <=(BigFloat a, BigFloat b)
        {
            return -1 == a.CompareTo(b) || a.CompareTo(b) == 0;
        }

        public static bool operator >=(BigFloat a, BigFloat b)
        {
            return 1 == a.CompareTo(b) || a.CompareTo(b) == 0;
        }

        public static implicit operator double(BigFloat a)
        {
            return (double)a.Numerator / (double)a.Denominator;
        }

        public static implicit operator float(BigFloat a)
        {
            return (float)a.Numerator / (float)a.Denominator;
        }

        public static bool operator ==(BigFloat? left, BigFloat? right)
        {
            return EqualityComparer<BigFloat>.Default.Equals(left, right);
        }

        public static bool operator !=(BigFloat? left, BigFloat? right)
        {
            return !(left == right);
        }

        public static BigFloat Parse(string value, CultureInfo cultureInfo)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            value = value.Trim().Replace(cultureInfo.NumberFormat.NumberGroupSeparator, "");
            int pos = value.IndexOf(cultureInfo.NumberFormat.NumberDecimalSeparator);
            value = value.Replace(cultureInfo.NumberFormat.NumberDecimalSeparator, "");

            if (pos < 0)
            {
                //no decimal point
                BigInteger numerator = BigInteger.Parse(value);
                return new BigFloat(numerator);
            }
            else
            {
                BigInteger numerator = BigInteger.Parse(value);
                BigInteger denominator = BigInteger.Pow(10, value.Length - pos);

                return new BigFloat(numerator, denominator);
            }
        }

        public static bool TryParse(string value, CultureInfo culture, out BigFloat result)
        {
            try
            {
                result = Parse(value, culture);
                return true;
            }
            catch (Exception)
            {
                result = Zero;
                return false;
            }
        }

        public static explicit operator BigFloat(double number)
        {
            var culture = CultureInfo.InvariantCulture;
            return Parse(number.ToString("N99", culture), culture);
        }

        public static explicit operator BigFloat(float number)
        {
            var culture = CultureInfo.InvariantCulture;
            return Parse(number.ToString("N99", culture), culture);
        }

        public static readonly BigFloat Zero = new BigFloat(0);
        public static readonly BigFloat One = new BigFloat(1);
    }
}
