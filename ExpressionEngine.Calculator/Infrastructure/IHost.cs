//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal interface IHost
    {
        IEnumerable<string> Commands { get; }
    }
}
