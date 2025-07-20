//monarch v2.0
namespace JaysAi.Finale.SystemLogic
{
    public static class FeatureToggleManager
    {
        public static bool EspEnabled { get; set; } = true;
        public static bool AimAssistEnabled { get; set; } = true;
        public static bool SnapAssistEnabled { get; set; } = true;
        public static bool OverlayVisible { get; set; } = true;
        public static bool AutoFireEnabled { get; set; } = false;

        public static void ToggleEsp() => EspEnabled = !EspEnabled;
        public static void ToggleAimAssist() => AimAssistEnabled = !AimAssistEnabled;
        public static void ToggleSnapAssist() => SnapAssistEnabled = !SnapAssistEnabled;
        public static void ToggleOverlay() => OverlayVisible = !OverlayVisible;
        public static void ToggleAutoFire() => AutoFireEnabled = !AutoFireEnabled;

        public static void ResetAll()
        {
            EspEnabled = true;
            AimAssistEnabled = true;
            SnapAssistEnabled = true;
            OverlayVisible = true;
            AutoFireEnabled = false;
        }
    }
}
