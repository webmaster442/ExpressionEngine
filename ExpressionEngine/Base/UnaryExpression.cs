using System;
using System.Globalization;

namespace ExpressionEngine.Base
{
    internal abstract class UnaryExpression: IExpression
    {
        public Variables? Variables { get; }

        protected UnaryExpression(IExpression? child)
        {
           Child = child;
        }

        public IExpression? Child { get; }

        public double Evaluate()
        {
            return Evaluate(Child?.Evaluate() ?? double.NaN);
        }

        protected abstract double Evaluate(double number);

        public abstract IExpression? Simplify();

        public abstract IExpression? Differentiate(string byVariable);

        public abstract string ToString(string format, IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(string.Empty, CultureInfo.CurrentCulture);
        }
    }
}
