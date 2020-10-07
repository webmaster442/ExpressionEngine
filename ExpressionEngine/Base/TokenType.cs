namespace ExpressionEngine.Base
{
    internal enum TokenType: uint
    {
        None = 0x0,
        Constant = 0x1,
        Variable = 0x2,
        Plus = 0x4,
        Minus = 0x8,
        Multiply = 0x10,
        Divide = 0x20,
        Exponent = 0x40,
        Function = 0x80,

        OpenParen = 0x400,
        CloseParen = 0x800,

        Eof = 0x1000
    }
}
