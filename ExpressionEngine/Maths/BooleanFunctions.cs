//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;

namespace ExpressionEngine.Maths
{
    public static class BooleanFunctions
    {
        public static Number And(Number a, Number b)
        {
            var b1 = a == Number.One;
            var b2 = b == Number.One;

            if (b1 && b2)
                return Number.One;
            else
                return Number.Zero;

        }

        public static Number Or(Number a, Number b)
        {
            var b1 = a == Number.One;
            var b2 = b == Number.One;

            if (b1 || b2)
                return Number.One;
            else
                return Number.Zero;
        }

        public static Number Not(Number a)
        {
            var b1 = a == Number.One;

            if (!b1)
            {
                return Number.One;
            }
            return Number.Zero;
        }
    }
}
