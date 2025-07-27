// Neural v3.1 — TimerService.cs
using System;
using System.Diagnostics;

namespace JaysAi.Finale.Utility
{
    public class TimerService
    {
        private readonly Stopwatch _stopwatch;

        public TimerService()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public float ElapsedSeconds => _stopwatch.ElapsedMilliseconds / 1000f;

        public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

        public void Restart()
        {
            _stopwatch.Restart();
        }

        public bool HasElapsed(float seconds)
        {
            return ElapsedSeconds >= seconds;
        }

        public bool HasElapsedMilliseconds(long milliseconds)
        {
            return ElapsedMilliseconds >= milliseconds;
        }
    }
}
