// Neural v3.1 — TrackedTarget.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public class TrackedTarget
    {
        public Enemy Enemy { get; private set; }
        public Queue<Vector3> PositionHistory { get; private set; } = new();
        public DateTime LastSeen { get; private set; }

        public Vector3 SmoothedPosition { get; private set; }
        public Vector3 Position => Enemy.Position;
        public Vector3 Velocity => Enemy.Velocity;
        public float Distance => Enemy.Distance;
        public int ID => Enemy.ID;
        public int Health => Enemy.Health;
        public bool IsVisible { get; private set; }

        public Vector2 ScreenPosition => Enemy.ScreenPosition;
        public Vector2? PredictedScreenPosition { get; private set; }

        private const int HistoryLimit = 10;

        public TrackedTarget(Enemy enemy)
        {
            Enemy = enemy;
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
            Vector3 sum = Vector3.Zero;
            foreach (var pos in PositionHistory)
                sum += pos;

            return PositionHistory.Count > 0 ? sum / PositionHistory.Count : Vector3.Zero;
        }

        public bool IsLost(TimeSpan timeout)
        {
            return DateTime.UtcNow - LastSeen > timeout;
        }

        public Vector3 PredictNextPosition(float delay = 0.1f)
        {
            return Position + Velocity * delay;
        }

        public void SetPredictedPosition(Vector3 worldPosition)
        {
            // In production: convert to screen position via W2S
            PredictedScreenPosition = WorldToScreenConverter.Project(worldPosition);
        }
    }
}
