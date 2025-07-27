using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace JaysAi.Finale.Settings
{
    public class SettingsManager
    {
        private readonly string _appName;
        private readonly string _projectName;
        private readonly string _fileName;

        private readonly string _profileDirectory;
        private readonly string _defaultPath;

        public SettingsManager(string appName, string projectName, string fileName)
        {
            _appName = appName;
            _projectName = projectName;
            _fileName = fileName;

            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _appName, _projectName);
            _defaultPath = Path.Combine(basePath, fileName);
            _profileDirectory = Path.Combine(basePath, "Profiles");

            Directory.CreateDirectory(basePath);
            Directory.CreateDirectory(_profileDirectory);
        }

        public void Save<T>(string profileName, T settings)
        {
            string path = Path.Combine(_profileDirectory, $"{profileName}.json");
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public void Save<T>(T settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_defaultPath, json);
        }

        public T Load<T>(string profileName)
        {
            string path = Path.Combine(_profileDirectory, $"{profileName}.json");
            if (!File.Exists(path)) return default;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        public T Load<T>()
        {
            if (!File.Exists(_defaultPath)) return default;

            string json = File.ReadAllText(_defaultPath);
            return JsonSerializer.Deserialize<T>(json);
        }

        public void Delete(string profileName)
        {
            string path = Path.Combine(_profileDirectory, $"{profileName}.json");
            if (File.Exists(path))
                File.Delete(path);
        }

        public List<string> GetAvailableProfiles()
        {
            if (!Directory.Exists(_profileDirectory))
                return new List<string>();

            return Directory.GetFiles(_profileDirectory, "*.json")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }
    }
}
