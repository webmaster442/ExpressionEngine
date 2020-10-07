using System;
using System.Runtime.Serialization;

namespace ExpressionEngine
{
    [Serializable]
    public sealed class CannotDifferentiateException : ExpressionEngineException
    {
        public CannotDifferentiateException() : base()
        {
        }

        public CannotDifferentiateException(string message) : base(message)
        {
        }

        public CannotDifferentiateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private CannotDifferentiateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
