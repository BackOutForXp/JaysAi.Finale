// neural v3.0
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JaysAi.Finale.Utility
{
    public static class JsonUtility
    {
        public static JsonNode? Parse(string json)
        {
            try
            {
                return JsonNode.Parse(json);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Unable to parse JSON string.", ex);
            }
        }

        public static T? GetValue<T>(JsonNode? node, string key)
        {
            if (node == null || !node.AsObject().TryGetPropertyValue(key, out var value))
                return default;

            try
            {
                return value?.GetValue<T>();
            }
            catch
            {
                return default;
            }
        }

        public static JsonNode Merge(JsonNode target, JsonNode source)
        {
            if (target is JsonObject targetObj && source is JsonObject sourceObj)
            {
                foreach (var kvp in sourceObj)
                {
                    targetObj[kvp.Key] = kvp.Value;
                }
            }

            return target;
        }

        public static JsonNode CreateObject()
        {
            return new JsonObject();
        }

        public static JsonNode LoadFromFile(string path)
        {
            try
            {
                if (!File.Exists(path)) return new JsonObject();
                var json = File.ReadAllText(path);
                return Parse(json) ?? new JsonObject();
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to load JSON from {path}", ex);
            }
        }

        public static void SaveToFile(string path, JsonNode node)
        {
            try
            {
                var json = node.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save JSON to {path}", ex);
            }
        }
    }
}
