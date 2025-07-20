//monarch v2.1 – Central toggle manager for features
namespace JaysAi.Finale.SystemLogic
{
    public static class FeatureToggle
    {
        public static bool EspEnabled { get; set; } = false;
        public static bool AimAssistEnabled { get; set; } = false;
        public static bool SnapEnabled { get; set; } = false;
        public static bool VisualsOverlayEnabled { get; set; } = false;

        public static bool RecoilCompensationEnabled { get; set; } = false;
        public static bool TriggerBotEnabled { get; set; } = false;

        public static void DisableAll()
        {
            EspEnabled = false;
            AimAssistEnabled = false;
            SnapEnabled = false;
            VisualsOverlayEnabled = false;
            RecoilCompensationEnabled = false;
            TriggerBotEnabled = false;
        }

        public static void EnableAll()
        {
            EspEnabled = true;
            AimAssistEnabled = true;
            SnapEnabled = true;
            VisualsOverlayEnabled = true;
            RecoilCompensationEnabled = true;
            TriggerBotEnabled = true;
        }
    }
}
