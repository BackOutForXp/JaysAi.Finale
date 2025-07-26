// neural v3.0
namespace JaysAi.Finale.Aimbot
{
    public class TriggerSettings
    {
        public bool EnableAutoTrigger { get; set; } = false;
        public bool RequireScope { get; set; } = true;
        public bool RequireOnTarget { get; set; } = true;

        public int MinimumShots { get; set; } = 1;
        public int MaxBurstLength { get; set; } = 3;
        public int DelayBetweenShots { get; set; } = 50; // milliseconds

        public float RequiredConfidence { get; set; } = 0.85f; // prediction confidence
        public float TriggerDistanceThreshold { get; set; } = 150.0f; // max range
        public float MaxFovAngle { get; set; } = 12.5f; // FOV range for activation

        public bool IsTriggerReady(float distance, float fovOffset, float confidence, bool isScoped)
        {
            if (!EnableAutoTrigger) return false;
            if (RequireScope && !isScoped) return false;
            if (RequireOnTarget && confidence < RequiredConfidence) return false;
            if (distance > TriggerDistanceThreshold) return false;
            if (fovOffset > MaxFovAngle) return false;

            return true;
        }
    }
}
