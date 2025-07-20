namespace JaysAi.Finale.SystemLogic
{
    public static class SystemConfig
    {
        // 🧠 Session-only flags (not saved permanently)
        public static bool SimulateFakeEnemies = false;
        public static bool DebugMode = true;
        public static bool DeveloperToolsEnabled = false;

        // 🎮 Control overrides
        public static bool IsControllerConnected = false;
        public static bool IsGameWindowFocused = true;

        // 🔐 Runtime license status
        public static string LastLicenseKey = string.Empty;

        // 🎯 Live aim config used during session
        public static float FovLimit = 150f;
        public static float SmoothingAmount = 5f;
        public static float RecoilCompensation = 1.2f;

        // 🔁 Internal use flags
        public static bool ConfigLoadedSuccessfully = false;
        public static bool IsEliteMode = false;

        // 🔍 Runtime path info
        public static string LoaderDirectory = AppDomain.CurrentDomain.BaseDirectory;
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Central hub for global session flags and overrides
// ✅ Tied into every module for conditional logic
// ✅ Avoids needing dozens of config lookups
// - [ ] Add game title detection (e.g., "BO2", "MW3")
// - [ ] Track runtime FPS / usage stats here for overlay
// ===================================================================
