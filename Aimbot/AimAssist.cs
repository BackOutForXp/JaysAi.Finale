//monarch v2.1 – Adaptive Aim Correction Logic
using System;
using OpenCvSharp;
using JaysAi.Finale.Input;
using JaysAi.Finale.AI;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class AimAssist
    {
        public static bool Enabled = true;
        public static float AimSmoothness = 0.35f;
        public static float MaxFov = 50f;

        private static int currentTargetId = -1;

        public static void Update(Vec2f crosshairPos, DetectedEntity[] entities)
        {
            if (!Enabled || entities == null || entities.Length == 0)
                return;

            float closestDist = float.MaxValue;
            DetectedEntity closestTarget = null;

            foreach (var entity in entities)
            {
                if (!entity.IsEnemy) continue;

                float dist = Vec2f.Distance(crosshairPos, entity.ScreenPosition);
                if (dist < MaxFov && dist < closestDist)
                {
                    closestDist = dist;
                    closestTarget = entity;
                }
            }

            if (closestTarget != null)
            {
                currentTargetId = closestTarget.ID;
                ApplyAimAssist(crosshairPos, closestTarget.ScreenPosition);
            }
            else
            {
                currentTargetId = -1;
            }
        }

        private static void ApplyAimAssist(Vec2f from, Vec2f to)
        {
            Vec2f delta = to - from;
            Vec2f smoothedDelta = delta * AimSmoothness;

            ControllerInputState.MoveStick(smoothedDelta.X, smoothedDelta.Y);
        }

        public static void SetSmoothness(float value)
        {
            AimSmoothness = Math.Clamp(value, 0.05f, 1.0f);
        }

        public static void SetFov(float fov)
        {
            MaxFov = Math.Clamp(fov, 10f, 180f);
        }
    }
}
