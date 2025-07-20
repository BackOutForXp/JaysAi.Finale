//monarch v2.1 – Prediction Engine
using System;
using System.Collections.Generic;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public class PredictionEngine
    {
        private readonly Dictionary<int, TargetHistory> historyMap = new();

        public class PredictionData
        {
            public float PredictedX;
            public float PredictedY;
            public float VelocityX;
            public float VelocityY;
            public float AccelerationX;
            public float AccelerationY;
        }

        private class TargetHistory
        {
            public List<float> X = new();
            public List<float> Y = new();
            public DateTime LastUpdate;
        }

        public PredictionData Predict(int targetId, float currentX, float currentY)
        {
            if (!historyMap.TryGetValue(targetId, out var history))
            {
                history = new TargetHistory();
                historyMap[targetId] = history;
            }

            var now = DateTime.Now;
            float deltaTime = (float)(now - history.LastUpdate).TotalSeconds;
            history.LastUpdate = now;

            history.X.Add(currentX);
            history.Y.Add(currentY);

            if (history.X.Count > 5)
            {
                history.X.RemoveAt(0);
                history.Y.RemoveAt(0);
            }

            float velocityX = 0, velocityY = 0, accelX = 0, accelY = 0;

            if (history.X.Count >= 2)
            {
                velocityX = (history.X[^1] - history.X[^2]) / deltaTime;
                velocityY = (history.Y[^1] - history.Y[^2]) / deltaTime;
            }

            if (history.X.Count >= 3)
            {
                float prevVelocityX = (history.X[^2] - history.X[^3]) / deltaTime;
                float prevVelocityY = (history.Y[^2] - history.Y[^3]) / deltaTime;

                accelX = (velocityX - prevVelocityX) / deltaTime;
                accelY = (velocityY - prevVelocityY) / deltaTime;
            }

            return new PredictionData
            {
                PredictedX = currentX + velocityX * deltaTime + 0.5f * accelX * deltaTime * deltaTime,
                PredictedY = currentY + velocityY * deltaTime + 0.5f * accelY * deltaTime * deltaTime,
                VelocityX = velocityX,
                VelocityY = velocityY,
                AccelerationX = accelX,
                AccelerationY = accelY
            };
        }

        public void ClearHistory(int targetId)
        {
            if (historyMap.ContainsKey(targetId))
                historyMap.Remove(targetId);
        }

        public void ClearAll()
        {
            historyMap.Clear();
        }
    }
}
