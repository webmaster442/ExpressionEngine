//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using System;
using System.Globalization;

namespace ExpressionEngine.BaseExpressions
{
    internal sealed class ConstantExpression : IExpression
    {
        public IVariables? Variables { get; }

        public Number Value { get; }

        public ConstantExpression(Number value)
        {
            Value = value;
        }

        public IExpression Differentiate(string byVariable)
        {
            return new ConstantExpression(0);
        }

        public INumber Evaluate()
        {
            return Value;
        }

        public IExpression Simplify()
        {
            return new ConstantExpression(Value);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
