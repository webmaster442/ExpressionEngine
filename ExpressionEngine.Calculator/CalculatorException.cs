using System;
using System.Runtime.Serialization;

namespace ExpressionEngine.Calculator
{
    [Serializable]
    public class CalculatorException : Exception
    {
        public CalculatorException()
        {
        }

        public CalculatorException(string? message) : base(message)
        {
        }

        public CalculatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CalculatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
