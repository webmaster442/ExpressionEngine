//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Maths
{
    public static class BooleanFunctions
    {
        public static double And(double a, double b)
        {
            var b1 = a == 1.0;
            var b2 = b == 1.0;

            if (b1 && b2)
                return 1.0;
            else
                return 0.0;

        }

        public static double Or(double a, double b)
        {
            var b1 = a == 1.0;
            var b2 = b == 1.0;

            if (b1 || b2)
                return 1.0;
            else
                return 0.0;
        }

        public static double Not(double a)
        {
            var b1 = a == 1.0;

            if (!b1)
            {
                return 1.0;
            }
            return 0.0;
        }
    }
}
