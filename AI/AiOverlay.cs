// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
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

        public AiOverlay(OverlaySignal overlaySignal)
        {
            _overlaySignal = overlaySignal;
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

            Vector2 endPoint = target.ScreenPosition + velocity * 10f;
            _overlaySignal.DrawArrow(target.ScreenPosition, endPoint, OverlayColor.Red);
        }

        public void Clear()
        {
            _drawnPaths.Clear();
        }

        public IReadOnlyList<Vector2> GetDrawnPaths() => _drawnPaths.AsReadOnly();
    }
}
