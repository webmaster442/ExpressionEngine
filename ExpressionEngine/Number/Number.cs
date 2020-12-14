//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace ExpressionEngine.Numbers
{
    public class Number: INumber, IComparable<Number>
    {
        public static readonly Number Zero = new Number(BigInteger.Zero);
        public static readonly Number One = new Number(BigInteger.One);
        public static readonly Number MinusOne = new Number(BigInteger.MinusOne);
        public static readonly Number PositiveInfinity = new Number(NumberState.PositiveInfinity);
        public static readonly Number NaN = new Number(NumberState.NaN);
        public static readonly Number NegativeInfinity = new Number(NumberState.NegativeInfinity);

        public const int Precision = 21;

        public BigInteger Numerator { get; }

        public BigInteger Denominator { get; }

        internal NumberState State { get; set; }

        internal Number(NumberState state)
        {
            Numerator = 0;
            Denominator = 0;
            State = state;
        }

        public Number(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == BigInteger.Zero)
            {
                numerator = 0;
                denominator = 0;
                State = NumberState.NaN;
            }
            BigInteger num = NumberAlgorithms.Gcd(numerator, denominator);
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

        public Number(BigInteger number)
        {
            Numerator = number;
            Denominator = BigInteger.One;
        }

        public static Number Parse(string value, CultureInfo cultureInfo)
        {
            switch (value.ToLower())
            {
                case "nan":
                    return new Number(NumberState.NaN);
                case "-∞":
                case "-infinity":
                    return new Number(NumberState.NegativeInfinity);
                case "∞":
                case "infinity":
                    return new Number(NumberState.PositiveInfinity);
            }

            value = value.Trim().Replace(cultureInfo.NumberFormat.NumberGroupSeparator, "");

            BigInteger multiplier = BigInteger.One;

            if (value.Contains("E"))
            {
                var parts = value.Split('E');
                value = parts[0];

                int exponent = int.Parse(parts[1], cultureInfo);

                if (exponent < 0)
                    multiplier = -BigInteger.Pow(10, exponent * -1);
                else
                    multiplier = BigInteger.Pow(10, exponent);
            }

            int count = value.Count(c => c == cultureInfo.NumberFormat.NumberDecimalSeparator[0]);
            if (count > 1)
                throw new FormatException();

            int i = value.IndexOf(cultureInfo.NumberFormat.NumberDecimalSeparator);
            value = value.Replace(cultureInfo.NumberFormat.NumberDecimalSeparator, "");

            if (i < 0)
            {
                //no decimal point
                BigInteger numerator = BigInteger.Parse(value);

                if (multiplier > 0)
                    return new Number(numerator * multiplier);
                else
                    return new Number(numerator, -multiplier);
            }
            else
            {
                BigInteger numerator = BigInteger.Parse(value);
                BigInteger denominator = BigInteger.Pow(10, value.Length - i);

                if (multiplier > 0)
                    return new Number(multiplier * numerator, denominator);
                else
                    return new Number(numerator, denominator) * new Number(1, -multiplier);
            }
        }

        public static bool TryParse(string value, CultureInfo culture, out Number result)
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

        public string ToString(IFormatProvider formatProvider)
        {
            if (NumberAlgorithms.TryHandleSpecialToString(this, out string special))
            {
                return special;
            }

            BigInteger integerPart = BigInteger.DivRem(Numerator, Denominator, out BigInteger remainder);

            int padding = (Denominator.ToString().Length - Numerator.ToString().Length) - 1;

            string pad = string.Empty;

            if (padding > 0)
            {
                pad = pad.PadLeft(padding, '0');
            }

            if (remainder == BigInteger.Zero)
                return integerPart.ToString();

            BigInteger num = Numerator;
            if (Numerator.Sign == -1 && integerPart == 0)
            {
                num *= -1;
            }
            BigInteger decimals = (num * BigInteger.Pow(10, Precision)) / Denominator;

            if (decimals == BigInteger.Zero)
                return integerPart.ToString();

            Stack<char> digits = new Stack<char>();

            int i = Precision;
            while (i-- > 0 && decimals > 0)
            {
                var digit = (decimals % 10).ToString();
                digits.Push(digit[0]);
                decimals /= 10;
            }

            string floatPart = new string(digits.ToArray()).TrimEnd('0');
            if (Numerator.Sign == -1 && integerPart == 0)
            {
                return $"-{integerPart}.{pad}{floatPart}";
            }

            return $"{integerPart}.{pad}{floatPart}";
        }

        public double ToDouble()
        {
            var str = ToString(CultureInfo.InvariantCulture);
            return double.Parse(str, CultureInfo.InvariantCulture);
        }

        public override bool Equals(object? obj)
        {
            return obj is Number number &&
                   Numerator.Equals(number.Numerator) &&
                   Denominator.Equals(number.Denominator) &&
                   State == number.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator, State);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public int CompareTo(Number? other)
        {
            if (Equals(other, null))
                throw new ArgumentNullException(nameof(other));

            BigInteger one = Numerator;
            BigInteger two = other.Numerator;

            one *= other.Denominator;
            two *= Denominator;

            return BigInteger.Compare(one, two);
        }

        public int CompareTo(INumber? other)
        {
            return CompareTo(other as Number);
        }

        private static int Compare(Number left, Number right)
        {
            if (Equals(left, null))
                throw new ArgumentNullException(nameof(left));
            if (Equals(right, null))
                throw new ArgumentNullException(nameof(right));

            return left.CompareTo(right);
        }

        public static Number operator *(Number a, Number b)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(a, b, out Number value))
            {
                return value;
            }

            Number fraction = new Number(a.Numerator, b.Denominator);
            Number fraction2 = new Number(b.Numerator, a.Denominator);
            return new Number(fraction.Numerator * fraction2.Numerator, fraction.Denominator * fraction2.Denominator);
        }

        public static Number operator -(Number a)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(a, out Number value))
            {
                return value;
            }

            return new Number(-a.Numerator, a.Denominator);
        }

        public static Number operator +(Number a, Number b)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(a, b, out Number value))
            {
                return value;
            }

            BigInteger num = NumberAlgorithms.Gcd(a.Denominator, b.Denominator);
            BigInteger numerator = a.Numerator * (b.Denominator / num) + b.Numerator * (a.Denominator / num);
            BigInteger denominator = a.Denominator * (b.Denominator / num);
            return new Number(numerator, denominator);
        }

        public static Number operator -(Number a, Number b)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(a, b, out Number value))
            {
                return value;
            }

            return a + -b;
        }

        public static Number operator /(Number a, Number b)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(a, b, out Number value))
            {
                return value;
            }

            return a * new Number(b.Denominator, b.Numerator);
        }

        public static bool operator ==(Number? left, Number? right)
        {
            return EqualityComparer<Number>.Default.Equals(left, right);
        }

        public static bool operator !=(Number? left, Number? right)
        {
            return !(left == right);
        }

        public static bool operator <(Number left, Number right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator <=(Number left, Number right)
        {
            return Compare(left, right) <= 0;
        }

        public static bool operator >(Number left, Number right)
        {
            return Compare(left, right) > 0;
        }

        public static bool operator >=(Number left, Number right)
        {
            return Compare(left, right) >= 0;
        }

        public static implicit operator Number(double value)
        {
            string num = value.ToString(CultureInfo.InvariantCulture);
            return Parse(num, CultureInfo.InvariantCulture);
        }

        public static implicit operator Number(int value)
        {
            return new Number(value);
        }

        public static implicit operator Number(long value)
        {
            return new Number(value);
        }
    }
}
