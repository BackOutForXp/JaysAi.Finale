// neural v3.0
using System.Collections.Generic;
using System.IO;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Features
{
    public static class ProfileManager
    {
        private static readonly string ProfilesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profiles");
        private static readonly Dictionary<string, UserSettings> CachedProfiles = new();

        public static void Initialize()
        {
            if (!Directory.Exists(ProfilesDirectory))
                Directory.CreateDirectory(ProfilesDirectory);

            LoadAllProfiles();
        }

        public static void SaveProfile(string name)
        {
            string path = GetProfilePath(name);
            JsonUtility.Serialize(UserSettings.Current, path);
        }

        public static void LoadProfile(string name)
        {
            string path = GetProfilePath(name);
            if (File.Exists(path))
            {
                var loaded = JsonUtility.Deserialize<UserSettings>(path);
                if (loaded != null)
                {
                    UserSettings.Current = loaded;
                    SettingManager.Apply();
                }
            }
        }

        public static List<string> ListProfiles()
        {
            var profileFiles = Directory.GetFiles(ProfilesDirectory, "*.json");
            var profileNames = new List<string>();
            foreach (var file in profileFiles)
                profileNames.Add(Path.GetFileNameWithoutExtension(file));
            return profileNames;
        }

        private static string GetProfilePath(string name)
        {
            return Path.Combine(ProfilesDirectory, $"{name}.json");
        }

        private static void LoadAllProfiles()
        {
            foreach (var name in ListProfiles())
            {
                var path = GetProfilePath(name);
                var profile = JsonUtility.Deserialize<UserSettings>(path);
                if (profile != null && !CachedProfiles.ContainsKey(name))
                    CachedProfiles.Add(name, profile);
            }
        }
    }
}
