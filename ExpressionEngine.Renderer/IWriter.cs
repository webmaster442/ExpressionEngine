//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Renderer
{
    public interface IWriter
    {
        void Write(string format, params object[] arguments);
        void WriteLine(string format, params object[] arguments);
        void WriteLine();
        string ToString();
    }
}
