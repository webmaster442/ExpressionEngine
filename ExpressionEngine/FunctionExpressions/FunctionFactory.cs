//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Maths;
using ExpressionEngine.Numbers;
using System;
using System.Collections.Generic;

namespace ExpressionEngine.FunctionExpressions
{
    internal static class FunctionFactory
    {
        private static readonly Dictionary<string, Func<IExpression?, IExpression>> SingleParamFunctions 
            = new Dictionary<string, Func<IExpression?, IExpression>>
        {
                { "ln", (child) => new LnExpression(child) },
                { "sin", (child) => new SinExpression(child) },
                { "cos", (child) => new CosExpression(child) },
                { "tan", (child) => new TanExpression(child) },
                { "ctg", (child) => new CtgExpression(child) },
                { "arcsin", (child) => new NonDifferentiatableFunction(child, NumberMath.ArcSin, "arcsin") },
                { "arccos", (child) => new NonDifferentiatableFunction(child, NumberMath.ArcCos, "arccos") },
                { "arctan", (child) => new NonDifferentiatableFunction(child, NumberMath.ArcTan, "arctan") },
                { "arcctg", (child) => new NonDifferentiatableFunction(child, NumberMath.ArcCtg, "arcctg") },
                { "deg2rad", (child) => new NonDifferentiatableFunction(child, NumberMath.DegToRad, "deg2rad")  },
                { "rad2deg", (child) => new NonDifferentiatableFunction(child, NumberMath.RadToDeg, "rad2deg")  },
                { "grad2deg", (child) => new NonDifferentiatableFunction(child, NumberMath.GradToDeg, "grad2deg")  },
                { "deg2grad", (child) => new NonDifferentiatableFunction(child, NumberMath.DegToGrad, "deg2grad")  },
                { "grad2rad", (child) => new NonDifferentiatableFunction(child, NumberMath.GradToRad, "grad2rad")  },
                { "rad2grad", (child) => new NonDifferentiatableFunction(child, NumberMath.RadToGrad, "rad2grad")  },
                { "factorial", (child) => new NonDifferentiatableFunction(child, FactorialWrapper, "factorial")  },

        };

        private static Number FactorialWrapper(Number arg)
        {
            return NumberMath.Factorial((int)arg.ToDouble());
        }

        private static readonly Dictionary<string, Func<IExpression?, IExpression?, IExpression>> TwoParamFunctions
            = new Dictionary<string, Func<IExpression?, IExpression?, IExpression>>
            {
                { "root", (child1, child2) => new RootExpression(child1, child2) },
                { "log", (child1, child2) => new LogExpression(child1, child2) },
            };

        public static bool IsSignleParamFunction(string identifier)
        {
            return SingleParamFunctions.ContainsKey(identifier);
        }

        public static bool IsTwoParamFunction(string identifier)
        {
            return TwoParamFunctions.ContainsKey(identifier);
        }

        internal static IExpression Create(string function, IExpression? exp)
        {
            return SingleParamFunctions[function](exp);
        }

        internal static IExpression Create(string function, IExpression? exp1, IExpression? exp2)
        {
            return TwoParamFunctions[function](exp1, exp2);
        }
    }
}
