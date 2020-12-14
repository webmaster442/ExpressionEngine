//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.Numbers;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class SubtractExpression : BinaryExpression
    {
        public SubtractExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        protected override Number Evaluate(Number number1, Number number2)
        {
            return number1 - number2;
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new SubtractExpression(Left?.Differentiate(byVariable), Right?.Differentiate(byVariable));
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as ConstantExpression;
            var rightConst = newRight as ConstantExpression;
            var rightNegate = newRight as NegateExpression;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new ConstantExpression(leftConst.Value - rightConst.Value);
            }
            if (leftConst?.Value == 0)
            {
                // 0 - y
                if (rightNegate != null)
                {
                    // y = -u (--u)
                    return rightNegate.Child;
                }
                return new NegateExpression(newRight);
            }
            if (rightConst?.Value == 0)
            {
                // x - 0
                return newLeft;
            }
            if (rightNegate != null)
            {
                // x - -y
                return new AddExpression(newLeft, rightNegate.Child);
            }
            // x - y;  no simplification
            return new SubtractExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} - {Right})";
        }


    }
}
