//monarch v2.1 – Internal Build Metadata
namespace JaysAi.Finale.Utility
{
    public static class BuildInfo
    {
        public static readonly string Version = "2.1.0";
        public static readonly string Codename = "Monarch Override";
        public static readonly string LoaderName = "JaysAi Monarch Loader";
        public static readonly string BuildDate = "2025-07-20";

        // Display toggles
        public static bool DebugOverlayEnabled = false;
        public static bool VersionWatermarkEnabled = true;

        // Internal feature toggles (safe mode, dev overrides)
        public static bool IsDeveloperBuild = true;
        public static bool SafeModeEnabled = false;

        // System-level flags (used for stealth checks, startup behavior)
        public static bool IsStealthMode = false;
        public static bool IsFirstLaunch = true;
    }
}
