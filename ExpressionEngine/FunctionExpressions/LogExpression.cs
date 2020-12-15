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
                return new ConstantExpression(Evaluate(leftConst.Value, rightConst.Value));
            }
            else
            {
                return new LogExpression(newLeft, newRight);
            }
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"log({Left}, {Right})";
        }

        protected override Number Evaluate(Number number1, Number number2)
        {
            return NumberMath.Log(number1, number2);
        }
    }
}
