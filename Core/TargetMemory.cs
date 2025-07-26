//neural v3.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Structures;

namespace JaysAi.Finale.Core
{
    public class TargetMemory
    {
        private readonly ConcurrentDictionary<int, TrackedTarget> _trackedTargets = new();
        private readonly TimeSpan _decayTime;
        private readonly object _lock = new();

        public TargetMemory(TimeSpan? decayTime = null)
        {
            _decayTime = decayTime ?? TimeSpan.FromSeconds(1.5);
        }

        public void Update(int id, DetectedObject obj)
        {
            if (obj == null) return;

            lock (_lock)
            {
                if (!_trackedTargets.ContainsKey(id))
                    _trackedTargets[id] = new TrackedTarget(id);

                _trackedTargets[id].Update(obj);
            }
        }

        public TrackedTarget? GetStrongestTarget(Func<TrackedTarget, float> scoreFunc)
        {
            lock (_lock)
            {
                CleanupExpired();

                return _trackedTargets.Values
                    .Where(t => !t.IsExpired)
                    .OrderByDescending(scoreFunc)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<TrackedTarget> GetAllTargets()
        {
            lock (_lock)
            {
                CleanupExpired();
                return _trackedTargets.Values.ToList();
            }
        }

        public void Reset()
        {
            lock (_lock)
                _trackedTargets.Clear();
        }

        private void CleanupExpired()
        {
            var now = DateTime.UtcNow;
            var expiredKeys = _trackedTargets
                .Where(pair => (now - pair.Value.LastSeen) > _decayTime)
                .Select(pair => pair.Key)
                .ToList();

            foreach (var key in expiredKeys)
                _trackedTargets.TryRemove(key, out _);
        }
    }

    public class TrackedTarget
    {
        public int Id { get; }
        public DetectedObject LastKnownObject { get; private set; }
        public DateTime LastSeen { get; private set; }

        public bool IsExpired => (DateTime.UtcNow - LastSeen).TotalSeconds > 1.5;

        public TrackedTarget(int id)
        {
            Id = id;
            LastSeen = DateTime.UtcNow;
            LastKnownObject = new DetectedObject();
        }

        public void Update(DetectedObject obj)
        {
            LastKnownObject = obj;
            LastSeen = DateTime.UtcNow;
        }
    }
}
