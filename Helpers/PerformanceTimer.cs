// Neural v3.0 — PerformanceTimer.cs
using System;
using System.Diagnostics;

namespace JaysAi.Finale.Helpers
{
    public class PerformanceTimer
    {
        private Stopwatch stopwatch;

        public PerformanceTimer()
        {
            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            if (!stopwatch.IsRunning)
                stopwatch.Start();
        }

        public void Stop()
        {
            if (stopwatch.IsRunning)
                stopwatch.Stop();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public void Restart()
        {
            stopwatch.Restart();
        }

        public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;

        public double ElapsedSeconds => stopwatch.Elapsed.TotalSeconds;

        public TimeSpan Elapsed => stopwatch.Elapsed;

        public bool IsRunning => stopwatch.IsRunning;

        public string FormatElapsed()
        {
            return $"{Elapsed.Minutes:D2}:{Elapsed.Seconds:D2}.{Elapsed.Milliseconds:D3}";
        }
    }
}
