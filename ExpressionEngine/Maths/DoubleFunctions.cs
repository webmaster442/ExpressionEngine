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
            return Math.PI * ((deg % 360) / 180.0);
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
    }
}
