using ExpressionEngine.BaseExpressions;
using ExpressionEngine.Properties;
using System;
using System.Globalization;

namespace ExpressionEngine.FunctionExpressions
{
    internal sealed class VariableExpression : IExpression
    {
        public Variables? Variables { get; }

        public string Identifier { get; }


        public VariableExpression(string identifier, Variables variables)
        {
            Variables = variables;
            Identifier = identifier.ToLower();
        }

        public IExpression Differentiate(string byVariable)
        {
            if (byVariable == Identifier)
            {
                // f(x) = x
                // d( f(x) ) = 1 * d( x )
                // d( x ) = 1
                // f'(x) = 1
                return new ConstantExpression(1);
            }
            // f(x) = c
            // d( f(x) ) = c * d( c )
            // d( c ) = 0
            // f'(x) = 0
            return new ConstantExpression(0);
        }

        public double Evaluate()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            return Variables.GetValue(Identifier);
        }

        public IExpression Simplify()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            if (Variables.IsConstant(Identifier))
            {
                return new ConstantExpression(Variables.GetValue(Identifier));
            }
            return new VariableExpression(Identifier, Variables);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Identifier.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
