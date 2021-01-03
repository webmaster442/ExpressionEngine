//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace ExpressionEngine.Renderer
{
    public class HtmlWriter : IWriter
    {
        private readonly StringBuilder _buffer;

        public HtmlWriter()
        {
            _buffer = new StringBuilder(4096);
        }

        public void Write(string format, params object[] arguments)
        {
            string str = string.Format(format, arguments);
            _buffer.AppendLine(str);
        }

        public void WriteLine(string format, params object[] arguments)
        {
            string str = string.Format(format, arguments);
            _buffer.AppendLine(str);
        }

        public void WriteLine()
        {
            _buffer.AppendLine();
        }

        public override string ToString()
        {
            return $"<pre><code>\r\n{_buffer}\r\n</code></pre>\r\n";
        }
    }
}
