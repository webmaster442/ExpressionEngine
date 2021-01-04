//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Renderer.Properties;
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

        public INumber this[string variable]
        {
            get
            {
                if (_contants.ContainsKey(variable))
                    return _contants[variable];
                else if (_variables.ContainsKey(variable))
                    return _variables[variable];
                else
                    throw new CommandException(Resources.ErrorUnknownVariable, variable);
            }
            set
            {
                if (IsConstant(variable))
                    throw new CommandException(Resources.ErrorConstantWriteDelete);

                if (_expressions.ContainsKey(variable))
                {
                    _expressions.Remove(variable);
                }

                _variables[variable] = value;
            }
        }

        public int Count => _variables.Count + _contants.Count + _expressions.Count;

        public IEnumerable<string> VariableNames => _variables.Keys;

        public INumber Ans
        {
            get { return this["ans"]; }
            set { this["ans"] = value; }
        }

        public void Clear(string? variableName = null)
        {
            if (string.IsNullOrWhiteSpace(variableName))
            {
                _variables.Clear();
                _expressions.Clear();
                return;
            }

            if (_contants.ContainsKey(variableName))
                throw new CommandException(Resources.ErrorConstantWriteDelete);

            if (_variables.ContainsKey(variableName))
                _variables.Remove(variableName);

            if (_expressions.ContainsKey(variableName))
                _expressions.Remove(variableName);
        }

        void IVariables.Clear()
        {
            Clear(null);
        }

        public IExpression? GetExpression(string variableName)
        {
            if (!IsExpression(variableName)) return null;
            return _expressions[variableName];
        }

        public bool IsConstant(string variableName)
        {
            return _contants.ContainsKey(variableName);
        }

        public bool IsExpression(string variableName)
        {
            return _expressions.ContainsKey(variableName);
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

        public bool IsDefined(string variable)
        {
            return _contants.ContainsKey(variable)
                || _variables.ContainsKey(variable);
        }
    }
}
