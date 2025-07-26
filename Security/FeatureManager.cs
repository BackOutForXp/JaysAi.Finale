// neural v3.0
using System;
using System.Collections.Concurrent;
using JaysAi.Finale.Models;

namespace JaysAi.Finale.Security
{
    public sealed class FeatureManager
    {
        private static readonly Lazy<FeatureManager> _instance = new(() => new FeatureManager());
        public static FeatureManager Instance => _instance.Value;

        private readonly ConcurrentDictionary<FeatureFlag, bool> _featureStates;

        private FeatureManager()
        {
            _featureStates = new ConcurrentDictionary<FeatureFlag, bool>();

            // Default feature state setup (can be replaced by config file or server sync)
            foreach (FeatureFlag flag in Enum.GetValues(typeof(FeatureFlag)))
                _featureStates[flag] = false;
        }

        public bool IsEnabled(FeatureFlag feature)
        {
            return _featureStates.TryGetValue(feature, out var enabled) && enabled;
        }

        public void EnableFeature(FeatureFlag feature)
        {
            _featureStates[feature] = true;
        }

        public void DisableFeature(FeatureFlag feature)
        {
            _featureStates[feature] = false;
        }

        public void ToggleFeature(FeatureFlag feature)
        {
            _featureStates.AddOrUpdate(feature, true, (_, current) => !current);
        }

        public void SetFeatureState(FeatureFlag feature, bool enabled)
        {
            _featureStates[feature] = enabled;
        }

        public void ResetAll()
        {
            foreach (var key in _featureStates.Keys)
                _featureStates[key] = false;
        }
    }
}
