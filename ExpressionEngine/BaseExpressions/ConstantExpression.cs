using System;
using System.Globalization;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class ConstantExpression : IExpression
    {
        public Variables? Variables { get; }

        public double Value { get; }

        public ConstantExpression(double value)
        {
            Value = value;
        }

        public IExpression Differentiate(string byVariable)
        {
            return new ConstantExpression(0);
        }

        public double Evaluate()
        {
            return Value;
        }

        public IExpression Simplify()
        {
            return new ConstantExpression(Value);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(string.Empty, CultureInfo.CurrentCulture);
        }
    }
}
