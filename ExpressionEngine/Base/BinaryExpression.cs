//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using System;
using System.Globalization;

namespace ExpressionEngine.Base
{
    internal abstract class BinaryExpression: IExpression
    {
        public IVariables? Variables { get; }

        protected BinaryExpression(IExpression? left, IExpression? right)
        {
            Left = left;
            Right = right;
        }

        public IExpression? Left { get; }
        public IExpression? Right { get; }

        public INumber Evaluate()
        {
            var l = Left?.Evaluate() as Number ?? new Number(NumberState.NaN);
            var r = Right?.Evaluate() as Number ?? new Number(NumberState.NaN);
            return Evaluate(l, r);
        }

        protected abstract Number Evaluate(Number number1, Number number2);

        public abstract IExpression? Simplify();

        public abstract IExpression? Differentiate(string byVariable);

        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
