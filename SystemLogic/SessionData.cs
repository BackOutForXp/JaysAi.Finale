// neural v3.0
using System;

namespace JaysAi.Finale.SystemLogic
{
    public class SessionData
    {
        public Guid SessionId { get; set; }
        public string Username { get; set; } = "Unknown";
        public DateTime StartTime { get; private set; }
        public DateTime LastInteraction { get; private set; }
        public string? IPAddress { get; set; } = null;
        public bool IsVerified { get; set; } = false;
        public bool IsDeveloperMode { get; set; } = false;
        public bool IsDiagnosticEnabled { get; set; } = false;

        public TimeSpan SessionDuration => DateTime.UtcNow - StartTime;
        public TimeSpan InactivityDuration => DateTime.UtcNow - LastInteraction;

        public SessionData(string username)
        {
            SessionId = Guid.NewGuid();
            Username = username;
            StartTime = DateTime.UtcNow;
            LastInteraction = StartTime;
        }

        public void RefreshActivity()
        {
            LastInteraction = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"[Session: {Username}] Uptime: {SessionDuration.TotalMinutes:F1} mins";
        }
    }
}
