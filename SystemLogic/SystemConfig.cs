// neural v3.0
using System;
using System.IO;
using System.Text.Json;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public static class SystemConfig
    {
        private const string ConfigFileName = "system_config.json";
        private static readonly object _lock = new();

        public static string LoaderVersion { get; private set; } = "1.0.0";
        public static bool DebugMode { get; private set; } = false;
        public static bool EnableSafeBoot { get; private set; } = true;
        public static string EnvironmentTag { get; private set; } = "production";

        public static void Load()
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(ConfigFileName))
                    {
                        Log.Warn($"System config not found. Generating default at {ConfigFileName}");
                        Save(); // Write defaults
                        return;
                    }

                    string json = File.ReadAllText(ConfigFileName);
                    var config = JsonSerializer.Deserialize<SystemConfigModel>(json);
                    if (config == null)
                        throw new Exception("Deserialized config is null");

                    LoaderVersion = config.LoaderVersion;
                    DebugMode = config.DebugMode;
                    EnableSafeBoot = config.EnableSafeBoot;
                    EnvironmentTag = config.EnvironmentTag;

                    Log.Info("System configuration loaded.");
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed to load system configuration: {ex.Message}");
                    Save(); // Recreate with defaults
                }
            }
        }

        public static void Save()
        {
            lock (_lock)
            {
                var config = new SystemConfigModel
                {
                    LoaderVersion = LoaderVersion,
                    DebugMode = DebugMode,
                    EnableSafeBoot = EnableSafeBoot,
                    EnvironmentTag = EnvironmentTag
                };

                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigFileName, json);
                Log.Info("System configuration saved.");
            }
        }

        private class SystemConfigModel
        {
            public string LoaderVersion { get; set; } = "1.0.0";
            public bool DebugMode { get; set; } = false;
            public bool EnableSafeBoot { get; set; } = true;
            public string EnvironmentTag { get; set; } = "production";
        }
    }
}
