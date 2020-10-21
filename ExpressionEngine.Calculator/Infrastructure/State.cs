//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
            get => _variables[variable];
            set => _variables[variable] = value;
        }

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

        public bool TrySetValue(string variable, double value)
        {
            _variables[variable] = value;
            return true;
        }
    }
}
