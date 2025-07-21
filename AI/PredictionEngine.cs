//monarch v2.1 – Predictive Target Movement Engine
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class PredictionEngine
    {
        private static readonly Dictionary<int, TargetHistory> _historyMap = new();

        public static void Initialize()
        {
            _historyMap.Clear();
        }

        public static void UpdateHistory(DetectedObject obj)
        {
            if (!_historyMap.TryGetValue(obj.Id, out var history))
            {
                history = new TargetHistory();
                _historyMap[obj.Id] = history;
            }

            history.Update(obj.X, obj.Y);
        }

        public static (float predictedX, float predictedY) PredictPosition(DetectedObject obj, float predictionFactor = 1.0f)
        {
            if (_historyMap.TryGetValue(obj.Id, out var history))
            {
                float dx = history.VelocityX * predictionFactor;
                float dy = history.VelocityY * predictionFactor;

                return (obj.X + dx, obj.Y + dy);
            }

            return (obj.X, obj.Y); // fallback: no prediction available
        }

        private class TargetHistory
        {
            private float _lastX;
            private float _lastY;
            public float VelocityX { get; private set; }
            public float VelocityY { get; private set; }

            public void Update(float currentX, float currentY)
            {
                VelocityX = currentX - _lastX;
                VelocityY = currentY - _lastY;

                _lastX = currentX;
                _lastY = currentY;
            }
        }
    }
}
