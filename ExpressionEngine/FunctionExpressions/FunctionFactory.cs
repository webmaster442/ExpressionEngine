using ExpressionEngine.Maths;
using System;
using System.Collections.Generic;

namespace ExpressionEngine.FunctionExpressions
{
    internal static class FunctionFactory
    {
        private static readonly Dictionary<string, Func<IExpression?, IExpression>> Functions 
            = new Dictionary<string, Func<IExpression?, IExpression>>
        {
                { "sin", (child) => new SinExpression(child) },
                { "cos", (child) => new CosExpression(child) },
                { "tan", (child) => new TanExpression(child) },
                { "ctg", (child) => new CtgExpression(child) },
                { "arcsin", (child) => new NonDifferentiatableFunction(child, Trigonometry.ArcSin, "arcsin") },
                { "arccos", (child) => new NonDifferentiatableFunction(child, Trigonometry.ArcCos, "arccos") },
                { "arctan", (child) => new NonDifferentiatableFunction(child, Trigonometry.ArcTan, "arctan") },
                { "arcctg", (child) => new NonDifferentiatableFunction(child, Trigonometry.ArcCtg, "arcctg") },
                { "deg2rad", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.DegToRad, "deg2rad")  },
                { "rad2deg", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.RadToDeg, "rad2deg")  },
                { "grad2deg", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.GradToDeg, "grad2deg")  },
                { "deg2grad", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.DegToGrad, "deg2grad")  },
                { "grad2rad", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.GradToRad, "grad2rad")  },
                { "rad2grad", (child) => new NonDifferentiatableFunction(child, DoubleFunctions.RadToGrad, "rad2grad")  },

        };

        public static bool IsFunction(string identifier)
        {
            return Functions.ContainsKey(identifier);
        }

        internal static IExpression Create(string function, IExpression? exp)
        {
            return Functions[function](exp);
        }
    }
}
