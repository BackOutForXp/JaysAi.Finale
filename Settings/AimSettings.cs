// Neural v3.1
namespace JaysAi.Finale.Settings
{
    public static class AimSettings
    {
        public static bool Enabled
        {
            get => UserSettings.Instance.Get("AimEnabled", true);
            set => UserSettings.Instance.Set("AimEnabled", value);
        }

        public static float MaxFov
        {
            get => UserSettings.Instance.Get("AimMaxFov", 90f);
            set => UserSettings.Instance.Set("AimMaxFov", value);
        }

        public static float Smoothing
        {
            get => UserSettings.Instance.Get("AimSmoothing", 0.4f);
            set => UserSettings.Instance.Set("AimSmoothing", value);
        }

        public static string AimBone
        {
            get => UserSettings.Instance.Get("AimBone", "Head");
            set => UserSettings.Instance.Set("AimBone", value);
        }

        public static bool RequireVisibility
        {
            get => UserSettings.Instance.Get("AimRequireVisible", true);
            set => UserSettings.Instance.Set("AimRequireVisible", value);
        }

        public static bool OnlyInFov
        {
            get => UserSettings.Instance.Get("AimOnlyInFov", true);
            set => UserSettings.Instance.Set("AimOnlyInFov", value);
        }
    }
}
