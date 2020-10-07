using ExpressionEngine.Base;
using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;

namespace ExpressionEngine
{
    public class Variables
    {
        private Dictionary<string, double> _lookupTable = new Dictionary<string, double>
        {
            { "pi", Math.PI },
            { "e", Math.E }
        };

        private HashSet<string> ProtectedNames = new HashSet<string>
        {
            "pi",
            "e",
        };

        public bool IsConstant(string variable)
        {
            return _lookupTable.ContainsKey(variable) 
                && ProtectedNames.Contains(variable);
        }

        public double GetValue(string variable)
        {
            if (_lookupTable.ContainsKey(variable))
            {
                return _lookupTable[variable];
            }
            throw new ExpressionEngineException($"{variable} is not defined");
        }

        public void Register(string variable, double value)
        {
            if (ProtectedNames.Contains(variable))
            {
                ExceptionHelper.ThrowException(Resources.ConstantCantReassign, variable);
                return;
            }
            
            _lookupTable[variable] = value;
        }
    }
}
