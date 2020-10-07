using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Maths;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal sealed class CosExpression : UnaryExpression
    {
        public CosExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new MultiplyExpression(new NegateExpression(new SinExpression(Child)), Child?.Differentiate(byVariable));
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new CosExpression(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"cos({Child})";
        }

        protected override double Evaluate(double number)
        {
            return Math.Round(Trigonometry.Cos(number));
        }
    }
}
