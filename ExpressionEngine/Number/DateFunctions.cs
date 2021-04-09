//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace ExpressionEngine.Numbers
{
    internal static class DateFunctions
    {
        private readonly static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime UnixTimeToUtcDateTime(double value)
        {
            return epoch.AddSeconds(value);
        }

        public static double UtcDateTimeToUnixTime(DateTime now)
        {
            return (now - epoch).TotalSeconds;
        }
    }
}
