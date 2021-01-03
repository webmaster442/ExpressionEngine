//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace ExpressionEngine.Renderer
{
    public class CommandException : Exception
    {
        public CommandException()
        {
        }

        public CommandException(string format, params string[] arguments) : base(string.Format(format, arguments))
        {
        }


        public CommandException(string? message) : base(message)
        {
        }

        public CommandException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
