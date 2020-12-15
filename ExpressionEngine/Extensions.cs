//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.FunctionExpressions;
using ExpressionEngine.LogicExpressions;
using ExpressionEngine.Maths;
using ExpressionEngine.Numbers;
using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine
{
    /// <summary>
    /// Expression extension: Allows integrating of expression
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Integrates the expression with Simpson algorithm
        /// </summary>
        /// <param name="expression">Expression to integrate</param>
        /// <param name="var">Integrate by which expression</param>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="steps">Number of steps</param>
        /// <returns>Integration result</returns>
        public static INumber Integrate(this IExpression expression, string var, INumber from, INumber to, int steps = 2048)
        {
            Number? _from = from as Number;
            Number? _to = to as Number;

            if (_from == null || _to == null)
                throw new ExpressionEngineException();

            if (_to < _from)
                throw new ArgumentException(Resources.IntegrateErrorRange);

            if (steps < 2 || (steps % 2 == 1))
                throw new ArgumentException(Resources.InvalidSteps);

            var flatExpression = expression.Flatten();

            var vars = flatExpression.FirstOrDefault(e => e.Variables != null)?.Variables;

            bool trigonometric = flatExpression.Any(e => IsTrigonometricNode(e));

            if (trigonometric)
            {
                switch (NumberMath.AngleMode)
                {
                    case AngleMode.Deg:
                        _from = NumberMath.DegToRad(_from);
                        _to = NumberMath.DegToRad(_to);
                        break;
                    case AngleMode.Grad:
                        _from = NumberMath.DegToGrad(_from);
                        _to = NumberMath.DegToGrad(_to);
                        break;
                }
            }

            if (vars == null)
                throw new InvalidOperationException(Resources.NoVariables);

            if (string.IsNullOrEmpty(var))
                throw new ArgumentNullException(nameof(var));

            AngleMode old = NumberMath.AngleMode;
            NumberMath.AngleMode = AngleMode.Rad;

            Number h = (_to - _from) / steps;
            Number x = _from + h;
            Number s = 0.0;
            for (int i=1; i<steps/2; i++)
            {
                vars[var] = x;
                Number Fx = (expression.Evaluate() as Number)!;
                vars[var] = x + h;
                Number Fx2 = (expression.Evaluate() as Number)!;

                s = s + (2 * Fx) + Fx2;
                x += 2 * h;
            }

            vars[var] = from;
            Number Fxa = (expression.Evaluate() as Number)!;
            vars[var] = to;
            Number Fxb = (expression.Evaluate() as Number)!;

            vars[var] = _to - h;
            Number Fxhb = (expression.Evaluate() as Number)!;

            Number result = (h / 3) * ((2 * s) + Fxa + Fxb + (4 * Fxhb));

            NumberMath.AngleMode = old;

            return NumberMath.Round(result, 15);
        }

        /// <summary>
        /// Returns true, if the expression only contains constants.
        /// </summary>
        /// <param name="expression">Expression to check</param>
        /// <returns>True, if the expression is only containing constants and can be evaluated</returns>
        public static bool IsConstantExpression(this IExpression expression)
        {
            return expression is ConstantExpression;
        }

        public static IEnumerable<IExpression> Flatten(this IExpression expression)
        {
            Stack<IExpression> expressions = new Stack<IExpression>();
            
            expressions.Push(expression);

            while (expressions.Count > 0)
            {
                var n = expressions.Pop();
              
                if (n != null)
                {
                    yield return n;
                }

                if (n is BinaryExpression bin)
                {
                    if (bin.Left != null)
                        expressions.Push(bin.Left);
                    if (bin.Right != null)
                        expressions.Push(bin.Right);
                }
                else if (n is UnaryExpression un)
                {
                    if (un.Child != null)
                        expressions.Push(un.Child);
                }
            }
        }

        /// <summary>
        /// Get minterms of a logical expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>Minterm collection</returns>
        public static IEnumerable<int> GetMinterms(this IExpression expression)
        {
            if (!IsLogicExpression(expression))
                throw new ExpressionEngineException(Resources.NotLogicExpression);

            var flatExpression = expression.Flatten();

            var vars = flatExpression.FirstOrDefault(e => e.Variables != null)?.Variables;

            if (vars == null)
                throw new InvalidOperationException(Resources.NoVariables);

            var varNames = flatExpression
                .Where(x => x is VariableExpression)
                .Cast<VariableExpression>()
                .Select(v => v.Identifier)
                .ToArray();

            if (varNames.Length > 24)
                throw new ExpressionEngineException(Resources.ToManyVariables);

            int combinations = 1 << varNames.Length;

            for (int i=0; i<combinations; i++)
            {
                string pattern = Utilities.GetBinaryValue(i, varNames.Length);
                for (int j=0; j<varNames.Length; j++)
                {
                    vars[varNames[j]] = pattern[j] == '1' ? Number.One : Number.Zero;
                }
                if ((expression.Evaluate() as Number) == Number.One)
                {
                    yield return i;
                }
            }
        }

        private static bool IsLogicExpressionNode(IExpression expression)
        {
            return expression is AndExpression
                || expression is OrExpression
                || expression is NotExpression
                || expression is ConstantExpression
                || expression is VariableExpression;
        }

        private static bool IsTrigonometricNode(IExpression expression)
        {
            return expression is SinExpression
                || expression is CosExpression
                || expression is TanExpression
                || expression is CtgExpression;
        }

        public static bool IsLogicExpression(this IExpression expression)
        {
            return expression
                    .Flatten()
                    .All(n => IsLogicExpressionNode(n));
        }
    }
}
