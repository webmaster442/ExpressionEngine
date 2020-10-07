using System;

namespace ExpressionEngine.Maths
{
    /// <summary>
    /// Trigonometic functions
    /// </summary>
    public static class Trigonometry
    {
        /// <summary>
        /// Gets or sets the current Angle mode that affects trigonometic functions.
        /// </summary>
        public static AngleMode AngleMode { get; set; }

        private static double ExecuteTrigometry(Func<double, double> function, double input)
        {
            double radians = input;
            if (AngleMode == AngleMode.Deg)
                radians = DoubleFunctions.DegToRad(input);
            else if (AngleMode == AngleMode.Grad)
                radians = DoubleFunctions.GradToRad(input);

            return function(radians);
        }

        private static double ExecuteInvertedTrigonometry(Func<double, double> function, double input)
        {
            double result = function(input);
            if (AngleMode == AngleMode.Deg)
                return DoubleFunctions.RadToDeg(result);
            else if (AngleMode == AngleMode.Grad)
                return DoubleFunctions.RadToGrad(result);

            return result;
        }

        /// <summary>
        /// Returns the sine of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The sine of input</returns>
        public static double Sin(double input)
        {
            return ExecuteTrigometry(Math.Sin, input);
        }

        /// <summary>
        /// Returns the angle (measured in units, specified by the AngleMode property) whose sine is the specified number.
        /// </summary>
        /// <param name="input">A number representing a sine</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static double ArcSin(double input)
        {
            return ExecuteInvertedTrigonometry(Math.Asin, input);
        }

        /// <summary>
        /// Returns the cosine of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The cosine of input</returns>
        public static double Cos(double input)
        {
            return ExecuteTrigometry(Math.Cos, input);
        }

        /// <summary>
        ///  Returns the angle (measured in units, specified by the AngleMode property) whose cosine is the specified number.
        /// </summary>
        /// <param name="input">A number representing a cosine</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static double ArcCos(double input)
        {
            return ExecuteInvertedTrigonometry(Math.Acos, input);
        }

        /// <summary>
        /// Returns the tangent of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The tangent of input</returns>
        public static double Tan(double input)
        {
            return Sin(input) / Math.Round(Cos(input), 15);
        }

        /// <summary>
        /// Returns the angle (measured in units, specified by the AngleMode property) whose tangent is the specified number.
        /// </summary>
        /// <param name="input">A number representing a tangent</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static double ArcTan(double input)
        {
            return ExecuteInvertedTrigonometry(Math.Atan, input);
        }

        /// <summary>
        /// Returns the cotangent of the specified angle
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>The cotangent of input</returns>
        public static double Ctg(double input)
        {
            return Cos(input) / Math.Round(Sin(input), 15);
        }

        /// <summary>
        ///  Returns the angle (measured in units, specified by the AngleMode property) whose cotangent is the specified number.
        /// </summary>
        /// <param name="input">An angle, measured in units, specified by the AngleMode property</param>
        /// <returns>An angle, measured in units, specified by the AngleMode property</returns>
        public static double ArcCtg(double input)
        {
            return ExecuteInvertedTrigonometry((x) => Math.Atan(1 / Ctg(x)), input);
        }

    }
}
