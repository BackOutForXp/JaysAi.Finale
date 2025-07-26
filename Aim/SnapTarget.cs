// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public class SnapTarget
    {
        public Vector3 WorldPosition { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public float DistanceToCrosshair { get; set; }
        public float ThreatLevel { get; set; }
        public float VisibilityScore { get; set; }
        public int TargetId { get; set; }
        public bool IsVisible { get; set; }

        public float GetPriorityScore()
        {
            // Scoring formula: higher visibility, closer distance, higher threat = better target
            float distanceWeight = 1.0f / (DistanceToCrosshair + 0.01f); // prevent div by zero
            return (VisibilityScore * 0.6f + ThreatLevel * 0.3f) * distanceWeight;
        }

        public bool IsValid()
        {
            return IsVisible && DistanceToCrosshair > 0 && ThreatLevel > 0;
        }
    }
}
