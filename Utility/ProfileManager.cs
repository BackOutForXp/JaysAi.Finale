// Neural v3.1 — ProfileManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Utility
{
    public static class ProfileManager
    {
        private static readonly string ProfilesDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JaysAi", "Finale", "Profiles");

        static ProfileManager()
        {
            Directory.CreateDirectory(ProfilesDirectory);
        }

        public static List<string> GetAvailableProfiles()
        {
            return Directory.GetFiles(ProfilesDirectory, "*.json")
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToList();
        }

        public static void SaveProfile(string profileName, AppSettings settings)
        {
            var filePath = GetProfilePath(profileName);
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static AppSettings? LoadProfile(string profileName)
        {
            var filePath = GetProfilePath(profileName);
            if (!File.Exists(filePath)) return null;

            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<AppSettings>(json);
            }
            catch
            {
                return null;
            }
        }

        public static void DeleteProfile(string profileName)
        {
            var filePath = GetProfilePath(profileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private static string GetProfilePath(string profileName)
        {
            var safeName = Path.GetFileNameWithoutExtension(profileName);
            return Path.Combine(ProfilesDirectory, safeName + ".json");
        }
    }
}
