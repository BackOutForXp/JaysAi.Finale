// File: System\OffsetProfileLoader.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.SystemLogic
{
    public static class OffsetProfileLoader
    {
        private static readonly string ProfilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "JaysAi",
            "Finale",
            "Offsets.json");

        public static List<OffsetProfile> LoadProfiles()
        {
            if (!File.Exists(ProfilePath))
            {
                Console.WriteLine($"[Offsets] No profile file found at {ProfilePath}. Creating default.");
                SaveDefaultProfiles();
                return LoadProfiles();
            }

            try
            {
                string json = File.ReadAllText(ProfilePath);
                var profiles = JsonSerializer.Deserialize<List<OffsetProfile>>(json) ?? new List<OffsetProfile>();
                Console.WriteLine($"[Offsets] Loaded {profiles.Count} offset profiles.");
                return profiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Offsets] Failed to load profiles: {ex.Message}");
                return new List<OffsetProfile>();
            }
        }

        public static void SaveDefaultProfiles()
        {
            var defaultProfiles = new List<OffsetProfile>
            {
                new OffsetProfile("DefaultGame", new Dictionary<string, int>
                {
                    { "Health", 0x100 },
                    { "Team", 0xF4 },
                    { "Position", 0x138 }
                })
            };

            try
            {
                string json = JsonSerializer.Serialize(defaultProfiles, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Directory.CreateDirectory(Path.GetDirectoryName(ProfilePath)!);
                File.WriteAllText(ProfilePath, json);

                Console.WriteLine("[Offsets] Default profile saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Offsets] Failed to save default: {ex.Message}");
            }
        }
    }
}
