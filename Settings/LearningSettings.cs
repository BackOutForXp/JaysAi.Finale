using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Settings
{
    public class LearningSettings
    {
        private const string SavePath = "AppData/JaysAi/Finale/LearnedProfiles.json";

        public Dictionary<int, TargetProfile> Profiles { get; set; } = new();

        public static LearningSettings Load()
        {
            try
            {
                if (!File.Exists(SavePath))
                    return new LearningSettings();

                var json = File.ReadAllText(SavePath);
                return JsonSerializer.Deserialize<LearningSettings>(json) ?? new LearningSettings();
            }
            catch
            {
                return new LearningSettings();
            }
        }

        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };

            var json = JsonSerializer.Serialize(this, options);
            Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
            File.WriteAllText(SavePath, json);
        }

        public void SyncFrom(TargetProfileManager manager)
        {
            Profiles.Clear();
            foreach (var profile in manager.GetAll())
            {
                Profiles[profile.EnemyId] = profile;
            }
        }

        public void SyncTo(TargetProfileManager manager)
        {
            foreach (var kvp in Profiles)
            {
                manager.GetOrCreate(kvp.Key).PreferredBone = kvp.Value.PreferredBone;
                manager.GetOrCreate(kvp.Key).AimSmoothing = kvp.Value.AimSmoothing;
                manager.GetOrCreate(kvp.Key).SnapDelayMs = kvp.Value.SnapDelayMs;
                manager.GetOrCreate(kvp.Key).EnablePrediction = kvp.Value.EnablePrediction;
                manager.GetOrCreate(kvp.Key).ConfidenceScore = kvp.Value.ConfidenceScore;
            }
        }
    }
}
