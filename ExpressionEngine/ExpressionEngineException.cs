using System;
using System.Runtime.Serialization;

namespace ExpressionEngine
{
    [Serializable]
    public class ExpressionEngineException : Exception
    {
        public ExpressionEngineException() : base()
        {
        }

        public ExpressionEngineException(string message) : base(message)
        {
        }

        public ExpressionEngineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExpressionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
