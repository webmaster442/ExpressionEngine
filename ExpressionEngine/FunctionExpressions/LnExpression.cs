//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Numbers;
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
            return new DivideExpression(new ConstantExpression(1), Child);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new LnExpression(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"ln({Child})";
        }

        protected override Number Evaluate(Number number)
        {
            return NumberMath.Ln(number);
        }
    }
}
