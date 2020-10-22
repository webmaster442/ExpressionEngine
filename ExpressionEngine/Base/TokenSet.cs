//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Base
{
    internal sealed class TokenSet
    {
        private readonly uint _tokens;

        public TokenSet(TokenType token)
        {
            _tokens = (uint)token;
        }

        public TokenSet(params TokenType[] tokens)
        {
            foreach (var token in tokens)
            {
                _tokens |= (uint)token;
            }
        }

        public TokenSet(TokenSet set)
        {
            _tokens = set._tokens;
        }

        private TokenSet(uint tokens)
        {
            _tokens = tokens;
        }

        public static TokenSet operator +(TokenSet t1, TokenSet t2)
        {
            return new TokenSet(t1._tokens | t2._tokens);
        }

        public static TokenSet operator +(TokenSet t1, TokenType t2)
        {
            return new TokenSet(t1._tokens | (uint)t2);
        }

        public bool Contains(TokenType type)
        {
            return (_tokens & (uint)type) != 0;
        }
    }
}
