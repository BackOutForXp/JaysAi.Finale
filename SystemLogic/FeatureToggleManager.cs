// neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.SystemLogic
{
    public class FeatureToggleManager
    {
        private readonly Dictionary<string, Action<bool>> _onToggleCallbacks = new();

        public FeatureToggleManager()
        {
            FeatureToggle.FeatureToggled += OnFeatureToggled;
        }

        private void OnFeatureToggled(string featureName, bool isEnabled)
        {
            if (_onToggleCallbacks.TryGetValue(featureName, out var callback))
            {
                callback?.Invoke(isEnabled);
            }
        }

        public void RegisterCallback(string featureName, Action<bool> onToggle)
        {
            if (string.IsNullOrWhiteSpace(featureName)) return;

            _onToggleCallbacks[featureName] = onToggle;

            // Immediate invoke with current state
            onToggle(FeatureToggle.IsEnabled(featureName));
        }

        public void UnregisterCallback(string featureName)
        {
            _onToggleCallbacks.Remove(featureName);
        }

        public void Dispose()
        {
            _onToggleCallbacks.Clear();
        }
    }
}
