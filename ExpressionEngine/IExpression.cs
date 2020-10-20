//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace ExpressionEngine
{
    public interface IExpression
    {
        /// <summary>
        /// Variables availabe for Expression
        /// </summary>
        Variables? Variables { get; }

        /// <summary>
        /// Evaluate an expression
        /// </summary>
        /// <returns>Result of expression</returns>
        double Evaluate();
        /// <summary>
        /// Diferentiate an expression
        /// </summary>
        /// <param name="byVariable">Diferentation variable</param>
        /// <returns>Diferentiated expression</returns>
        IExpression? Differentiate(string byVariable);
        /// <summary>
        /// Simplifies an expresion
        /// </summary>
        /// <returns></returns>
        IExpression? Simplify();
        /// <summary>
        /// Converts expression to string
        /// </summary>
        /// <param name="formatProvider">format provider</param>
        /// <returns>string representation of expression</returns>
        string ToString(IFormatProvider formatProvider);
    }
}
