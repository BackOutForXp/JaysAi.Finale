// Monarch v1.0 – JsonUtility.cs

using System;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.Utility
{
    public static class JsonUtility
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public static void Save<T>(string filePath, T data)
        {
            var json = JsonSerializer.Serialize(data, Options);
            File.WriteAllText(filePath, json);
        }

        public static T Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"JSON file not found at path: {filePath}");

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, Options)!;
        }

        public static T? TryLoad<T>(string filePath)
        {
            try
            {
                return Load<T>(filePath);
            }
            catch
            {
                return default;
            }
        }
    }
}
