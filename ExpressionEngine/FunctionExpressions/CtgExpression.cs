﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Maths;
using ExpressionEngine.Numbers;
using System;


namespace ExpressionEngine.FunctionExpressions
{
    internal sealed class CtgExpression : UnaryExpression
    {
        public CtgExpression(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new NegateExpression(new MultiplyExpression(new ExponentExpression(new SinExpression(Child), new ConstantExpression(-2)), Child?.Differentiate(byVariable)));
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is ConstantExpression childConst)
            {
                // child is constant
                return new ConstantExpression(Evaluate(childConst.Value));
            }
            return new CtgExpression(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"ctg({Child})";
        }

        protected override Number Evaluate(Number number)
        {
            return NumberMath.Round(NumberMath.Ctg(number), 21);
        }
    }
}
