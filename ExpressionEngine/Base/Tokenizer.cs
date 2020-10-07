using ExpressionEngine.FunctionExpressions;
using ExpressionEngine.Properties;
using System.Globalization;
using System.Text;

namespace ExpressionEngine.Base
{
    internal class Tokenizer
    {
        private readonly string _function;
        private int _index;

        public Tokenizer(string function)
        {
            if (function == null)
            {
                function = string.Empty;
            }
            _function = function;
            _index = 0;
        }

        public Token Next()
        {
            while (_index < _function.Length)
            {
                if (char.IsNumber(_function[_index]))
                {
                    var number = new StringBuilder();
                    number.Append(_function[_index++]);
                    while (_index < _function.Length && char.IsNumber(_function[_index]))
                    {
                        number.Append(_function[_index++]);
                    }
                    return new Token(number.ToString(), TokenType.Constant);
                }
                if (IsAlpha(_function[_index]))
                {
                    var var = new StringBuilder();
                    var.Append(_function[_index++]);
                    while (_index < _function.Length && IsAlpha(_function[_index]))
                    {
                        var.Append(_function[_index++]);
                    }

                    string variable = var.ToString().ToLower(CultureInfo.InvariantCulture);

                    if (FunctionFactory.IsFunction(variable))
                    {
                        return new Token(variable, TokenType.Function);
                    }
                    else
                    {
                        return new Token(variable, TokenType.Variable);
                    }
                }
                switch (_function[_index++])
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        continue;

                    case '+':
                        return new Token("+", TokenType.Plus);
                    case '-':
                        return new Token("-", TokenType.Minus);
                    case '*':
                        return new Token("*", TokenType.Multiply);
                    case '/':
                        return new Token("/", TokenType.Divide);
                    case '^':
                        return new Token("^", TokenType.Exponent);
                    case '(':
                        return new Token("(", TokenType.OpenParen);
                    case ')':
                        return new Token(")", TokenType.CloseParen);
                    default:
                        ExceptionHelper.ThrowException(Resources.InvalidToken, _function[_index - 1], _function);
                        break;
                }
            }
            return new Token(string.Empty, TokenType.Eof);
        }

        private bool IsAlpha(char c)
        {
            return ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z');
        }
    }
}
