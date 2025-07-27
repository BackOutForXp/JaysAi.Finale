// Neural v3.1 — AppSettingsExtensions.cs
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Utility
{
    public static class AppSettingsExtensions
    {
        public static void ApplyFrom(this AppSettings target, AppSettings source)
        {
            if (target == null || source == null) return;

            target.EnableESP = source.EnableESP;
            target.EnableAimAssist = source.EnableAimAssist;
            target.CrosshairStyle = source.CrosshairStyle;
            target.AimSmoothness = source.AimSmoothness;
            target.AimFov = source.AimFov;
            target.TriggerBot = source.TriggerBot;
            target.RecoilControl = source.RecoilControl;
            target.SelectedProfile = source.SelectedProfile;
            target.Keybinds = source.Keybinds;
        }
    }
}
