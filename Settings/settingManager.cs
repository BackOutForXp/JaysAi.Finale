//neural v3.0
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JaysAi.Finale.Settings
{
    public sealed class SettingsManager
    {
        private static readonly Lazy<SettingsManager> _instance = new(() => new SettingsManager());
        public static SettingsManager Instance => _instance.Value;

        private readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "global-settings.json");
        private AppSettings _settings;

        public AppSettings Settings => _settings;

        private SettingsManager()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_settingsPath))
                {
                    var json = File.ReadAllText(_settingsPath);
                    _settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
                else
                {
                    _settings = new AppSettings();
                    Save();
                }
            }
            catch
            {
                _settings = new AppSettings(); // fallback to defaults on error
            }
        }

        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_settings, options);
                Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
                File.WriteAllText(_settingsPath, json);
            }
            catch
            {
                // handle save errors if needed
            }
        }

        public void Reset()
        {
            _settings = new AppSettings();
            Save();
        }
    }

    public class AppSettings
    {
        public bool EnableOverlay { get; set; } = true;
        public string Theme { get; set; } = "Dark";
        public string Language { get; set; } = "en-US";
        public float MasterVolume { get; set; } = 0.8f;

        // Custom fields can be added as needed
        [JsonExtensionData]
        public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();
    }
}
