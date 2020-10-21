//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Properties;
using System;

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
        public static double Integrate(this IExpression expression, string var, double from, double to, int steps = 1000)
        {
            if (to < from)
                throw new ArgumentException(Resources.IntegrateErrorRange);

            if (steps < 1)
                throw new ArgumentException(Resources.ToLessSteps);

            if (expression.Variables == null)
                throw new InvalidOperationException(Resources.NoVariables);

            if (string.IsNullOrEmpty(var))
                throw new ArgumentNullException(nameof(var));

            if (expression.IsConstantExpression())
            {
                // constant expression's integral is the constant.
                return expression.Evaluate();
            }

            double h = (to - from) / steps;
            double x = from + h;
            double s = 0.0;
            for (int i=1; i<steps/2; i++)
            {
                expression.Variables[var] = x;
                double Fx = expression.Evaluate();
                expression.Variables[var] = x + h;
                double Fx2 = expression.Evaluate();

                s = s + 2 * Fx + Fx2;
                x += 2 * h;
            }

            expression.Variables[var] = from;
            double Fxa = expression.Evaluate();
            expression.Variables[var] = to;
            double Fxb = expression.Evaluate();

            expression.Variables[var] = to - h;
            double Fxhb = expression.Evaluate();

            return h / 3 * (2 * Fxa + Fxb + 4 * Fxhb);
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
    }
}
