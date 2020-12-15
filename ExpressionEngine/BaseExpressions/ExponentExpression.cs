//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.Numbers;
using ExpressionEngine.Properties;
using System;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class ExponentExpression : BinaryExpression
    {
        public ExponentExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            if (Right is ConstantExpression)
            {
                // f(x) = g(x)^n
                // f'(x) = n * g'(x) * g(x)^(n-1)
                return 
                    new MultiplyExpression(new MultiplyExpression(Right, Left?.Differentiate(byVariable)),
                                           new ExponentExpression(Left, new SubtractExpression(Right, new ConstantExpression(1))));
            }
            var simple = Left?.Simplify();
            if (simple is ConstantExpression constant)
            {
                // f(x) = a^g(x)
                // f'(x) = (ln a) * g'(x) * a^g(x)
                var a = constant.Value;
                return new MultiplyExpression(new MultiplyExpression(new ConstantExpression(NumberMath.Ln(a)), Right?.Differentiate(byVariable)), new ExponentExpression(simple, Right));
            }
            throw new CannotDifferentiateException(Resources.CanotDifferentiate);
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
                return new ConstantExpression(Evaluate(leftConst.Value, rightConst.Value));
            }
            if (rightConst != null)
            {
                if (rightConst.Value == 0)
                {
                    // x ^ 0
                    return new ConstantExpression(1);
                }
                if (rightConst.Value == 1)
                {
                    // x ^ 1
                    return newLeft;
                }
            }
            else if (leftConst?.Value == 0)
            {
                // 0 ^ y
                return new ConstantExpression(0);
            }
            // x ^ y;  no simplification
            return new ExponentExpression(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left} ^ {Right})";
        }

        protected override Number Evaluate(Number number1, Number number2)
        {
            return NumberMath.Pow(number1, number2);
        }
    }
}
