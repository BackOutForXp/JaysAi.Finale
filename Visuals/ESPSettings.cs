// Neural v3.0 — EspSettings.cs
namespace JaysAi.Finale.Overlay
{
    public static class EspSettings
    {
        // === General ESP Toggles ===
        public static bool EnableESP { get; set; } = true;
        public static bool ShowBoxes { get; set; } = true;
        public static bool ShowHealthBars { get; set; } = true;
        public static bool ShowNames { get; set; } = true;
        public static bool ShowSkeletons { get; set; } = false;

        // === Visual Behavior ===
        public static bool UseBoxFill { get; set; } = false;
        public static bool UseRoundedCorners { get; set; } = true;
        public static bool UseAntiAliasing { get; set; } = true;

        // === ESP Object Filters ===
        public static bool OnlyEnemies { get; set; } = true;
        public static bool ShowTeammates { get; set; } = false;
        public static bool ShowDownedPlayers { get; set; } = true;

        // === Advanced AI Filtering ===
        public static float MinConfidenceThreshold { get; set; } = 0.45f;
        public static bool EnableSmartOcclusionCheck { get; set; } = false;

        // === Debugging / Experimental ===
        public static bool ShowDebugInfo { get; set; } = false;
        public static bool EnableExperimentalRenderPass { get; set; } = false;

        // === Reserved Fields ===
        public static bool Reserved1 { get; set; } = false;
        public static bool Reserved2 { get; set; } = false;
    }
}
