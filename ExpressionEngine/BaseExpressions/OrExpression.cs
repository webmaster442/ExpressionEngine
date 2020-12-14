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
    internal sealed class OrExpression : BinaryExpression
    {
        public OrExpression(IExpression? left, IExpression? right) : base(left, right)
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
                return new ConstantExpression(BooleanFunctions.Or(leftConst.Value, rightConst.Value));
            }
            if (leftConst?.Value == 1
                || rightConst?.Value == 1)
            {
                return new ConstantExpression(1.0);
            }

            return new OrExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} & {Right})";
        }

        protected override Number Evaluate(Number number1, Number number2)
        {
            return BooleanFunctions.Or(number1, number2);
        }
    }
}
