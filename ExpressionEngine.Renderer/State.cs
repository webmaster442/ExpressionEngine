//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ExpressionEngine.Renderer
{
    internal class State : IState
    {
        private Dictionary<string, INumber> _variables;
        private Dictionary<string, INumber> _contants;
        private Dictionary<string, IExpression> _expressions;

        public State()
        {
            _contants = new Dictionary<string, INumber>
            {
                { "pi", NumberHelper.Pi },
                { "e", NumberHelper.E }
            };
            _variables = new Dictionary<string, INumber>();
            _expressions = new Dictionary<string, IExpression>();
        }

        public INumber this[string variable] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => _variables.Count;

        public IEnumerable<string> VariableNames => _variables.Keys;

        public void Clear()
        {
            _variables.Clear();
        }

        public bool IsConstant(string variableName)
        {
            return _contants.ContainsKey(variableName);
        }

        public void SetExpression(string variableName, IExpression? expression)
        {
            if (expression == null) return;
            if (_variables.ContainsKey(variableName))
                _variables.Remove(variableName);

            _expressions[variableName] = expression;
        }

        public bool TrySetValue(string variable, INumber value)
        {
            if (IsConstant(variable))
            {
                return false;
            }
            else
            {
                if (_expressions.ContainsKey(variable))
                {
                    _expressions.Remove(variable);
                }

                _variables[variable] = value;
                return true;
            }
        }
    }
}
