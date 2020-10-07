using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal sealed class LogExpression : BinaryExpression
    {
        public LogExpression(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new MultiplyExpression(new DivideExpression(new ConstantExpression(1), new LnExpression(Right)),
                                          new DivideExpression(new ConstantExpression(1), Left));
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            if (newLeft is ConstantExpression leftConst
                && newRight is ConstantExpression rightConst)
            {
                // two constants
                return new ConstantExpression(Math.Log(leftConst.Value, rightConst.Value));
            }
            else
            {
                return new LogExpression(newLeft, newRight);
            }
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"log({Left}, {Right})";
        }

        protected override double Evaluate(double number1, double number2)
        {
            return Math.Log(number1, number2);
        }
    }
}
