//monarch v2.1 – Shared global flags and state memory

namespace JaysAi.Finale.SystemLogic
{
    public static class AppState
    {
        public static bool IsESPEnabled { get; set; } = false;
        public static bool IsAimbotEnabled { get; set; } = false;
        public static bool IsSnapEnabled { get; set; } = false;

        public static string LastStatusMessage { get; set; } = "";
    }
}
