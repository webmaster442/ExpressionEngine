//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Numerics;

namespace ExpressionEngine.Numbers
{
    internal static class NumberMath
    {
        public static readonly Number Pi = Number.Parse("3.1415926535897932384626433832795028841971693994", CultureInfo.InvariantCulture);
        public static readonly Number E  = Number.Parse("2.7182818284590452353602874713526624977572470937", CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets or sets the current Angle mode that affects trigonometic functions.
        /// </summary>
        public static AngleMode AngleMode { get; set; }

        public static Number Abs(Number value)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(value, out Number result))
            {
                return result;
            }

            BigInteger num = BigInteger.Abs(value.Numerator);
            return new Number(num, value.Denominator);
        }

        public static Number Ceiling(Number value)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(value, out Number result))
            {
                return result;
            }

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
            if (NumberAlgorithms.TryHandleSpecialCase(value, out Number result))
            {
                return result;
            }

            return Ceiling(value) - Number.One;
        }

        public static Number Truncate(Number value)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(value, out Number result))
            {
                return result;
            }

            BigInteger numerator = value.Numerator;
            numerator -= BigInteger.Remainder(numerator, value.Denominator);
            return new Number(numerator, value.Denominator);
        }

        public static Number Round(Number value, int digits)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(value, out Number result))
            {
                return result;
            }

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

        public static Number Pow(Number input, Number exponent)
        {
            return Math.Pow(input.ToDouble(), exponent.ToDouble());
        }

        /// <summary>
        /// Converts an angle specified in radians to degress
        /// </summary>
        /// <param name="deg">angle in degrees</param>
        /// <returns>angle in radians</returns>
        public static Number DegToRad(Number deg)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(deg, out Number result))
            {
                return result;
            }

            return Pi * (deg / 180.0);
        }

        /// <summary>
        /// Converts an angle specified in degress to radians
        /// </summary>
        /// <param name="rad">angle in radians</param>
        /// <returns>angle in degrees</returns>
        public static Number RadToDeg(Number rad)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(rad, out Number result))
            {
                return result;
            }

            return (rad * 180) / Pi;
        }

        /// <summary>
        /// Converts an angle specified in gradians to degress
        /// </summary>
        /// <param name="grad">angle in gradians</param>
        /// <returns>angle in degrees</returns>
        public static Number GradToDeg(Number grad)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(grad, out Number result))
            {
                return result;
            }

            return (360 * grad) / 400;
        }

        /// <summary>
        /// Converts an angle specified in degress to gradians
        /// </summary>
        /// <param name="deg">angle in gradians</param>
        /// <returns>angle in gradians</returns>
        public static Number DegToGrad(Number deg)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(deg, out Number result))
            {
                return result;
            }

            return (400 * deg) / 360;
        }

        /// <summary>
        /// Converts an angle specified in gradians to degress
        /// </summary>
        /// <param name="grad">angle in gradians</param>
        /// <returns>angle in radians</returns>
        public static Number GradToRad(Number grad)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(grad, out Number result))
            {
                return result;
            }

            return (grad * Pi) / 200.0;
        }

        /// <summary>
        /// Converts an angle specified in radians to gradians
        /// </summary>
        /// <param name="rad">angle in radians</param>
        /// <returns>angle in gradians</returns>
        public static Number RadToGrad(Number rad)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(rad, out Number result))
            {
                return result;
            }

            return (rad * 200) / Pi;
        }

        /// <summary>
        /// Returns the product of all positive integers less than or equal to n
        /// </summary>
        /// <remarks>if n is negative NaN is returned, if N > 100 positiveInfity returned</remarks>
        /// <param name="n">factorial limit</param>
        /// <returns>the product of all positive integers less than or equal to n</returns>
        public static Number Factorial(int n)
        {
            if (n < 0)
                return Number.NaN;
            else if (n == 0)
                return Number.One;
            else if (n > 170)
                return Number.PositiveInfinity;

            Number fact = Number.One;

            for (int k = 1; k <= n; k++)
            {
                fact *= k;
            }

            return fact;
        }

        private static Number ExecuteTrigometry(Func<double, double> function, Number input)
        {
            Number radians = input;
            if (AngleMode == AngleMode.Deg)
                radians = DegToRad(input);
            else if (AngleMode == AngleMode.Grad)
                radians = GradToRad(input);

            return function(radians.ToDouble());
        }

        private static Number ExecuteInvertedTrigonometry(Func<double, double> function, Number input)
        {
            Number result = function(input.ToDouble());
            if (AngleMode == AngleMode.Deg)
                return RadToDeg(result);
            else if (AngleMode == AngleMode.Grad)
                return RadToGrad(result);

            return result;
        }

        /// <summary>
        /// Returns the sine of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The sine of input</returns>
        public static Number Sin(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteTrigometry(Math.Sin, input);
        }

        /// <summary>
        /// Returns the angle (measured in units, specified by the AngleMode property) whose sine is the specified number.
        /// </summary>
        /// <param name="input">A number representing a sine</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static Number ArcSin(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteInvertedTrigonometry(Math.Asin, input);
        }

        /// <summary>
        /// Returns the cosine of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The cosine of input</returns>
        public static Number Cos(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteTrigometry(Math.Cos, input);
        }

        /// <summary>
        ///  Returns the angle (measured in units, specified by the AngleMode property) whose cosine is the specified number.
        /// </summary>
        /// <param name="input">A number representing a cosine</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static Number ArcCos(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteInvertedTrigonometry(Math.Acos, input);
        }

        /// <summary>
        /// Returns the tangent of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The tangent of input</returns>
        public static Number Tan(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return Sin(input) / Round(Cos(input), 21);
        }

        /// <summary>
        /// Returns the angle (measured in units, specified by the AngleMode property) whose tangent is the specified number.
        /// </summary>
        /// <param name="input">A number representing a tangent</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static Number ArcTan(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteInvertedTrigonometry(Math.Atan, input);
        }

        /// <summary>
        /// Returns the cotangent of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The cotangent of input</returns>
        public static Number Ctg(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return Cos(input) / Round(Sin(input), 21);
        }

        /// <summary>
        ///  Returns the angle (measured in units, specified by the AngleMode property) whose cotangent is the specified number.
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static Number ArcCtg(Number input)
        {
            if (NumberAlgorithms.TryHandleSpecialCase(input, out Number result))
            {
                return result;
            }

            return ExecuteInvertedTrigonometry((x) => Math.Atan((1.0 / Ctg(x)).ToDouble()), input);
        }

        public static Number Ln(Number number)
        {
            return ((Number)BigInteger.Log(number.Numerator)) - ((Number)BigInteger.Log(number.Denominator));
        }

        public static Number Log(Number number, Number exponent)
        {
            if (exponent.Numerator == 10 && exponent.Denominator == 1)
            {
                return ((Number)BigInteger.Log10(number.Numerator)) - ((Number)BigInteger.Log10(number.Denominator));
            }
            return Ln(number) / Ln(exponent);
        }
    }
}
