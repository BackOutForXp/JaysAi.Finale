// Monarch v1.0 – ConfigManager.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.SystemLogic
{
    public static class ConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        private static Dictionary<string, object> _configData = new();

        static ConfigManager()
        {
            LoadConfig();
        }

        public static void LoadConfig()
        {
            if (!File.Exists(ConfigPath))
            {
                _configData = new Dictionary<string, object>();
                SaveConfig();
                return;
            }

            try
            {
                string json = File.ReadAllText(ConfigPath);
                _configData = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigManager] Failed to load config: {ex.Message}");
                _configData = new Dictionary<string, object>();
            }
        }

        public static void SaveConfig()
        {
            try
            {
                string json = JsonSerializer.Serialize(_configData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigManager] Failed to save config: {ex.Message}");
            }
        }

        public static T Get<T>(string key, T defaultValue = default)
        {
            if (_configData.TryGetValue(key, out var value) && value is JsonElement jsonElement)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(jsonElement.GetRawText()) ?? defaultValue;
                }
                catch
                {
                    return defaultValue;
                }
            }

            return value is T casted ? casted : defaultValue;
        }

        public static void Set<T>(string key, T value)
        {
            _configData[key] = value!;
            SaveConfig();
        }

        public static void Delete(string key)
        {
            if (_configData.Remove(key))
                SaveConfig();
        }

        public static void Clear()
        {
            _configData.Clear();
            SaveConfig();
        }
    }
}
