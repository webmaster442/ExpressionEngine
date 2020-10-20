//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Base;
using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;

namespace ExpressionEngine
{
    public class Variables
    {
        private readonly Dictionary<string, double> _lookupTable = new Dictionary<string, double>
        {
            { "pi", Math.PI },
            { "e", Math.E }
        };

        private readonly HashSet<string> _protectedNames = new HashSet<string>
        {
            "pi",
            "e",
        };

        public bool IsConstant(string variable)
        {
            return _lookupTable.ContainsKey(variable) 
                && _protectedNames.Contains(variable);
        }

        public double GetValue(string variable)
        {
            if (_lookupTable.ContainsKey(variable))
            {
                return _lookupTable[variable];
            }
            throw new ExpressionEngineException($"{variable} is not defined");
        }

        public double this[string name]
        {
            get { return GetValue(name); }
            set { Register(name, value); }
        }

        public void Register(string variable, double value)
        {
            if (_protectedNames.Contains(variable))
            {
                ExceptionHelper.ThrowException(Resources.ConstantCantReassign, variable);
                return;
            }
            
            _lookupTable[variable] = value;
        }
    }
}
