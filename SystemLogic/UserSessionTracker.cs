// neural v3.0
using System;
using System.Collections.Generic;
using System.Timers;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class UserSessionTracker : IDisposable
    {
        private static readonly Lazy<UserSessionTracker> _instance = new(() => new UserSessionTracker());
        public static UserSessionTracker Instance => _instance.Value;

        private readonly Dictionary<Guid, DateTime> _activityLog = new();
        private readonly Timer _inactivityTimer;
        private readonly object _syncRoot = new();

        public event EventHandler<Guid>? SessionTimedOut;

        private UserSessionTracker()
        {
            _inactivityTimer = new Timer(10000); // Check every 10 seconds
            _inactivityTimer.Elapsed += CheckInactivity;
            _inactivityTimer.AutoReset = true;
            _inactivityTimer.Start();
        }

        public void TrackActivity(Guid sessionId)
        {
            lock (_syncRoot)
            {
                _activityLog[sessionId] = DateTime.UtcNow;
            }
        }

        public TimeSpan GetInactivityDuration(Guid sessionId)
        {
            lock (_syncRoot)
            {
                return _activityLog.TryGetValue(sessionId, out var lastActive)
                    ? DateTime.UtcNow - lastActive
                    : TimeSpan.MaxValue;
            }
        }

        private void CheckInactivity(object? sender, ElapsedEventArgs e)
        {
            List<Guid> expiredSessions = new();

            lock (_syncRoot)
            {
                foreach (var kvp in _activityLog)
                {
                    if ((DateTime.UtcNow - kvp.Value).TotalMinutes > 30)
                    {
                        expiredSessions.Add(kvp.Key);
                    }
                }

                foreach (var sessionId in expiredSessions)
                {
                    _activityLog.Remove(sessionId);
                }
            }

            foreach (var expiredId in expiredSessions)
            {
                SessionTimedOut?.Invoke(this, expiredId);
            }
        }

        public void RemoveSession(Guid sessionId)
        {
            lock (_syncRoot)
            {
                _activityLog.Remove(sessionId);
            }
        }

        public void Dispose()
        {
            _inactivityTimer.Dispose();
        }
    }
}
