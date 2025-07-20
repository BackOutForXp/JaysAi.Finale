// Monarch v1.0 – PerformanceTracker.cs

using System;
using System.Diagnostics;

namespace JaysAi.Finale.Utility
{
    public class PerformanceTracker
    {
        private Stopwatch stopwatch;
        private string label;
        private long lastElapsed;
        private bool logging;

        public PerformanceTracker(string label = "", bool startImmediately = true, bool enableLogging = false)
        {
            this.label = label;
            logging = enableLogging;
            stopwatch = new Stopwatch();

            if (startImmediately)
                stopwatch.Start();
        }

        public void Restart(string? newLabel = null)
        {
            if (!string.IsNullOrEmpty(newLabel))
                label = newLabel;

            stopwatch.Restart();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void Resume()
        {
            if (!stopwatch.IsRunning)
                stopwatch.Start();
        }

        public long ElapsedMilliseconds
        {
            get
            {
                lastElapsed = stopwatch.ElapsedMilliseconds;
                return lastElapsed;
            }
        }

        public void Log(string? extraInfo = null)
        {
            if (!logging) return;

            var message = $"{label} took {ElapsedMilliseconds} ms";
            if (!string.IsNullOrEmpty(extraInfo))
                message += $" | {extraInfo}";

            Console.WriteLine($"[PERF] {message}");
        }
    }
}
