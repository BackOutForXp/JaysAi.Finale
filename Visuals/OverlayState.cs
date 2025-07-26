// Neural v3.0 — OverlayState.cs
namespace JaysAi.Finale.Overlay
{
    public static class OverlayState
    {
        // === Master State Switches ===
        public static bool OverlaySystemEnabled { get; set; } = true;
        public static bool IsInitialized { get; set; } = false;

        // === Individual Module States ===
        public static bool EspEnabled { get; set; } = true;
        public static bool CrosshairEnabled { get; set; } = true;
        public static bool FovRingEnabled { get; set; } = true;
        public static bool DebugConsoleVisible { get; set; } = false;

        // === Render Performance Flags ===
        public static bool UseAntiAliasing { get; set; } = true;
        public static bool UseSafeMode { get; set; } = false;

        // === Diagnostic + Update Info ===
        public static int FrameCount { get; set; } = 0;
        public static double LastRenderMs { get; set; } = 0;
        public static string LastActiveOverlay { get; set; } = "";

        // === Runtime Flag Controls (for hotkeys, GUI sync, etc.) ===
        public static bool ShowOverlayBorders { get; set; } = false;
        public static bool ShowOverlayLabels { get; set; } = false;

        // === Reserved Future Flags ===
        public static bool Reserved1 { get; set; } = false;
        public static bool Reserved2 { get; set; } = false;
    }
}
