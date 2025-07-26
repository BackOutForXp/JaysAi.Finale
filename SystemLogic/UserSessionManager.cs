// neural v3.0
using System;
using System.Collections.Concurrent;
using JaysAi.Finale.Security;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class UserSessionManager
    {
        private static readonly Lazy<UserSessionManager> _instance = new(() => new UserSessionManager());
        public static UserSessionManager Instance => _instance.Value;

        private readonly ConcurrentDictionary<Guid, UserSession> _activeSessions;

        private UserSessionManager()
        {
            _activeSessions = new ConcurrentDictionary<Guid, UserSession>();
        }

        public Guid StartSession(string username, string licenseKey)
        {
            var session = new UserSession
            {
                SessionId = Guid.NewGuid(),
                Username = username,
                LicenseKey = licenseKey,
                LoginTime = DateTime.UtcNow,
                IsActive = true
            };

            _activeSessions[session.SessionId] = session;
            return session.SessionId;
        }

        public void EndSession(Guid sessionId)
        {
            if (_activeSessions.TryRemove(sessionId, out var session))
            {
                session.IsActive = false;
                session.LogoutTime = DateTime.UtcNow;
            }
        }

        public UserSession? GetSession(Guid sessionId)
        {
            _activeSessions.TryGetValue(sessionId, out var session);
            return session;
        }

        public bool IsSessionActive(Guid sessionId)
        {
            return _activeSessions.TryGetValue(sessionId, out var session) && session.IsActive;
        }

        public void ClearInactiveSessions()
        {
            foreach (var kvp in _activeSessions)
            {
                if (!kvp.Value.IsActive)
                    _activeSessions.TryRemove(kvp.Key, out _);
            }
        }
    }

    public class UserSession
    {
        public Guid SessionId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string LicenseKey { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public bool IsActive { get; set; }
    }
}
