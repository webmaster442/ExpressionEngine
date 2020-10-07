using ExpressionEngine.Base;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class AddExpression : BinaryExpression
    {
        public AddExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        protected override double Evaluate(double number1, double number2)
        {
            return number1 + number2;
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new AddExpression(Left?.Differentiate(byVariable), Right?.Differentiate(byVariable));
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
                return new ConstantExpression(leftConst.Value + rightConst.Value);
            }
            if (leftConst?.Value == 0)
            {
                // 0 + y
                return newRight;
            }
            if (rightConst?.Value == 0)
            {
                // x + 0
                return newLeft;
            }
            if (newRight is NegateExpression rightNegate)
            {
                // x + -y;  return x - y;  (this covers -x + -y case too)
                return new SubtractExpression(newLeft, rightNegate.Child);
            }
            if (newLeft is NegateExpression leftNegate)
            {
                // -x + y
                return new SubtractExpression(newRight, leftNegate.Child);
            }
            // x + y;  no simplification
            return new AddExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} + {Right})";
        }
    }
}
