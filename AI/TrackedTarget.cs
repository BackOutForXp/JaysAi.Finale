// neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public class TrackedTarget
    {
        public Enemy Enemy { get; private set; }
        public Queue<Vector3> PositionHistory { get; private set; }
        public DateTime LastSeen { get; private set; }
        public Vector3 SmoothedPosition { get; private set; }
        public bool IsVisible { get; private set; }

        private const int HistoryLimit = 10;

        public TrackedTarget(Enemy enemy)
        {
            Enemy = enemy;
            PositionHistory = new Queue<Vector3>();
            Update(enemy.Position, enemy.IsVisible);
        }

        public void Update(Vector3 newPosition, bool isVisible)
        {
            IsVisible = isVisible;
            LastSeen = DateTime.UtcNow;

            if (PositionHistory.Count >= HistoryLimit)
                PositionHistory.Dequeue();

            PositionHistory.Enqueue(newPosition);
            SmoothedPosition = CalculateSmoothedPosition();
        }

        private Vector3 CalculateSmoothedPosition()
        {
            Vector3 sum = new(0, 0, 0);
            foreach (var pos in PositionHistory)
                sum += pos;

            return sum / PositionHistory.Count;
        }

        public bool IsLost(TimeSpan timeout)
        {
            return DateTime.UtcNow - LastSeen > timeout;
        }

        public Vector3 PredictNextPosition()
        {
            if (PositionHistory.Count < 2)
                return SmoothedPosition;

            var history = PositionHistory.ToArray();
            var velocity = history[^1] - history[^2];
            return SmoothedPosition + velocity;
        }
    }
}
