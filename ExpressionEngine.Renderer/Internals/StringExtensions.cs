//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine.Renderer.Internals
{
    internal static class StringExtensions
    {
        private static IEnumerable<string> Split(string str, Predicate<char> controller)
        {
            int nextPiece = 0;

            for (int c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str[nextPiece..c];
                    nextPiece = c + 1;
                }
            }

            yield return str[nextPiece..];
        }

        private static string TrimMatchingQuotes(string input, char quote)
        {
            if ((input.Length >= 2)
                && (input[0] == quote)
                && (input[^1] == quote))
            {
                return input[1..^1];
            }
            return input;
        }

        public static string[] Tokenize(this string line)
        {
            var q = Split(line, c => c == ' ')
                        .Select(x => TrimMatchingQuotes(x, '"'));
            return q.ToArray();

        }

    }
}
