//neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.Structures;
using JaysAi.Finale.Helpers;

namespace JaysAi.Finale.Modules
{
    public sealed class TargetLockTracker
    {
        private readonly Dictionary<int, LockSnapshot> _lockHistory = new();
        private readonly object _sync = new();

        public void UpdateLock(int targetId, Vector2 position)
        {
            lock (_sync)
            {
                if (!_lockHistory.TryGetValue(targetId, out var snapshot))
                {
                    snapshot = new LockSnapshot { TargetId = targetId };
                    _lockHistory[targetId] = snapshot;
                }

                snapshot.LastSeen = DateTime.UtcNow;
                snapshot.Position = position;
            }
        }

        public Vector2? GetTargetPosition(int targetId)
        {
            lock (_sync)
            {
                if (_lockHistory.TryGetValue(targetId, out var snapshot))
                {
                    return snapshot.Position;
                }
                return null;
            }
        }

        public bool IsTargetLost(int targetId, TimeSpan maxInactive)
        {
            lock (_sync)
            {
                return _lockHistory.TryGetValue(targetId, out var snapshot) &&
                       (DateTime.UtcNow - snapshot.LastSeen) > maxInactive;
            }
        }

        public void PruneInactiveTargets(TimeSpan threshold)
        {
            lock (_sync)
            {
                var now = DateTime.UtcNow;
                var expired = new List<int>();

                foreach (var kvp in _lockHistory)
                {
                    if ((now - kvp.Value.LastSeen) > threshold)
                        expired.Add(kvp.Key);
                }

                foreach (var id in expired)
                    _lockHistory.Remove(id);
            }
        }
    }

    internal class LockSnapshot
    {
        public int TargetId { get; set; }
        public DateTime LastSeen { get; set; }
        public Vector2 Position { get; set; }
    }
}
