// File: System\ConfigData.cs
using System;
using System.IO;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.SystemLogic
{
    public static class ConfigData
    {
        private static readonly string ProfileFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JaysAi", "Finale", "Profiles");

        public static string GetProfilePath(string profileName)
        {
            return Path.Combine(ProfileFolder, $"{profileName}.json");
        }

        public static bool ProfileExists(string profileName)
        {
            return File.Exists(GetProfilePath(profileName));
        }

        public static void DeleteProfile(string profileName)
        {
            var path = GetProfilePath(profileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static string[] GetAllProfileNames()
        {
            if (!Directory.Exists(ProfileFolder))
                return Array.Empty<string>();

            var files = Directory.GetFiles(ProfileFolder, "*.json");
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileNameWithoutExtension(files[i]);

            return files;
        }

        public static void EnsureProfileFolder()
        {
            if (!Directory.Exists(ProfileFolder))
                Directory.CreateDirectory(ProfileFolder);
        }
    }
}
