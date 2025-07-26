// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.AI
{
    public class AiMemory
    {
        private readonly Dictionary<int, TrackedTarget> _targetCache = new();
        private readonly Queue<FrameSnapshot> _frameHistory = new();
        private readonly object _lock = new();

        public int MaxSnapshots { get; set; } = 60;

        public void UpdateTargetMemory(List<TrackedTarget> currentTargets)
        {
            lock (_lock)
            {
                foreach (var target in currentTargets)
                {
                    if (_targetCache.ContainsKey(target.ID))
                        _targetCache[target.ID].UpdateFrom(target);
                    else
                        _targetCache[target.ID] = target.Clone();
                }
            }
        }

        public TrackedTarget? GetTargetById(int id)
        {
            lock (_lock)
            {
                _targetCache.TryGetValue(id, out var target);
                return target;
            }
        }

        public void PushFrameSnapshot(FrameSnapshot snapshot)
        {
            lock (_lock)
            {
                _frameHistory.Enqueue(snapshot);
                if (_frameHistory.Count > MaxSnapshots)
                    _frameHistory.Dequeue();
            }
        }

        public FrameSnapshot[] GetRecentSnapshots(int count)
        {
            lock (_lock)
            {
                return _frameHistory.Reverse().Take(count).ToArray();
            }
        }

        public void ClearMemory()
        {
            lock (_lock)
            {
                _targetCache.Clear();
                _frameHistory.Clear();
            }
        }

        public IReadOnlyDictionary<int, TrackedTarget> AllCachedTargets => _targetCache;
    }
}
