//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Numbers;
using ExpressionEngine.Properties;
using System;

namespace ExpressionEngine.FunctionExpressions
{
    internal class NonDifferentiatableFunction : UnaryExpression
    {
        private readonly Func<Number, Number> _function;
        private readonly string _name;

        public NonDifferentiatableFunction(IExpression? child, Func<Number, Number> function, string name) : base(child)
        {
            _function = function;
            _name = name;
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new CannotDifferentiateException(Resources.CanotDifferentiate);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new NonDifferentiatableFunction(newChild, _function, _name);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"{_name}({Child})";
        }

        protected override Number Evaluate(Number number)
        {
            return _function.Invoke(number);
        }
    }
}
