// neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;

namespace JaysAi.Finale.AI
{
    public class EnemyScanner
    {
        private readonly float _minConfidence;
        private readonly Func<DetectedObject, bool>? _customFilter;

        public EnemyScanner(float minConfidenceThreshold = 0.6f, Func<DetectedObject, bool>? customFilter = null)
        {
            _minConfidence = minConfidenceThreshold;
            _customFilter = customFilter;
        }

        public List<DetectedObject> Scan(IEnumerable<DetectedObject> detectedObjects)
        {
            if (detectedObjects == null)
                return new();

            return detectedObjects
                .Where(obj =>
                    obj != null &&
                    obj.IsVisible &&
                    obj.IsEnemy &&
                    obj.Confidence >= _minConfidence &&
                    obj.IsValid &&
                    (_customFilter == null || _customFilter(obj)))
                .OrderByDescending(obj => obj.Confidence)
                .ThenBy(obj => obj.Area)
                .ToList();
        }

        public DetectedObject? GetTopEnemy(IEnumerable<DetectedObject> detectedObjects)
        {
            return Scan(detectedObjects).FirstOrDefault();
        }
    }
}
