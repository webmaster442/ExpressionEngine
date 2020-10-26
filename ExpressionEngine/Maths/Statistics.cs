//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine.Maths
{
    public static class Statistics
    {
        public static StatisticResult Statistic(params double[] items)
        {
            return Statistic(items as IEnumerable<double>);
        }

        public static StatisticResult Statistic(IEnumerable<double> items)
        {
            var ordered = items.OrderBy(x => x).ToArray();

            StatisticResult result = new StatisticResult();

            if (ordered.Length > 0)
            {
                result.Maximum = ordered[ordered.Length - 1];
                result.Minimum = ordered[0];
            }

            double sum = 0;
            double currentValue = 0;
            double mode = 0;
            int occurence = 0;
            int maxOccurence = 0;
            double meanSquare = 0;
            int n = 0;
            double Ex = 0.0;
            double Ex2 = 0.0;
            foreach (var item in items)
            {
                sum += item;
                meanSquare += Math.Pow(item, 2);

                ++n;
                Ex += item - ordered[0];
                Ex2 += Math.Pow(item - ordered[0], 2);

                Mode(ref currentValue, ref mode, ref occurence, ref maxOccurence, item);

            }

            int itemIndex = ordered.Length / 2;

            if (itemIndex % 2 == 0)
                result.Median = (ordered[itemIndex] + ordered[itemIndex - 1]) / 2.0;
            else
                result.Median = ordered[itemIndex];

            if (maxOccurence > 1)
                result.Mode = mode;

            if (ordered.Length < 2)
                result.Variance = 0.0;
            else
                result.Variance = (Ex2 - (Ex * Ex) / n) / (n - 1);

            result.Sum = sum;

            result.StandardDeviation = Math.Sqrt(result.Variance);
            result.Range = result.Maximum - result.Minimum;
            result.Average = sum / ordered.Length;
            result.RootMeanSquare = Math.Sqrt(meanSquare / ordered.Length);

            return result;
        }

        private static void Mode(ref double current, ref double mode, ref int count, ref int max, double item)
        {
            if (current != item)
            {
                current = item;
                count = 1;
            }
            else
            {
                ++count;
            }

            if (count > max)
            {
                max = count;
                mode = current;
            }
        }
    }
}
