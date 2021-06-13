//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Maths;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal class SinExpression : UnaryExpression
    {
        public SinExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new MultiplyExpression(new CosExpression(Child), Child?.Differentiate(byVariable));
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new SinExpression(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"sin({Child})";
        }

        protected override double Evaluate(double number)
        {
            return Math.Round(Trigonometry.Sin(number), 15);
        }
    }
}
