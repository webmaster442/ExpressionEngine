//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Base
{
    internal enum TokenType: uint
    {
        None = 0x0,
        Constant = 0x1,
        Variable = 0x2,
        Not = 0x4,
        Plus = 0x8,
        Minus = 0x10,
        Multiply = 0x20,
        Divide = 0x40,
        And = 0x80,
        Or = 0x100,
        Exponent = 0x200,
        Function1 = 0x400,
        Function2 = 0x800,
        ArgumentDivider = 0x1000,

        OpenParen = 0x2000,
        CloseParen = 0x4000,

        Eof = 0x10000
    }
}
