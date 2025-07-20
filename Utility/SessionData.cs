// Monarch v1.0 – SessionData.cs

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JaysAi.Finale.Utility
{
    public static class SessionData
    {
        private static readonly ConcurrentDictionary<string, object> _sessionVariables = new();
        private static readonly Dictionary<string, DateTime> _eventTimestamps = new();

        public static void Set<T>(string key, T value)
        {
            _sessionVariables[key] = value!;
        }

        public static T Get<T>(string key, T defaultValue = default!)
        {
            return _sessionVariables.TryGetValue(key, out var value) && value is T typed
                ? typed
                : defaultValue;
        }

        public static bool Exists(string key)
        {
            return _sessionVariables.ContainsKey(key);
        }

        public static void Clear()
        {
            _sessionVariables.Clear();
            _eventTimestamps.Clear();
        }

        public static void MarkEvent(string eventName)
        {
            _eventTimestamps[eventName] = DateTime.UtcNow;
        }

        public static TimeSpan TimeSince(string eventName)
        {
            return _eventTimestamps.TryGetValue(eventName, out var time)
                ? DateTime.UtcNow - time
                : TimeSpan.MaxValue;
        }

        public static IReadOnlyDictionary<string, object> Dump()
        {
            return _sessionVariables;
        }

        public static IReadOnlyDictionary<string, DateTime> DumpEvents()
        {
            return _eventTimestamps;
        }
    }
}
