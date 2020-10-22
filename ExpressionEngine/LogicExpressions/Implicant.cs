//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEngine.LogicExpressions
{
    internal sealed class Implicant : IEquatable<Implicant?>
    {
        public string Mask { get; set; } //number mask.
        public List<int> Minterms { get; }

        public Implicant()
        {
            Mask = string.Empty;
            Minterms = new List<int>(); //original integers in group.
        }

        public string ToString(int length, bool lsba = false, bool negate = false)
        {
            var strFinal = new StringBuilder();
            var mask = Mask;

            while (mask.Length != length)
                mask = "0" + mask;

            if (!lsba)
            {
                for (int i = 0; i < mask.Length; i++)
                {
                    if (negate)
                    {
                        if (mask[i] == '0') strFinal.AppendFormat("{0} |", Convert.ToChar(65 + i));
                        else if (mask[i] == '1') strFinal.AppendFormat("!{0} |", Convert.ToChar(65 + i));
                    }
                    else
                    {
                        if (mask[i] == '0') strFinal.AppendFormat("!{0}", Convert.ToChar(65 + i));
                        else if (mask[i] == '1') strFinal.AppendFormat("{0}", Convert.ToChar(65 + i));
                    }
                }
            }
            else
            {
                for (int i = 0; i < mask.Length; i++)
                {
                    if (negate)
                    {
                        if (mask[i] == '0') strFinal.AppendFormat("{0} |", Convert.ToChar((65 + (length - 1)) - i));
                        else if (mask[i] == '1') strFinal.AppendFormat("!{0} |", Convert.ToChar((65 + (length - 1)) - i));
                        if (i != mask.Length - 1) strFinal.Append("|");
                    }
                    else
                    {
                        if (mask[i] == '0') strFinal.AppendFormat("!{0}", Convert.ToChar((65 + (length - 1)) - i));
                        else if (mask[i] == '1') strFinal.AppendFormat("!{0}", Convert.ToChar((65 + (length - 1)) - i));
                    }
                }
            }
            if (negate) return "(" + strFinal.Remove(strFinal.Length - 1, 1) + ")";
            return strFinal.ToString();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Implicant);
        }

        public bool Equals(Implicant? other)
        {
            return other != null &&
                   Mask == other.Mask &&
                   Minterms.SequenceEqual(other.Minterms);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Mask, Minterms);
        }
    }
}
