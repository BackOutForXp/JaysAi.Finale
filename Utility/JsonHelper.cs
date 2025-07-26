// neural v3.0
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JaysAi.Finale.Utility
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string Serialize<T>(T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, Options);
            }
            catch (Exception ex)
            {
                // Optionally log or handle
                throw new InvalidOperationException("Failed to serialize object.", ex);
            }
        }

        public static T? Deserialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json, Options);
            }
            catch (JsonException ex)
            {
                // Optionally log or handle
                throw new InvalidDataException("Failed to parse JSON.", ex);
            }
        }

        public static T? LoadFromFile<T>(string path)
        {
            try
            {
                if (!File.Exists(path)) return default;
                var json = File.ReadAllText(path);
                return Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to read or parse JSON from {path}", ex);
            }
        }

        public static void SaveToFile<T>(string path, T obj)
        {
            try
            {
                var json = Serialize(obj);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to write JSON to {path}", ex);
            }
        }
    }
}
