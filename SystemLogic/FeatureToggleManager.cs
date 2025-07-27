// Neural v3.1 — FeatureToggleManager.cs
using System.Collections.Generic;

namespace JaysAi.Finale.Features
{
    public static class FeatureToggleManager
    {
        private static readonly Dictionary<string, bool> _toggles = new();

        public static void Enable(string featureName)
        {
            _toggles[featureName] = true;
        }

        public static void Disable(string featureName)
        {
            _toggles[featureName] = false;
        }

        public static void Toggle(string featureName)
        {
            _toggles[featureName] = !_toggles.GetValueOrDefault(featureName, false);
        }

        public static bool IsEnabled(string featureName)
        {
            return _toggles.TryGetValue(featureName, out var enabled) && enabled;
        }

        public static void Reset()
        {
            _toggles.Clear();
        }

        public static IReadOnlyDictionary<string, bool> GetAll() => _toggles;
    }
}
