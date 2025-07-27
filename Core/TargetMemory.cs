using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class TargetMemory
    {
        public int EnemyId { get; }
        public string EnemyName { get; }

        public List<float> SnapScores { get; } = new();
        public List<Vector3> PositionHistory { get; } = new();
        public List<bool> VisibilityHistory { get; } = new();
        public List<bool> SnapSuccessHistory { get; } = new();

        public DateTime LastSeen { get; private set; }
        public int SnapAttempts { get; private set; }
        public int SnapSuccesses { get; private set; }

        public TargetMemory(int id, string name)
        {
            EnemyId = id;
            EnemyName = name;
            LastSeen = DateTime.UtcNow;
        }

        public void RecordObservation(Vector3 position, float score, bool isVisible)
        {
            LastSeen = DateTime.UtcNow;

            PositionHistory.Add(position);
            SnapScores.Add(score);
            VisibilityHistory.Add(isVisible);

            // Keep only latest 50 frames
            if (PositionHistory.Count > 50) PositionHistory.RemoveAt(0);
            if (SnapScores.Count > 50) SnapScores.RemoveAt(0);
            if (VisibilityHistory.Count > 50) VisibilityHistory.RemoveAt(0);
        }

        public void RecordSnapAttempt(bool success)
        {
            SnapAttempts++;
            if (success) SnapSuccesses++;
            SnapSuccessHistory.Add(success);
            if (SnapSuccessHistory.Count > 50) SnapSuccessHistory.RemoveAt(0);
        }

        public float GetSnapSuccessRate()
        {
            return SnapAttempts == 0 ? 0f : (float)SnapSuccesses / SnapAttempts;
        }

        public bool IsFrequentlyVisible(float threshold = 0.5f)
        {
            if (VisibilityHistory.Count == 0) return false;
            int visibleCount = VisibilityHistory.FindAll(v => v).Count;
            return (float)visibleCount / VisibilityHistory.Count >= threshold;
        }

        public bool IsHighConfidence(float snapThreshold = 0.6f)
        {
            return GetSnapSuccessRate() >= snapThreshold && IsFrequentlyVisible();
        }
    }
}
