//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Base
{
    internal static class ExceptionHelper
    {
        public static void ThrowException(string format, params object[] parameters)
        {
            throw new ExpressionEngineException(string.Format(format, parameters));
        }
    }
}
