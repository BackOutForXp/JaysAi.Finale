// File: Security/FeatureManager.cs
using JaysAi.Finale.Settings;
using System;

namespace JaysAi.Finale.Security
{
    public static class FeatureManager
    {
        private static AppSettings? _settings;

        public static void Initialize(AppSettings settings)
        {
            _settings = settings;
        }

        public static bool IsFeatureEnabled(string featureKey)
        {
            return featureKey switch
            {
                "AimAssist" => _settings?.EnableAimAssist ?? false,
                "ESP" => _settings?.EnableESP ?? false,
                "Crosshair" => _settings?.EnableCrosshair ?? false,
                "Stealth" => _settings?.EnableStealthMode ?? false,
                _ => false
            };
        }

        public static void RequireFeature(string featureKey)
        {
            if (!IsFeatureEnabled(featureKey))
                throw new UnauthorizedAccessException($"Feature '{featureKey}' is not enabled.");
        }
    }
}
