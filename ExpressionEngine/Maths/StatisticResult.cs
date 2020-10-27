//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;

namespace ExpressionEngine.Maths
{
    public sealed class StatisticResult
    {
        /// <summary>
        /// Minimum value
        /// </summary>
        public double Minimum { get; internal set; }

        /// <summary>
        /// Maximum value
        /// </summary>
        public double Maximum { get; internal set; }

        /// <summary>
        /// Range is the length of the smallest interval which contains all the data.
        /// </summary>
        public double Range { get; internal set; }

        /// <summary>
        /// Median is the number separating the higher half of a sample, a population, or a probability distribution, from the lower half.
        /// </summary>
        public double Median { get; internal set; }

        /// <summary>
        /// Sum of all valuess
        /// </summary>
        public double Sum { get; internal set; }

        /// <summary>
        /// Variance is the measure of the amount of variation of all the scores for a variable (not just the extremes which give the range).
        /// </summary>
        public double Variance { get; internal set; }

        /// <summary>
        /// The Standard Deviation of a statistical population, a data set, or a probability distribution is the square root of its variance.
        /// </summary>
        public double StandardDeviation { get; internal set; }

        /// <summary>
        /// Mode is the value that occurs the most frequently in a data set or a probability distribution.
        /// </summary>
        public double Mode { get; internal set; }

        /// <summary>
        /// Root Mean Square is the measure of the magnitude of a varying series. 
        /// </summary>
        public double RootMeanSquare { get; internal set; }

        public double Average { get; set; }

        public StatisticResult()
        {
            Minimum = double.NaN;
            Maximum = double.NaN;
            Median = double.NaN;
            Range = double.NaN;
            Mode = double.NaN;
            Sum = double.NaN;
            Variance = double.NaN;
            StandardDeviation = double.NaN;
            RootMeanSquare = double.NaN;
            Average = double.NaN;
        }

        public string ToString(IFormatProvider format)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}\r\n", nameof(Minimum), Minimum.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Maximum), Maximum.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Range), Range.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Median), Median.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Mode), Mode.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Sum), Sum.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Average), Average.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(RootMeanSquare), RootMeanSquare.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(Variance), Variance.ToString(format));
            sb.AppendFormat("{0}: {1}\r\n", nameof(StandardDeviation), StandardDeviation.ToString(format));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
