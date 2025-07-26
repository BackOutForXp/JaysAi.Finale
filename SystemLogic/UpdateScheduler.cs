// neural v3.0
using System;
using System.Timers;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class UpdateScheduler : IDisposable
    {
        private readonly Timer _updateTimer;
        private bool _isRunning;

        public event EventHandler? OnScheduledUpdate;

        public UpdateScheduler(double intervalInMinutes = 60)
        {
            _updateTimer = new Timer(intervalInMinutes * 60_000); // Convert minutes to milliseconds
            _updateTimer.Elapsed += HandleTimerElapsed;
            _updateTimer.AutoReset = true;
        }

        public void Start()
        {
            if (_isRunning) return;

            _updateTimer.Start();
            _isRunning = true;
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _updateTimer.Stop();
            _isRunning = false;
        }

        private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            OnScheduledUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Stop();
            _updateTimer.Dispose();
        }
    }
}
