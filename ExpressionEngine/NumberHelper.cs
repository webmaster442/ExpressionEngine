using ExpressionEngine.Numbers;
using System.Globalization;

namespace ExpressionEngine
{
    public static class NumberHelper
    {
        public static INumber Pi => NumberMath.Pi;
        public static INumber E => NumberMath.E;

        public static INumber FromDouble(double d)
        {
            return (Number)d;
        }

        public static INumber FromString(string input)
        {
            return Number.Parse(input, CultureInfo.InvariantCulture);
        }

        public static INumber FromString(string input, CultureInfo culture)
        {
            return Number.Parse(input, culture);
        }
    }
}
