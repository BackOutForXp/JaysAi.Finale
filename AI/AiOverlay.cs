// Neural v3.1 — AiOverlay.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Visuals;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class AiOverlay
    {
        private readonly OverlaySignal _overlaySignal;
        private readonly List<Vector2> _drawnPaths = new();

        public bool ShowPredictionLines { get; set; } = true;
        public bool ShowTargetInfo { get; set; } = true;
        public bool ShowVelocityVector { get; set; } = false;

        public AiOverlay()
        {
            _overlaySignal = new OverlaySignal(); // Singleton or injected later
        }

        public void BindToAI(AiManager ai)
        {
            // Optional: Link back if needed
        }

        public void UpdateOverlayData(List<TrackedTarget> targets)
        {
            foreach (var target in targets)
            {
                DrawTrackedTargetInfo(target);

                if (target.PredictedScreenPosition.HasValue)
                    DrawPredictionPath(target, target.PredictedScreenPosition.Value);

                if (target.Velocity.Length() > 0.01f)
                    DrawVelocity(target, target.Velocity);
            }
        }

        public void DrawTrackedTargetInfo(TrackedTarget target)
        {
            if (!ShowTargetInfo) return;

            string label = $"ID: {target.ID} | HP: {target.Health} | Dist: {Math.Round(target.Distance)}m";
            _overlaySignal.DrawText(target.ScreenPosition, label, OverlayColor.White, size: 12);
        }

        public void DrawPredictionPath(TrackedTarget target, Vector2 predictedPosition)
        {
            if (!ShowPredictionLines) return;

            _overlaySignal.DrawLine(target.ScreenPosition, predictedPosition, OverlayColor.Green);
            _drawnPaths.Add(predictedPosition);
        }

        public void DrawVelocity(TrackedTarget target, Vector2 velocity)
        {
            if (!ShowVelocityVector) return;

            Vector2 endPoint = target.ScreenPosition + velocity * 0.5f;
            _overlaySignal.DrawLine(target.ScreenPosition, endPoint, OverlayColor.Yellow);
        }

        public void Unbind()
        {
            _drawnPaths.Clear();
        }
    }
}
