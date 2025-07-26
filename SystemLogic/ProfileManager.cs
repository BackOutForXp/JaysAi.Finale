// neural v3.0
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class ProfileManager
    {
        private static readonly Lazy<ProfileManager> _instance = new(() => new ProfileManager());
        private readonly string _profileDirectory;
        private readonly Dictionary<string, UserProfile> _profiles;

        public static ProfileManager Instance => _instance.Value;
        public UserProfile? ActiveProfile { get; private set; }

        private ProfileManager()
        {
            _profileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profiles");
            _profiles = new Dictionary<string, UserProfile>(StringComparer.OrdinalIgnoreCase);

            Directory.CreateDirectory(_profileDirectory);
            LoadAllProfiles();
        }

        private void LoadAllProfiles()
        {
            foreach (var file in Directory.GetFiles(_profileDirectory, "*.json"))
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var profile = JsonSerializer.Deserialize<UserProfile>(json);
                    if (profile != null && !_profiles.ContainsKey(profile.Name))
                        _profiles[profile.Name] = profile;
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Failed to load profile: {file}. Error: {ex.Message}");
                }
            }
        }

        public void SaveProfile(UserProfile profile)
        {
            try
            {
                string path = Path.Combine(_profileDirectory, $"{profile.Name}.json");
                string json = JsonSerializer.Serialize(profile, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
                _profiles[profile.Name] = profile;
                Logger.Info($"Profile saved: {profile.Name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to save profile {profile.Name}: {ex.Message}");
            }
        }

        public void DeleteProfile(string name)
        {
            if (_profiles.TryGetValue(name, out _))
            {
                string path = Path.Combine(_profileDirectory, $"{name}.json");
                if (File.Exists(path)) File.Delete(path);
                _profiles.Remove(name);
                Logger.Info($"Profile deleted: {name}");
            }
        }

        public bool LoadProfile(string name)
        {
            if (_profiles.TryGetValue(name, out var profile))
            {
                ActiveProfile = profile;
                Logger.Info($"Profile loaded: {name}");
                return true;
            }

            Logger.Warn($"Profile not found: {name}");
            return false;
        }

        public IEnumerable<string> GetAllProfileNames() => _profiles.Keys;

        public void SetActiveProfile(UserProfile profile)
        {
            ActiveProfile = profile;
            Logger.Info($"Active profile set to: {profile.Name}");
        }
    }
}
