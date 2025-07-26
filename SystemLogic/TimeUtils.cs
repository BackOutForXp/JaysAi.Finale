// Neural v3.1 — TimeUtils.cs
using System;
using System.Diagnostics;

namespace JaysAi.Finale.SystemLogic
{
    public static class TimeUtils
    {
        private static readonly Stopwatch _globalStopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Total time in milliseconds since app launch.
        /// </summary>
        public static long TimeSinceStartupMs => _globalStopwatch.ElapsedMilliseconds;

        /// <summary>
        /// Total time in seconds since app launch.
        /// </summary>
        public static double TimeSinceStartupSec => _globalStopwatch.Elapsed.TotalSeconds;

        /// <summary>
        /// Current UTC time.
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Current local time.
        /// </summary>
        public static DateTime LocalNow => DateTime.Now;

        /// <summary>
        /// Returns a formatted UTC timestamp (good for logs/files).
        /// </summary>
        public static string GetFormattedTimestamp(string format = "yyyy-MM-dd_HH-mm-ss") =>
            DateTime.UtcNow.ToString(format);

        /// <summary>
        /// High-resolution timestamp in Stopwatch ticks.
        /// </summary>
        public static long HighResTimestamp => Stopwatch.GetTimestamp();

        /// <summary>
        /// Converts Stopwatch ticks to milliseconds.
        /// </summary>
        public static double TicksToMilliseconds(long ticks) =>
            ticks * (1000.0 / Stopwatch.Frequency);

        /// <summary>
        /// Converts Stopwatch ticks to seconds.
        /// </summary>
        public static double TicksToSeconds(long ticks) =>
            ticks / (double)Stopwatch.Frequency;

        /// <summary>
        /// Returns time in seconds from now to a given DateTime.
        /// </summary>
        public static double SecondsSince(DateTime timestamp) =>
            (DateTime.UtcNow - timestamp).TotalSeconds;
    }
}
