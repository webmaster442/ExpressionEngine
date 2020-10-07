using ExpressionEngine.Base;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class NegateExpression : UnaryExpression
    {
        public NegateExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new NegateExpression(Child?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(-childConst.Value);
            }
            return new NegateExpression(newChild);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"(-{Child})";
        }

        protected override double Evaluate(double number)
        {
            return -number;
        }
    }
}
