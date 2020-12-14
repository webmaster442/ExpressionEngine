//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Numbers;
using System;
using System.Globalization;

namespace ExpressionEngine.Base
{
    internal abstract class UnaryExpression: IExpression
    {
        public IVariables? Variables { get; }

        protected UnaryExpression(IExpression? child)
        {
           Child = child;
        }

        public IExpression? Child { get; }

        public INumber Evaluate()
        {
            return Evaluate(Child?.Evaluate() as Number ?? new Number(NumberState.NaN));
        }

        protected abstract Number Evaluate(Number number);

        public abstract IExpression? Simplify();

        public abstract IExpression? Differentiate(string byVariable);

        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
