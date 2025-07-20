// File: System/TimerService.cs
// Monarch v2.1 – Global timer logic fully resolved

using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace JaysAi.Finale.SystemLogic
{
    public class TimerService
    {
        private readonly System.Timers.Timer _timer;

        public TimerService(double interval)
        {
            _timer = new Timer(interval)
            {
                AutoReset = true,
                Enabled = false
            };
            _timer.Elapsed += TimerElapsed;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Placeholder for future timed logic
            // Use this to trigger auto-updates, cleanup cycles, heartbeat checks, etc.
            // Example: Logger.Log("[TimerService] Tick.");
        }
    }
}
