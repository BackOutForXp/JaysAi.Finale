// Monarch v1.0 – UserSettings.cs

using System.Collections.Generic;

namespace JaysAi.Finale.Utility
{
    public static class UserSettings
    {
        private static readonly Dictionary<string, object> Settings = new();

        public static T Get<T>(string key, T defaultValue = default)
        {
            if (Settings.TryGetValue(key, out var value) && value is T typedValue)
                return typedValue;

            return defaultValue!;
        }

        public static void Set<T>(string key, T value)
        {
            Settings[key] = value!;
        }

        public static void Clear() => Settings.Clear();

        public static IReadOnlyDictionary<string, object> GetAll() => Settings;
    }
}
