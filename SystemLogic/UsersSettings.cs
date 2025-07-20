//monarch v2.0
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.SystemLogic
{
    public class UserSettings
    {
        private const string SettingsPath = "user_config.json";

        public bool EspEnabled { get; set; } = true;
        public bool AimAssistEnabled { get; set; } = true;
        public bool SnapAssistEnabled { get; set; } = true;
        public float AimFov { get; set; } = 75f;
        public float PredictionSmoothness { get; set; } = 0.85f;

        public static UserSettings Load()
        {
            if (!File.Exists(SettingsPath))
                return new UserSettings();

            try
            {
                string json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            }
            catch
            {
                return new UserSettings(); // Fallback on failure
            }
        }

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsPath, json);
        }
    }
}
