//heavenly v3.0
namespace JaysAi.Finale.Aimbot
{
    public static class SnapConfig
    {
        public static bool Enabled { get; set; } = true;

        // Max pixel distance from crosshair to target before snap triggers
        public static float SnapRadius { get; set; } = 80.0f;

        // Multiplier for how fast aim is pulled toward target (0.0f - 1.0f)
        public static float SnapStrength { get; set; } = 0.85f;

        // Delay between valid snaps (in ms)
        public static int SnapCooldownMs { get; set; } = 100;

        // Option to prevent snapping through walls (line of sight check)
        public static bool RequireVisibility { get; set; } = true;

        // Dynamic adjustment when tracking moving targets
        public static bool PredictMovement { get; set; } = true;
    }
}
