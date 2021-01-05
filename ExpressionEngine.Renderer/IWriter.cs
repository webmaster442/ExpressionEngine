//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Renderer
{
    public interface IWriter
    {
        void WriteLine(object? o);
        void Write(object? o);
        void Write(string format, params object[] arguments);
        void WriteLine(string format, params object[] arguments);
        string ToString();
    }
}
