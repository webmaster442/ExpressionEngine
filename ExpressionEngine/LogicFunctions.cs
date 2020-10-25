//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.LogicExpressions;
using ExpressionEngine.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine
{
    public static class LogicFunctions
    {

        public static IEnumerable<int> GetMinterms(int function, int variables)
        {
            string binary = GetBinary(function, variables);

            int i = 0;
            foreach (var chr in binary.Reverse())
            {
                if (chr == '1')
                    yield return i;
                ++i;
            }
        }

        public static IEnumerable<int> GetMaxterms(int function, int variables)
        {
            string binary = GetBinary(function, variables);

            int i = 0;
            foreach (var chr in binary.Reverse())
            {
                if (chr == '0')
                    yield return i;
                ++i;
            }
        }

        private static string GetBinary(int function, int variables)
        {
            if (variables > 24)
                throw new ExpressionEngineException(Resources.ToManyVariables);

            int maxValue = (1 << (int)Math.Pow(2, variables)) - 1;

            if (function > maxValue)
                throw new ExpressionEngineException(Resources.FunctionRequiresMoreVariables);

            string binary = Utilities.GetBinaryValue(function, variables);
            return binary;
        }
    }
}
