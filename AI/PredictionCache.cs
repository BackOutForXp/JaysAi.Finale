//heavenly v3.0 – Prediction History Cache System
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class PredictionCache
    {
        private readonly Queue<Vector2> _positions = new();
        private readonly int _maxSize;

        public PredictionCache(int maxSize = 10)
        {
            _maxSize = Math.Max(1, maxSize);
        }

        /// <summary>
        /// Adds a new position sample to the cache.
        /// </summary>
        public void AddPosition(Vector2 position)
        {
            _positions.Enqueue(position);
            if (_positions.Count > _maxSize)
                _positions.Dequeue();
        }

        /// <summary>
        /// Returns smoothed average of position history.
        /// </summary>
        public Vector2 GetSmoothedPosition()
        {
            if (_positions.Count == 0)
                return Vector2.Zero;

            Vector2 sum = Vector2.Zero;
            foreach (var pos in _positions)
                sum += pos;

            return sum / _positions.Count;
        }

        /// <summary>
        /// Returns velocity estimated from the last two positions.
        /// </summary>
        public Vector2 EstimateVelocity(float deltaTimeSeconds)
        {
            if (_positions.Count < 2 || deltaTimeSeconds <= 0.0001f)
                return Vector2.Zero;

            Vector2[] array = _positions.ToArray();
            return (array[^1] - array[^2]) / deltaTimeSeconds;
        }

        /// <summary>
        /// Clears all historical data.
        /// </summary>
        public void Clear()
        {
            _positions.Clear();
        }

        public int Count => _positions.Count;
    }
}
