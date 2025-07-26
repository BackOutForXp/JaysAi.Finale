//neural v3.0
namespace JaysAi.Finale.Settings
{
    public static class BuildInfo
    {
        public const string Name = "JaysAi.Finale";
        public const string Version = "v3.0-Neural";
        public const string Codename = "Neural Monarch";
        public const string Channel = "Stable";
        public const string BuildDate = "2025-07-24";
        public const string LicenseMode = "Full Access";
        public const bool IsInternalBuild = false;

        public static string FullSignature =>
            $"{Name} {Version} [{Codename}] - {Channel} ({BuildDate})";
    }
}
