// neural v3.0
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using JaysAi.Finale.Settings.Models;

namespace JaysAi.Finale.Settings
{
    public sealed class ProfileManager
    {
        private static readonly Lazy<ProfileManager> _instance = new(() => new ProfileManager());
        private readonly string _profilesPath = Path.Combine(AppContext.BaseDirectory, "Profiles");
        private readonly Dictionary<string, UserSettings> _profiles = new(StringComparer.OrdinalIgnoreCase);

        public static ProfileManager Instance => _instance.Value;

        private ProfileManager()
        {
            Directory.CreateDirectory(_profilesPath);
            LoadAllProfiles();
        }

        private void LoadAllProfiles()
        {
            _profiles.Clear();

            foreach (var file in Directory.GetFiles(_profilesPath, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var profile = JsonSerializer.Deserialize<UserSettings>(json);
                    var profileName = Path.GetFileNameWithoutExtension(file);

                    if (profile != null)
                        _profiles[profileName] = profile;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ProfileManager] Failed to load profile '{file}': {ex.Message}");
                }
            }
        }

        public IReadOnlyDictionary<string, UserSettings> Profiles => _profiles;

        public bool SaveProfile(string profileName, UserSettings settings)
        {
            try
            {
                var path = GetProfilePath(profileName);
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
                _profiles[profileName] = settings;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileManager] Failed to save profile '{profileName}': {ex.Message}");
                return false;
            }
        }

        public bool DeleteProfile(string profileName)
        {
            try
            {
                var path = GetProfilePath(profileName);
                if (File.Exists(path))
                    File.Delete(path);

                return _profiles.Remove(profileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileManager] Failed to delete profile '{profileName}': {ex.Message}");
                return false;
            }
        }

        public bool TryGetProfile(string profileName, out UserSettings? settings)
        {
            return _profiles.TryGetValue(profileName, out settings);
        }

        private string GetProfilePath(string profileName)
        {
            return Path.Combine(_profilesPath, $"{profileName}.json");
        }
    }
}
