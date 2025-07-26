// Neural v3.0 — SnapshotPredictor.cs
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class SnapshotPredictor
    {
        private readonly Queue<FrameSnapshot> _history = new();
        private readonly int _maxHistory;
        private readonly TimeSpan _maxAge;

        public SnapshotPredictor(int maxHistoryFrames = 10, double maxAgeMilliseconds = 300)
        {
            _maxHistory = maxHistoryFrames;
            _maxAge = TimeSpan.FromMilliseconds(maxAgeMilliseconds);
        }

        /// <summary>
        /// Adds a new snapshot to the internal history for prediction.
        /// </summary>
        public void AddSnapshot(FrameSnapshot snapshot)
        {
            _history.Enqueue(snapshot);

            // Remove old snapshots by age or count
            while (_history.Count > _maxHistory || (DateTime.UtcNow - _history.Peek().Timestamp) > _maxAge)
            {
                _history.Dequeue();
            }
        }

        /// <summary>
        /// Attempts to predict future position of a target after a given delay.
        /// </summary>
        public Vector3 PredictFuturePosition(TimeSpan futureDelta)
        {
            if (_history.Count < 2) return _history.Count == 1 ? _history.Peek().Position : Vector3.Zero;

            // Use linear regression on time-position history
            var snapshots = _history.ToArray();
            var totalDeltaTime = (snapshots[^1].Timestamp - snapshots[0].Timestamp).TotalSeconds;

            if (totalDeltaTime <= 0) return snapshots[^1].Position;

            var displacement = snapshots[^1].Position - snapshots[0].Position;
            var velocity = displacement / (float)totalDeltaTime;

            return snapshots[^1].Position + velocity * (float)futureDelta.TotalSeconds;
        }

        /// <summary>
        /// Clears all internal frame history.
        /// </summary>
        public void Clear()
        {
            _history.Clear();
        }

        /// <summary>
        /// Gets the most recent snapshot or null if none exist.
        /// </summary>
        public FrameSnapshot? GetLatestSnapshot()
        {
            return _history.Count > 0 ? _history.ToArray()[^1] : null;
        }
    }
}
