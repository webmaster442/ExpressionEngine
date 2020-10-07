using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal sealed class LnExpression : UnaryExpression
    {
        public LnExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new DivideExpression(new ConstantExpression(1), Child?.Differentiate(byVariable));
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

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"ln({Child})";
        }

        protected override double Evaluate(double number)
        {
            return Math.Log(number);
        }
    }
}
