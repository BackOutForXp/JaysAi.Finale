// File: Settings/SettingsManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.Settings
{
    public class SettingsManager<T> where T : new()
    {
        private static readonly string AppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "JaysAi", "Finale", "Profiles");

        private readonly Dictionary<string, T> _loadedProfiles = new();
        private string _currentProfile = "default";

        public string CurrentProfile => _currentProfile;
        public T Settings { get; private set; } = new();

        public SettingsManager()
        {
            Directory.CreateDirectory(AppDataPath);
            LoadProfile("default");
        }

        public void Save()
        {
            string filePath = GetProfilePath(_currentProfile);
            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void LoadProfile(string profileName)
        {
            _currentProfile = profileName;
            string filePath = GetProfilePath(profileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Settings = JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            else
            {
                Settings = new T();
                Save(); // auto-create profile if missing
            }

            if (!_loadedProfiles.ContainsKey(profileName))
                _loadedProfiles.Add(profileName, Settings);
        }

        public void DeleteProfile(string profileName)
        {
            string filePath = GetProfilePath(profileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            if (_loadedProfiles.ContainsKey(profileName)) _loadedProfiles.Remove(profileName);
        }

        public IEnumerable<string> GetAvailableProfiles()
        {
            if (!Directory.Exists(AppDataPath)) yield break;

            foreach (var file in Directory.GetFiles(AppDataPath, "*.json"))
            {
                yield return Path.GetFileNameWithoutExtension(file);
            }
        }

        private string GetProfilePath(string profileName)
        {
            return Path.Combine(AppDataPath, $"{profileName}.json");
        }
    }
}
