//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public class SnapTarget
    {
        public TrackedTarget Target { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public float DistanceToCrosshair { get; set; }
        public float Score { get; set; }
        public bool IsVisible { get; set; }

        public SnapTarget(TrackedTarget target, Vector2 screenPos, float distance, float score, bool visible)
        {
            Target = target;
            ScreenPosition = screenPos;
            DistanceToCrosshair = distance;
            Score = score;
            IsVisible = visible;
        }

        public bool IsValid(float maxDistance)
        {
            return IsVisible && DistanceToCrosshair <= maxDistance;
        }
    }
}
