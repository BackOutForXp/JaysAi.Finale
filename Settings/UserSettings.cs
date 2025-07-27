// Neural v3.1 — UserSettings.cs
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.Settings
{
    public sealed class UserSettings
    {
        private static readonly Lazy<UserSettings> _instance = new(() => new UserSettings());
        public static UserSettings Instance => _instance.Value;

        private readonly string _settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "user-settings.json");
        private Dictionary<string, object> _settings;

        public IReadOnlyDictionary<string, object> All => _settings;

        private UserSettings()
        {
            Load();
        }

        public void Set<T>(string key, T value)
        {
            _settings[key] = value!;
            Save();
        }

        public T Get<T>(string key, T fallback = default!)
        {
            if (_settings.TryGetValue(key, out var value))
            {
                if (value is JsonElement element)
                {
                    try
                    {
                        return element.Deserialize<T>() ?? fallback;
                    }
                    catch
                    {
                        return fallback;
                    }
                }

                if (value is T typed)
                    return typed;
            }

            return fallback;
        }

        public void Remove(string key)
        {
            if (_settings.Remove(key))
                Save();
        }

        public void Clear()
        {
            _settings.Clear();
            Save();
        }

        private void Load()
        {
            try
            {
                if (File.Exists(_settingsFile))
                {
                    var json = File.ReadAllText(_settingsFile);
                    _settings = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new();
                }
                else
                {
                    _settings = new Dictionary<string, object>();
                    Save();
                }
            }
            catch
            {
                _settings = new Dictionary<string, object>();
            }
        }

        private void Save()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
                };

                Directory.CreateDirectory(Path.GetDirectoryName(_settingsFile)!);
                var json = JsonSerializer.Serialize(_settings, options);
                File.WriteAllText(_settingsFile, json);
            }
            catch
            {
                // Silent fallback
            }
        }

        // =========================
        // 🔧 Neural v3.1 Properties
        // =========================

        public bool EnableESP
        {
            get => Get("EnableESP", true);
            set => Set("EnableESP", value);
        }

        public float EspBoxThickness
        {
            get => Get("EspBoxThickness", 2.0f);
            set => Set("EspBoxThickness", value);
        }

        public SKColor EspBoxColor
        {
            get => Get("EspBoxColor", SKColors.Lime);
            set => Set("EspBoxColor", value);
        }

        public bool EspAntiAlias
        {
            get => Get("EspAntiAlias", true);
            set => Set("EspAntiAlias", value);
        }

        public bool EspBoxFillEnabled
        {
            get => Get("EspBoxFillEnabled", false);
            set => Set("EspBoxFillEnabled", value);
        }

        public SKColor EspBoxFillColor
        {
            get => Get("EspBoxFillColor", SKColors.Red.WithAlpha(60));
            set => Set("EspBoxFillColor", value);
        }

        // ✅ UI-Bound Properties for ESP Panel
        public bool ESPEnabled
        {
            get => Get("ESPEnabled", true);
            set => Set("ESPEnabled", value);
        }

        public bool BoxESP
        {
            get => Get("BoxESP", true);
            set => Set("BoxESP", value);
        }

        public bool HealthBarESP
        {
            get => Get("HealthBarESP", true);
            set => Set("HealthBarESP", value);
        }
    }
}
