// Neural v3.0 — PredictionFeed.cs
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Modules
{
    public class PredictionFeed
    {
        private readonly ConcurrentDictionary<int, PredictionSnapshot> _predictions;

        public event Action<PredictionSnapshot>? OnNewPrediction;

        public PredictionFeed()
        {
            _predictions = new ConcurrentDictionary<int, PredictionSnapshot>();
        }

        /// <summary>
        /// Updates or inserts a prediction entry for a target.
        /// </summary>
        public void PushPrediction(int targetId, PredictionSnapshot snapshot)
        {
            _predictions[targetId] = snapshot;
            OnNewPrediction?.Invoke(snapshot);
        }

        /// <summary>
        /// Gets the latest prediction for a target.
        /// </summary>
        public bool TryGetPrediction(int targetId, out PredictionSnapshot snapshot)
        {
            return _predictions.TryGetValue(targetId, out snapshot);
        }

        /// <summary>
        /// Gets all active prediction entries.
        /// </summary>
        public IEnumerable<PredictionSnapshot> GetAll() => _predictions.Values;

        /// <summary>
        /// Clears all prediction data.
        /// </summary>
        public void Clear() => _predictions.Clear();

        /// <summary>
        /// Removes a specific target's prediction.
        /// </summary>
        public void Remove(int targetId)
        {
            _predictions.TryRemove(targetId, out _);
        }
    }
}
