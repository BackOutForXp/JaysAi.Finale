//neural v3.0
using System.Collections.Generic;

namespace JaysAi.Finale.Settings
{
    public class ConfigData
    {
        public string ProfileName { get; set; } = "Default";
        public bool ESPEnabled { get; set; } = true;
        public bool AimAssistEnabled { get; set; } = true;
        public bool SilentAimEnabled { get; set; } = false;
        public float AimSensitivity { get; set; } = 1.0f;
        public float Deadzone { get; set; } = 0.1f;

        public Dictionary<string, string> CustomBinds { get; set; } = new();
        public Dictionary<string, float> ThresholdSettings { get; set; } = new();
        public Dictionary<string, object> ExperimentalFlags { get; set; } = new();

        public ConfigData Clone()
        {
            return new ConfigData
            {
                ProfileName = this.ProfileName,
                ESPEnabled = this.ESPEnabled,
                AimAssistEnabled = this.AimAssistEnabled,
                SilentAimEnabled = this.SilentAimEnabled,
                AimSensitivity = this.AimSensitivity,
                Deadzone = this.Deadzone,
                CustomBinds = new Dictionary<string, string>(this.CustomBinds),
                ThresholdSettings = new Dictionary<string, float>(this.ThresholdSettings),
                ExperimentalFlags = new Dictionary<string, object>(this.ExperimentalFlags)
            };
        }
    }
}
