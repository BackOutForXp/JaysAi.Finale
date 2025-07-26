// neural v3.0
using System;
using System.Diagnostics;

namespace JaysAi.Finale.SystemLogic
{
    public static class TimeUtils
    {
        private static readonly Stopwatch _globalStopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Returns total time in milliseconds since application started.
        /// </summary>
        public static long TimeSinceStartupMs => _globalStopwatch.ElapsedMilliseconds;

        /// <summary>
        /// Returns total time in seconds since application started.
        /// </summary>
        public static double TimeSinceStartupSec => _globalStopwatch.Elapsed.TotalSeconds;

        /// <summary>
        /// Gets the current UTC timestamp.
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets the current local time timestamp.
        /// </summary>
        public static DateTime LocalNow => DateTime.Now;

        /// <summary>
        /// Returns a formatted timestamp for logging or filename generation.
        /// </summary>
        public static string GetFormattedTimestamp(string format = "yyyy-MM-dd_HH-mm-ss") =>
            DateTime.UtcNow.ToString(format);

        /// <summary>
        /// Returns a high-resolution timestamp in ticks.
        /// </summary>
        public static long HighResTimestamp => Stopwatch.GetTimestamp();

        /// <summary>
        /// Converts Stopwatch ticks to milliseconds.
        /// </summary>
        public static double TicksToMilliseconds(long ticks) =>
            ticks * 1000.0 / Stopwatch.Frequency;

        /// <summary>
        /// Converts Stopwatch ticks to seconds.
        /// </summary>
        public static double TicksToSeconds(long ticks) =>
            ticks * 1.0 / Stopwatch.Frequency;
    }
}
