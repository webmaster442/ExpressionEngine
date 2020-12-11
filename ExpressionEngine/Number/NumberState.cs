//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Numbers
{
    internal enum NumberState
    {
        PositiveInfinity = 0xF000,
        NegativeInfinity = 0x0F00,
        NaN = 0x00F0,
        Value = 0x000F,
    }
}
