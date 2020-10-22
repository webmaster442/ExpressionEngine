//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.Maths;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class NotExpression : UnaryExpression
    {
        public NotExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new CannotDifferentiateException();
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(BooleanFunctions.Not(childConst.Value));
            }
            return new NegateExpression(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"(!{Child})";
        }

        protected override double Evaluate(double number)
        {
            return BooleanFunctions.Not(number);
        }
    }
}
