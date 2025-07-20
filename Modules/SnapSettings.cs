// Monarch v1.0 – SnapSettings.cs
// ✅ Monarch Fix Checklist
// [x] Exposed global tuning fields (smoothing, strength, FOV, deadzone)
// [x] Real-time adjustable (for UI slider binding later)
// [x] Centralized config — used by Aimbot, ESP, StickInput

namespace JaysAi.Finale.Modules
{
    public static class SnapSettings
    {
        // Percentage values (0.0f to 1.0f)
        public static float SnapSmoothing { get; set; } = 0.12f;
        public static float SnapStrength { get; set; } = 1.0f;
        public static float SnapDeadzone { get; set; } = 0.06f;

        // Field of View for target detection (scaled against screen center)
        public static float SnapFOV { get; set; } = 0.42f;

        // Toggle for enabling/disabling snap logic
        public static bool SnapEnabled { get; set; } = true;
    }
}
