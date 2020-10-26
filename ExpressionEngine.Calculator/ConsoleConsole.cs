//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Calculator.Infrastructure;
using Mono.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine.Calculator
{
    internal class ConsoleConsole : IConsole
    {
        private LineEditor _editor;

        public int CurrentWidth => Console.BufferWidth;

        public string Prompt { get; set; }

        public Func<string, int, string[]>? AutocompleteLookup { get; set; }

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

        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleConsole()
        {
            _editor = new LineEditor(nameof(ExpressionEngine.Calculator));
            _editor.AutoCompleteEvent += OnAutoComplete;
            Prompt = string.Empty;
        }

        private LineEditor.Completion OnAutoComplete(string text, int pos)
        {
            string[]? results = AutocompleteLookup?.Invoke(text, pos);
            string[] emptyArray = new string[] { string.Empty };
            return new LineEditor.Completion(string.Empty, results ?? emptyArray);
        }

        public string[] ReadTokens()
        {
            string line = _editor.Edit(Prompt, string.Empty).ToLower();

            var q = Split(line, c => c == ' ')
                        .Select(x => TrimMatchingQuotes(x, '"'));
            return q.ToArray();

        }

        public void Write(string format, params object[] arguments)
        {
            Console.Write(format, arguments);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}