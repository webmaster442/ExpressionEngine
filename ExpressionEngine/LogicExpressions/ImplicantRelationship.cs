﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.LogicExpressions
{
    internal class ImplicantRelationship
    {
        public Implicant A { get; set; }
        public Implicant B { get; set; }

        public ImplicantRelationship(Implicant first, Implicant second)
        {
            A = first;
            B = second;
        }
    }
}
