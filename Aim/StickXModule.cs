//monarch v2.1
using System;
using JaysAi.SystemLogic;

namespace JaysAi.Finale.Aim
{
    public class StickXModule
    {
        private float targetX;
        private float targetY;
        private float sensitivity = 1.0f;
        private float smoothness = 0.1f;

        public void SetTarget(float x, float y)
        {
            targetX = x;
            targetY = y;
        }

        public (float deltaX, float deltaY) ComputeAdjustment(float currentX, float currentY)
        {
            float dx = (targetX - currentX) * sensitivity;
            float dy = (targetY - currentY) * sensitivity;

            dx *= smoothness;
            dy *= smoothness;

            return (dx, dy);
        }

        public void ApplyInput(ref float outX, ref float outY, float currentX, float currentY)
        {
            var (dx, dy) = ComputeAdjustment(currentX, currentY);
            outX += dx;
            outY += dy;
        }

        public void UpdateSettings(float newSensitivity, float newSmoothness)
        {
            sensitivity = newSensitivity;
            smoothness = newSmoothness;
        }

        public void Reset()
        {
            targetX = 0;
            targetY = 0;
        }
    }
}
