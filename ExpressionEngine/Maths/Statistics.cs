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
            return Statistic(items);
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
            double mean = 0;
            double M2 = 0;
            double currentValue = 0;
            double mode = 0;
            int occurence = 0;
            int maxOccurence = 0;
            double meanSquare = 0;
            foreach (var item in items)
            {
                sum += item;
                double delta = item - mean;
                mean += delta / ordered.Length;
                M2 += delta * (item - mean);
                meanSquare += Math.Pow(item, 2);

                Mode(ref currentValue, ref mode, ref occurence, ref maxOccurence, item);

            }

            int itemIndex = ordered.Length / 2;

            if (itemIndex % 2 == 0)
                result.Median = (ordered[itemIndex] + ordered[itemIndex - 1]) / 2.0;
            else
                result.Median = ordered[itemIndex];

            if (maxOccurence > 1)
                result.Mode = mode;

            result.Sum = sum;
            result.Variance = M2 / (ordered.Length - 1);
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
