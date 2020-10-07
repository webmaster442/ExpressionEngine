using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Properties;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal class RootExpression : BinaryExpression
    {
        public RootExpression(IExpression? child1, IExpression? child2) :base(child1, child2)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            if (Right is ConstantExpression)
            {
                // f(x) = g(x)^n
                // f'(x) = n * g'(x) * g(x)^(n-1)

                var newRight = new DivideExpression(new ConstantExpression(1), Right);

                return
                    new MultiplyExpression(new MultiplyExpression(newRight, Left?.Differentiate(byVariable)),
                                           new ExponentExpression(Left, new SubtractExpression(newRight, new ConstantExpression(1))));
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
                return new ConstantExpression(Math.Pow(leftConst.Value, 1 / rightConst.Value));
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
            return new ExponentExpression(newLeft, new DivideExpression(new ConstantExpression(1), newRight));
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"Root({Left}, {Right})";
        }

        protected override double Evaluate(double number1, double number2)
        {
            return Math.Pow(number1, 1 / number2);
        }
    }
}