using System;

namespace ExpressionEngine
{
    public interface IExpression: IFormattable
    {
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
    }
}
