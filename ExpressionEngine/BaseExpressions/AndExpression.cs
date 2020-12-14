//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.Maths;
using ExpressionEngine.Numbers;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class AndExpression : BinaryExpression
    {
        public AndExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new CannotDifferentiateException();
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as ConstantExpression;
            var rightConst = newRight as ConstantExpression;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new ConstantExpression(BooleanFunctions.And(leftConst.Value, rightConst.Value));
            }
            if (leftConst?.Value == 0
                || rightConst?.Value == 0)
            {
                return new ConstantExpression(0.0);
            }

            return new AndExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} & {Right})";
        }

        protected override Number Evaluate(Number number1, Number number2)
        {
            return BooleanFunctions.And(number1, number2);
        }
    }
}
