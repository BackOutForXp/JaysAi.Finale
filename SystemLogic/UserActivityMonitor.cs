// neural v3.0
using System;
using System.Timers;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class UserActivityMonitor : IDisposable
    {
        private readonly Timer _activityTimer;
        private DateTime _lastActivityTime;
        private readonly TimeSpan _inactivityThreshold;

        public event EventHandler? OnInactivityDetected;
        public event EventHandler? OnUserActivityDetected;

        public UserActivityMonitor(TimeSpan? inactivityThreshold = null, double checkIntervalMs = 5000)
        {
            _inactivityThreshold = inactivityThreshold ?? TimeSpan.FromMinutes(5);
            _lastActivityTime = DateTime.UtcNow;

            _activityTimer = new Timer(checkIntervalMs);
            _activityTimer.Elapsed += CheckUserActivity;
            _activityTimer.AutoReset = true;
        }

        public void Start() => _activityTimer.Start();

        public void Stop() => _activityTimer.Stop();

        public void ReportActivity()
        {
            _lastActivityTime = DateTime.UtcNow;
            OnUserActivityDetected?.Invoke(this, EventArgs.Empty);
        }

        private void CheckUserActivity(object? sender, ElapsedEventArgs e)
        {
            if (DateTime.UtcNow - _lastActivityTime > _inactivityThreshold)
                OnInactivityDetected?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Stop();
            _activityTimer.Dispose();
        }
    }
}
