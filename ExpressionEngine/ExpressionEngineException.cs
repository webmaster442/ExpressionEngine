//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
