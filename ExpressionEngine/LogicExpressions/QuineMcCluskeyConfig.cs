//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.LogicExpressions
{
    public class QuineMcCluskeyConfig
    {
        /// <summary>
        /// If set, returns hazard free version of the expression
        /// </summary>
        public bool HazardFree { get; set; }

        /// <summary>
        /// If set, A variable is treated as the least significant
        /// </summary>
        public bool AIsLsb { get; set; }

        /// <summary>
        /// Negate the result expresion or not
        /// </summary>
        public bool Negate { get; set; }
    }
}
