//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Maths;
using System;
using System.Collections.Generic;

namespace ExpressionEngine.Calculator.Infrastructure
{
    internal class State : IVariables
    {
        private Dictionary<string, double> _variables;
        private Dictionary<string, double> _contants;
        private Dictionary<string, IExpression> _expressions;

        public State()
        {
            _contants = new Dictionary<string, double>
            {
                { "pi", Math.PI },
                { "e", Math.E }
            };
            _variables = new Dictionary<string, double>();
            _expressions = new Dictionary<string, IExpression>();
        }

        public double this[string variable]
        {
            get
            {
                if (_contants.ContainsKey(variable))
                    return _contants[variable];
                else if (_variables.ContainsKey(variable))
                    return _variables[variable];
                else
                    throw new ExpressionEngineException($"Unknown variable: {variable}");
            }
            set
            {
                if (IsConstant(variable))
                    throw new ExpressionEngineException("Can't overwrite a constant");

                if (_expressions.ContainsKey(variable))
                {
                    _expressions.Remove(variable);
                }

                _variables[variable] = value;
            }
        }

        public int Count => _variables.Count;

        public IEnumerable<string> VariableNames => _variables.Keys;

        public AngleMode AngleMode
        {
            get => Trigonometry.AngleMode;
            set => Trigonometry.AngleMode = value;
        }

        public void Clear()
        {
            _variables.Clear();
        }

        public bool IsConstant(string variableName)
        {
            return _contants.ContainsKey(variableName);
        }

        public bool TrySetValue(string variable, double value)
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

        public bool TryGetExpression(string variableName, out IExpression? expression)
        {
            expression = null;
            if (_expressions.ContainsKey(variableName))
            {
                expression = _expressions[variableName];
                return true;
            }
            return false;
        }

        public void SetExpression(string variableName, IExpression? expression)
        {
            if (expression == null) return;
            if (_variables.ContainsKey(variableName))
                _variables.Remove(variableName);

            _expressions[variableName] = expression;
        }
    }
}
