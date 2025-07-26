// neural v3.0
using System;
using System.Collections.Concurrent;

namespace JaysAi.Finale.SystemLogic
{
    public static class FeatureToggle
    {
        private static readonly ConcurrentDictionary<string, bool> _featureFlags = new(StringComparer.OrdinalIgnoreCase);

        public static event Action<string, bool>? FeatureToggled;

        public static void Enable(string featureName)
        {
            Set(featureName, true);
        }

        public static void Disable(string featureName)
        {
            Set(featureName, false);
        }

        public static void Set(string featureName, bool isEnabled)
        {
            if (_featureFlags.TryGetValue(featureName, out var current) && current == isEnabled)
                return;

            _featureFlags[featureName] = isEnabled;
            FeatureToggled?.Invoke(featureName, isEnabled);
        }

        public static bool IsEnabled(string featureName)
        {
            return _featureFlags.TryGetValue(featureName, out var isEnabled) && isEnabled;
        }

        public static void ClearAll()
        {
            _featureFlags.Clear();
        }

        public static IReadOnlyDictionary<string, bool> GetAll()
        {
            return new Dictionary<string, bool>(_featureFlags);
        }
    }
}
