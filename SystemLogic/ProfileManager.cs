using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

namespace JaysAi.Finale.SystemLogic
{
    public static class ProfileManager
    {
        private static readonly string ProfileDirectory = "Profiles";

        public static void SaveProfile(string name, ConfigData config)
        {
            try
            {
                Directory.CreateDirectory(ProfileDirectory);
                string path = Path.Combine(ProfileDirectory, $"{name}.json");
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, json);
                Console.WriteLine($"[ProfileManager] Saved profile: {name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileManager] Error saving profile {name}: {ex.Message}");
            }
        }

        public static ConfigData? LoadProfile(string name)
        {
            try
            {
                string path = Path.Combine(ProfileDirectory, $"{name}.json");
                if (!File.Exists(path))
                {
                    Console.WriteLine($"[ProfileManager] Profile '{name}' not found.");
                    return null;
                }

                string json = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<ConfigData>(json);
                Console.WriteLine($"[ProfileManager] Loaded profile: {name}");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileManager] Error loading profile {name}: {ex.Message}");
                return null;
            }
        }

        public static void DeleteProfile(string name)
        {
            string path = Path.Combine(ProfileDirectory, $"{name}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"[ProfileManager] Deleted profile: {name}");
            }
        }

        public static string[] ListProfiles()
        {
            Directory.CreateDirectory(ProfileDirectory);
            var files = Directory.GetFiles(ProfileDirectory, "*.json");
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileNameWithoutExtension(files[i]);

            return files;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [ ] Add Profile dropdown to LoaderGUI.xaml (Owner/Elite only?)
// - [x] Reads and writes /Profiles/*.json
// - [ ] Sync with ConfigManager so loader can apply profile live
// - [ ] Future: Add import/export + profile sharing
// ===================================================================
