//monarch v2.1 – Config Save/Load Manager
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.Utility
{
    public static class ConfigManager
    {
        private static readonly string ConfigPath = "JaysAiConfig.json";

        public static void Save()
        {
            var config = new FeatureSettings
            {
                EspEnabled = FeatureToggle.EspEnabled,
                BoxEsp = FeatureToggle.BoxEsp,
                NameEsp = FeatureToggle.NameEsp,
                AimbotEnabled = FeatureToggle.AimbotEnabled,
                AimFov = FeatureToggle.AimFov,
                AimSmoothness = FeatureToggle.AimSmoothness,
                RecoilControlEnabled = FeatureToggle.RecoilControlEnabled,
                RecoilSmoothness = FeatureToggle.RecoilSmoothness,
                SnapAssistEnabled = FeatureToggle.SnapAssistEnabled,
                SnapStrength = FeatureToggle.SnapStrength,
                StealthMode = FeatureToggle.StealthMode,
                LoaderActive = FeatureToggle.LoaderActive
            };

            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        public static void Load()
        {
            if (!File.Exists(ConfigPath))
                return;

            var json = File.ReadAllText(ConfigPath);
            var config = JsonSerializer.Deserialize<FeatureSettings>(json);

            FeatureToggle.EspEnabled = config.EspEnabled;
            FeatureToggle.BoxEsp = config.BoxEsp;
            FeatureToggle.NameEsp = config.NameEsp;
            FeatureToggle.AimbotEnabled = config.AimbotEnabled;
            FeatureToggle.AimFov = config.AimFov;
            FeatureToggle.AimSmoothness = config.AimSmoothness;
            FeatureToggle.RecoilControlEnabled = config.RecoilControlEnabled;
            FeatureToggle.RecoilSmoothness = config.RecoilSmoothness;
            FeatureToggle.SnapAssistEnabled = config.SnapAssistEnabled;
            FeatureToggle.SnapStrength = config.SnapStrength;
            FeatureToggle.StealthMode = config.StealthMode;
            FeatureToggle.LoaderActive = config.LoaderActive;
        }

        private class FeatureSettings
        {
            public bool EspEnabled { get; set; }
            public bool BoxEsp { get; set; }
            public bool NameEsp { get; set; }
            public bool AimbotEnabled { get; set; }
            public float AimFov { get; set; }
            public float AimSmoothness { get; set; }
            public bool RecoilControlEnabled { get; set; }
            public float RecoilSmoothness { get; set; }
            public bool SnapAssistEnabled { get; set; }
            public float SnapStrength { get; set; }
            public bool StealthMode { get; set; }
            public bool LoaderActive { get; set; }
        }
    }
}
