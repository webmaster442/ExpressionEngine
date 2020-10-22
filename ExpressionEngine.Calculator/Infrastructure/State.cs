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

        public State()
        {
            _contants = new Dictionary<string, double>
            {
                { "pi", Math.PI },
                { "e", Math.E }
            };
            _variables = new Dictionary<string, double>();
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
                _variables[variable] = value;
                return true;
            }
        }
    }
}
