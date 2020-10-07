using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Properties;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal class NonDifferentiatableFunction : UnaryExpression
    {
        private readonly Func<double, double> _function;
        private readonly string _name;

        public NonDifferentiatableFunction(IExpression? child, Func<double, double> function, string name) : base(child)
        {
            _function = function;
            _name = name;
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new CannotDifferentiateException(Resources.CanotDifferentiate);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new NonDifferentiatableFunction(newChild, _function, _name);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{_name}({Child})";
        }

        protected override double Evaluate(double number)
        {
            return _function.Invoke(number);
        }
    }
}
