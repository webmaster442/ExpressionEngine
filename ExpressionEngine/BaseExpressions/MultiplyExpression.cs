﻿using ExpressionEngine.Base;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class MultiplyExpression : BinaryExpression
    {
        public MultiplyExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return
                new AddExpression(new MultiplyExpression(Left, Right?.Differentiate(byVariable)),
                                  new MultiplyExpression(Left?.Differentiate(byVariable), Right));
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as ConstantExpression;
            var rightConst = newRight as ConstantExpression;
            var leftNegate = newLeft as NegateExpression;
            var rightNegate = newRight as NegateExpression;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new ConstantExpression(leftConst.Value * rightConst.Value);
            }
            if (leftConst != null)
            {
                if (leftConst.Value == 0)
                {
                    // 0 * y
                    return new ConstantExpression(0);
                }
                if (leftConst.Value == 1)
                {
                    // 1 * y
                    return newRight;
                }
                if (leftConst.Value == -1)
                {
                    // -1 * y
                    if (rightNegate != null)
                    {
                        // y = -u (-y = --u)
                        return rightNegate.Child;
                    }
                    return new NegateExpression(newRight);
                }
            }
            else if (rightConst != null)
            {
                if (rightConst.Value == 0)
                {
                    // x * 0
                    return new ConstantExpression(0);
                }
                if (rightConst.Value == 1)
                {
                    // x * 1
                    return newLeft;
                }
                if (rightConst.Value == -1)
                {
                    // x * -1
                    if (leftNegate != null)
                    {
                        // x = -u (-x = --u)
                        return leftNegate.Child;
                    }
                    return new NegateExpression(newLeft);
                }
            }
            else if (leftNegate != null && rightNegate != null)
            {
                // -x * -y
                return new MultiplyExpression(leftNegate.Child, rightNegate.Child);
            }
            // x * y
            return new MultiplyExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} * {Right})";
        }

        protected override double Evaluate(double number1, double number2)
        {
            return number1 * number2;
        }
    }
}
