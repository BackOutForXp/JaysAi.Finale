// Monarch v1.0 – SettingsManager.cs
// ✅ Monarch Fix Checklist
// [x] Uses System.Text.Json (modern, native)
// [x] Loads and saves cleanly
// [x] Handles missing file cases
// [x] Ready for config injection

using System;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale
{
    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        public static AppSettings Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    return new AppSettings(); // Return defaults if no config

                string json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                return new AppSettings(); // Fallback to safe config
            }
        }

        public static void Save(AppSettings settings)
        {
            try
            {
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);
            }
            catch
            {
                // Optional: Log failure
            }
        }
    }

    public class AppSettings
    {
        public bool EnableESP { get; set; } = true;
        public bool EnableAimAssist { get; set; } = false;
        public string OverlayTheme { get; set; } = "DarkRed";
        // Add more config options here as needed
    }
}
