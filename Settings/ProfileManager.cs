// File: Settings/ProfileManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JaysAi.Finale.Settings
{
    public static class ProfileManager
    {
        private static readonly string ProfilesFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JaysAi", "Finale", "Profiles");

        static ProfileManager()
        {
            Directory.CreateDirectory(ProfilesFolder);
        }

        public static void SaveProfile(string name, AppSettings settings)
        {
            string path = Path.Combine(ProfilesFolder, $"{name}.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(settings, options));
        }

        public static AppSettings? LoadProfile(string name)
        {
            string path = Path.Combine(ProfilesFolder, $"{name}.json");
            if (!File.Exists(path)) return null;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppSettings>(json);
        }

        public static bool DeleteProfile(string name)
        {
            string path = Path.Combine(ProfilesFolder, $"{name}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public static List<string> ListProfiles()
        {
            return Directory.GetFiles(ProfilesFolder, "*.json")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }
    }
}
