// neural v3.0
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JaysAi.Finale.Helpers.System
{
    public static class ConfigManager
    {
        private static readonly string ConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");

        public static async Task SaveAsync<T>(string configName, T configData)
        {
            try
            {
                Directory.CreateDirectory(ConfigDirectory);

                string filePath = Path.Combine(ConfigDirectory, $"{configName}.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(configData, options);

                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigManager] Failed to save config: {ex.Message}");
            }
        }

        public static async Task<T?> LoadAsync<T>(string configName)
        {
            try
            {
                string filePath = Path.Combine(ConfigDirectory, $"{configName}.json");
                if (!File.Exists(filePath)) return default;

                string json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigManager] Failed to load config: {ex.Message}");
                return default;
            }
        }

        public static void Delete(string configName)
        {
            try
            {
                string filePath = Path.Combine(ConfigDirectory, $"{configName}.json");
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigManager] Failed to delete config: {ex.Message}");
            }
        }

        public static bool Exists(string configName)
        {
            string filePath = Path.Combine(ConfigDirectory, $"{configName}.json");
            return File.Exists(filePath);
        }
    }
}
