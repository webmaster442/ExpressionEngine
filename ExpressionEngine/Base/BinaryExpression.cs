using System;
using System.Globalization;

namespace ExpressionEngine.Base
{
    internal abstract class BinaryExpression: IExpression
    {
        public Variables? Variables { get; }

        protected BinaryExpression(IExpression? left, IExpression? right)
        {
            Left = left;
            Right = right;
        }

        public IExpression? Left { get; }
        public IExpression? Right { get; }

        public double Evaluate()
        {
            return Evaluate(Left?.Evaluate() ?? double.NaN, Right?.Evaluate() ?? double.NaN);
        }

        protected abstract double Evaluate(double number1, double number2);

        public abstract IExpression? Simplify();

        public abstract IExpression? Differentiate(string byVariable);

        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
