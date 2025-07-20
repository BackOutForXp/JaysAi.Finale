//monarch v2.1 – Snap tuning config for aimbot behavior
namespace JaysAi.Finale.Config
{
    public class SnapConfig
    {
        public bool SnapEnabled { get; set; } = true;
        public float SnapRange { get; set; } = 100f;
        public float SnapSpeed { get; set; } = 1.0f;
        public float MinLockDistance { get; set; } = 45f;
        public int SnapCooldownMs { get; set; } = 85;

        public bool HumanizerEnabled { get; set; } = true;
        public float RandomnessFactor { get; set; } = 0.04f;
        public float CurveModifier { get; set; } = 0.12f;

        public bool PrioritizeLowHealth { get; set; } = true;
        public bool RequireVisibility { get; set; } = true;

        public static SnapConfig LoadDefault()
        {
            return new SnapConfig();
        }
    }
}
