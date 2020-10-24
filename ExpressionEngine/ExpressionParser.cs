//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.BaseExpressions;
using ExpressionEngine.FunctionExpressions;
using ExpressionEngine.LogicExpressions;
using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpressionEngine
{
    public class ExpressionParser
    {
        private Tokenizer? _tokenizer;
        private Token _currentToken;
        private readonly TokenSet FirstMultExp;
        private readonly TokenSet FirstExpExp;
        private readonly TokenSet FirstUnaryExp;
        private readonly TokenSet FirstFactorPrefix;
        private readonly TokenSet FirstFactor;

        public ExpressionParser()
        {
            var firstFunction = new TokenSet(TokenType.Function1, TokenType.Function2);
            FirstFactor = firstFunction + new TokenSet(TokenType.Variable, TokenType.OpenParen);
            FirstFactorPrefix = FirstFactor + TokenType.Constant;
            FirstUnaryExp = FirstFactorPrefix + TokenType.Minus + TokenType.Not;
            FirstExpExp = new TokenSet(FirstUnaryExp);
            FirstMultExp = new TokenSet(FirstUnaryExp);
        }

        /// <summary>
        ///     Parse a string representation of a parametric function into an
        ///     expression tree that can be later evaluated.
        /// </summary>
        /// <param name="function">The function to parse</param>
        /// <returns>An expression tree representing the function parsed</returns>
        public IExpression? Parse(string function, IVariables variables)
        {
            _tokenizer = new Tokenizer(function);
            _currentToken = new Token("", TokenType.None);

            if (!Next())
            {
                throw new ExpressionEngineException(Resources.EmptyFunction);
            }
            var exp = ParseAddExpression(variables);
            var leftover = new StringBuilder();
            while (_currentToken.Type != TokenType.Eof)
            {
                leftover.Append(_currentToken.Value);
                Next();
            }
            if (leftover.Length > 0)
            {
                ExceptionHelper.ThrowException(Resources.TrailingChars, leftover);
            }
            return exp;
        }

        public IExpression? ParseMinterms(IEnumerable<int> minterms, IVariables variables, int varCount, bool msbA = true)
        {
            var ordered = minterms.OrderByDescending(m => m);

            List<string> expressions = new List<string>();
            foreach (var minterm in ordered)
            {
                string binary = Utilities.GetBinaryValue(minterm, varCount);
                expressions.Add(Utilities.GetMintermExpression(binary, msbA));
            }
            
            var final = string.Join('|', expressions);

            return Parse(final, variables);
        }

        private IExpression? ParseAddExpression(IVariables variables)
        {
            if (Check(FirstMultExp))
            {
                var exp = ParseMultExpression(variables);

                while (Check(new TokenSet(TokenType.Plus, TokenType.Minus, TokenType.Or)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstMultExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpression);
                    }
                    var right = ParseMultExpression(variables);

                    switch (opType)
                    {
                        case TokenType.Plus:
                            exp = new AddExpression(exp, right);
                            break;

                        case TokenType.Or:
                            exp = new OrExpression(exp, right);
                            break;

                        case TokenType.Minus:
                            exp = new SubtractExpression(exp, right);
                            break;

                        default:
                            ExceptionHelper.ThrowException(Resources.ExpectedPlussminus, opType);
                            break;
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private IExpression? ParseMultExpression(IVariables variables)
        {
            if (Check(FirstExpExp))
            {
                var exp = ParseExpExpression(variables);

                while (Check(new TokenSet(TokenType.Multiply, TokenType.Divide, TokenType.And)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstExpExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpressionAfterMultDiv);
                    }
                    var right = ParseExpExpression(variables);

                    switch (opType)
                    {
                        case TokenType.Multiply:
                            exp = new MultiplyExpression(exp, right);
                            break;

                        case TokenType.Divide:
                            exp = new DivideExpression(exp, right);
                            break;

                        case TokenType.And:
                            exp = new AndExpression(exp, right);
                            break;

                        default:
                            ExceptionHelper.ThrowException(Resources.ExpectedMultiplyDivide, opType);
                            break;
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private IExpression? ParseExpExpression(IVariables variables)
        {
            if (Check(FirstUnaryExp))
            {
                var exp = ParseUnaryExpression(variables);

                if (Check(new TokenSet(TokenType.Exponent)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstUnaryExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpressionAfterExponent);
                    }
                    var right = ParseUnaryExpression(variables);

                    switch (opType)
                    {
                        case TokenType.Exponent:
                            exp = new ExponentExpression(exp, right);
                            break;

                        default:
                            ExceptionHelper.ThrowException(Resources.ExpectedExponent, opType);
                            break;
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private IExpression? ParseUnaryExpression(IVariables variables)
        {
            var negate = false;
            var not = false;
            if (_currentToken.Type == TokenType.Minus)
            {
                Eat(TokenType.Minus);
                negate = true;
            }
            if (_currentToken.Type == TokenType.Not)
            {
                Eat(TokenType.Not);
                not = true;
            }

            if (Check(FirstFactorPrefix))
            {
                var exp = ParseFactorPrefix(variables);

                if (negate)
                {
                    return new NegateExpression(exp);
                }
                else if (not)
                {
                    return new NotExpression(exp);
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private IExpression? ParseFactorPrefix(IVariables variables)
        {
            IExpression? exp = null;
            if (_currentToken.Type == TokenType.Constant)
            {
                exp = new ConstantExpression(Convert.ToDouble(_currentToken.Value, CultureInfo.InvariantCulture));
                Eat(TokenType.Constant);
            }

            if (Check(FirstFactor))
            {
                if (exp == null)
                {
                    return ParseFactor(variables);
                }
                return new MultiplyExpression(exp, ParseFactor(variables));
            }
            // This should be impossible because bad symbols are caught by Tokenizer,
            //  constants would have been parsed in the if-statement above, and
            //  anything else is treated as a Factor (UndefinedVariableException
            //  will be thrown when you try to evaluate the function).
            if (exp == null)
            {
                throw new ExpressionEngineException(Resources.InvalidExpression);
            }
            return exp;
        }

        private IExpression? ParseFactor(IVariables variables)
        {
            IExpression? exp = null;
            do
            {
                IExpression? right = null;
                switch (_currentToken.Type)
                {
                    case TokenType.Variable:
                        right = new VariableExpression(_currentToken.Value, variables);
                        Eat(TokenType.Variable);
                        break;

                    case TokenType.Function1:
                        right = ParseFunction(variables);
                        break;
                    case TokenType.Function2:
                        right = ParseFunction2(variables);
                        break;

                    case TokenType.OpenParen:
                        Eat(TokenType.OpenParen);
                        right = ParseAddExpression(variables);
                        Eat(TokenType.CloseParen);
                        break;

                    default:
                        ExceptionHelper.ThrowException(Resources.UnexpectedTokenInFactior, _currentToken.Type);
                        break;
                }

                exp = (exp == null) ? right : new MultiplyExpression(exp, right);
            } while (Check(FirstFactor));

            return exp;
        }

        private IExpression? ParseFunction(IVariables variables)
        {
            var opType = _currentToken.Type;
            var function = _currentToken.Value;
            Eat(opType);
            Eat(TokenType.OpenParen);
            var exp = ParseAddExpression(variables);
            Eat(TokenType.CloseParen);

            if (opType == TokenType.Function1
                && FunctionFactory.IsSignleParamFunction(function))
            {
                return FunctionFactory.Create(function, exp);
            }
            else
            {
                ExceptionHelper.ThrowException(Resources.UnknownFuction, function);
                return null;
            }
        }

        private IExpression? ParseFunction2(IVariables variables)
        {
            var opType = _currentToken.Type;
            var function = _currentToken.Value;
            Eat(opType);
            Eat(TokenType.OpenParen);
            var exp1 = ParseAddExpression(variables);
            Eat(TokenType.ArgumentDivider);
            var exp2 = ParseAddExpression(variables);
            Eat(TokenType.CloseParen);

            if (opType == TokenType.Function2
                && FunctionFactory.IsTwoParamFunction(function))
            {
                return FunctionFactory.Create(function, exp1, exp2);
            }
            else
            {
                ExceptionHelper.ThrowException(Resources.UnknownFuction, function);
                return null;
            }
        }

        /// <summary>
        ///     Assign the next token in the queue to CurrentToken
        /// </summary>
        /// <returns>
        ///     true if there are still more tokens in the queue,
        ///     false if we have looked at all available tokens already
        /// </returns>
        private bool Next()
        {
            if (_currentToken.Type == TokenType.Eof)
            {
                throw new ExpressionEngineException(Resources.OutOfTokens);
            }
            _currentToken = _tokenizer?.Next() ?? new Token(string.Empty, TokenType.None);

            return _currentToken.Type != TokenType.Eof;
        }

        /// <summary>
        ///     Assign the next token in the queue to CurrentToken if the CurrentToken's
        ///     type matches that of the specified parameter.  If the CurrentToken's
        ///     type does not match the parameter, throw a syntax exception
        /// </summary>
        /// <param name="type">The type that your syntax expects CurrentToken to be</param>
        private void Eat(TokenType type)
        {
            if (_currentToken.Type != type)
            {
                ExceptionHelper.ThrowException(Resources.Missing, type);
            }
            Next();
        }

        /// <summary>
        ///     Check if the CurrentToken is a member of a set Token types
        /// </summary>
        /// <param name="tokens">The set of Token types to check against</param>
        /// <returns>
        ///     true if the CurrentToken's type is in the set
        ///     false if it is not
        /// </returns>
        private bool Check(TokenSet tokens) => tokens.Contains(_currentToken.Type);
    }
}
