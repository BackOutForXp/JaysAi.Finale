// Monarch v1.0 – SnapZoneConfig.cs
// ✅ Monarch Fix Checklist
// [x] Stores FOV radius for lock-on
// [x] Configurable toggle for dynamic or static snap zone
// [x] Supports HUD visualization if needed later

namespace JaysAi.Finale.Modules
{
    public static class SnapZoneConfig
    {
        public static float SnapRadius { get; set; } = 50f;

        public static bool DynamicZoneEnabled { get; set; } = false;
        public static float MinSnapRadius { get; set; } = 35f;
        public static float MaxSnapRadius { get; set; } = 80f;

        public static void AdjustSnapRadius(float playerSpeed)
        {
            if (!DynamicZoneEnabled)
                return;

            // Dynamic scaling: wider radius at faster speeds
            SnapRadius = MinSnapRadius + (playerSpeed * 0.5f);
            if (SnapRadius > MaxSnapRadius)
                SnapRadius = MaxSnapRadius;
        }
    }
}
