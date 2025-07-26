// neural v3.0
namespace JaysAi.Finale.Aimbot
{
    public class SnapConfig
    {
        public bool IsEnabled { get; set; } = true;
        public float SnapFOV { get; set; } = 90.0f; // Degrees
        public float Sensitivity { get; set; } = 1.0f; // Pixel ratio
        public bool UsePrediction { get; set; } = true;
        public bool RequireADS { get; set; } = true;
        public bool HeadOnly { get; set; } = false;
        public bool PrioritizeVisibleTargets { get; set; } = true;
        public bool StickyAim { get; set; } = false;
        public float SnapCooldown { get; set; } = 0.15f; // Seconds
        public float SmoothingFactor { get; set; } = 0.0f; // 0 = instant snap
        public int SnapZoneLayer { get; set; } = 0; // For snap zone prioritization
    }
}
