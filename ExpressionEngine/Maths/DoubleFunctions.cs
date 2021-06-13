//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace ExpressionEngine.Maths
{
    /// <summary>
    /// Functions that have a double return value
    /// </summary>
    public static class DoubleFunctions
    {
        /// <summary>
        /// Converts an angle specified in radians to degress
        /// </summary>
        /// <param name="deg">angle in degrees</param>
        /// <returns>angle in radians</returns>
        public static double DegToRad(double deg)
        {
            return Math.PI * (deg / 180.0);
        }

        /// <summary>
        /// Converts an angle specified in degress to radians
        /// </summary>
        /// <param name="rad">angle in radians</param>
        /// <returns>angle in degrees</returns>
        public static double RadToDeg(double rad)
        {
            return (rad * 180.0) / Math.PI;
        }

        /// <summary>
        /// Converts an angle specified in gradians to degress
        /// </summary>
        /// <param name="grad">angle in gradians</param>
        /// <returns>angle in degrees</returns>
        public static double GradToDeg(double grad)
        {
            return (360.0 * grad) / 400.0;
        }

        /// <summary>
        /// Converts an angle specified in degress to gradians
        /// </summary>
        /// <param name="deg">angle in gradians</param>
        /// <returns>angle in gradians</returns>
        public static double DegToGrad(double deg)
        {
            return (400.0 * deg) / 360.0;
        }

        /// <summary>
        /// Converts an angle specified in gradians to degress
        /// </summary>
        /// <param name="grad">angle in gradians</param>
        /// <returns>angle in radians</returns>
        public static double GradToRad(double grad)
        {
            return (grad * Math.PI) / 200.0;
        }

        /// <summary>
        /// Converts an angle specified in radians to gradians
        /// </summary>
        /// <param name="rad">angle in radians</param>
        /// <returns>angle in gradians</returns>
        public static double RadToGrad(double rad)
        {
            return (rad * 200.0) / Math.PI;
        }

        /// <summary>
        /// Returns the product of all positive integers less than or equal to n
        /// </summary>
        /// <remarks>if n is negative NaN is returned, if N > 100 positiveInfity returned</remarks>
        /// <param name="n">factorial limit</param>
        /// <returns>the product of all positive integers less than or equal to n</returns>
        public static double Factorial(int n)
        {
            if (n < 0) 
                return double.NaN;
            else if (n == 0)
                return 1;
            else if (n > 170)
                return double.PositiveInfinity;

            double fact = 1.0;

            for (int k = 1; k <= n; k++)
            {
                fact *= k;
            }

            return fact;
        }
    }
}
