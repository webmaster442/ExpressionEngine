//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace ExpressionEngine.LogicExpressions
{
    internal class Utilities
    {
        public static void GetBalanced(ref string a, ref string b)
        {
            if (a.Length < b.Length)
            {
                a.PadLeft(b.Length - a.Length, '0');
            }
            else
            {
                b.PadLeft(a.Length - b.Length, '0');
            }
        }

        public static int GetDifferences(string a, string b)
        {
            GetBalanced(ref a, ref b);

            int differences = 0;

            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    differences++;

            return differences;
        }

        public static int GetOneCount(string a)
        {
            int count = 0;

            foreach (char c in a)
                if (c == '1')
                    count++;

            return count;
        }

        public static string GetMask(string a, string b)
        {
            GetBalanced(ref a, ref b);

            string final = string.Empty;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    final += '-';
                else
                    final += a[i];
            }

            return final;
        }

        public static bool ContainsSubList(List<int> list, List<int> OtherList)
        {
            bool ret = true;
            foreach (var item in OtherList)
            {
                if (!list.Contains(item))
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        public static bool ContainsAtleastOne(List<int> list, List<int> OtherList)
        {
            bool ret = false;
            foreach (var item in OtherList)
            {
                if (list.Contains(item))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
    }
}
