//neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Settings
{
    public sealed class AppSettings
    {
        private static readonly Lazy<AppSettings> _instance = new(() => new AppSettings());

        public static AppSettings Instance => _instance.Value;

        // Core Configurable Settings
        public bool IsDebugModeEnabled { get; set; } = false;
        public bool EnableOverlay { get; set; } = true;
        public bool UseControllerInput { get; set; } = true;
        public string CurrentGameProfile { get; set; } = "BO6";

        // Diagnostic Logging
        public bool EnableDiagnostics { get; set; } = false;
        public string DiagnosticsOutputPath { get; set; } = "Logs/Diagnostics.log";

        // Performance Settings
        public int FrameRateLimit { get; set; } = 144;
        public int MaxThreadPoolSize { get; set; } = 8;

        // Experimental Flags
        public bool UseExperimentalFeatures { get; set; } = false;
        public bool EnableNeuralFeedback { get; set; } = false;

        // Input Profiles
        public Dictionary<string, string> InputProfileMappings { get; private set; }

        private AppSettings()
        {
            InputProfileMappings = new Dictionary<string, string>
            {
                { "Default", "Input/Profiles/Default.json" },
                { "ControllerBO6", "Input/Profiles/ControllerBO6.json" }
            };
        }

        public void LoadFrom(AppSettings other)
        {
            if (other == null) return;

            IsDebugModeEnabled = other.IsDebugModeEnabled;
            EnableOverlay = other.EnableOverlay;
            UseControllerInput = other.UseControllerInput;
            CurrentGameProfile = other.CurrentGameProfile;
            EnableDiagnostics = other.EnableDiagnostics;
            DiagnosticsOutputPath = other.DiagnosticsOutputPath;
            FrameRateLimit = other.FrameRateLimit;
            MaxThreadPoolSize = other.MaxThreadPoolSize;
            UseExperimentalFeatures = other.UseExperimentalFeatures;
            EnableNeuralFeedback = other.EnableNeuralFeedback;

            InputProfileMappings = new Dictionary<string, string>(other.InputProfileMappings);
        }
    }
}
