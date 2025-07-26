//neural v3.0
using System;

namespace JaysAi.Finale.Modules
{
    public sealed class SnapSettings
    {
        public bool SnapAssistEnabled { get; set; } = true;

        // Angle-based targeting
        public float SnapFOV { get; set; } = 15.0f;
        public float SnapSpeed { get; set; } = 12.5f;

        // Distance limit to avoid snapping too far
        public float MaxSnapDistance { get; set; } = 300.0f;

        // Ignore targets within this radius (prevent micro jitter)
        public float DeadzoneRadius { get; set; } = 2.0f;

        // Target prioritization (head, body, etc.)
        public string TargetPriority { get; set; } = "head";

        // Layers to filter (e.g., enemy vs team)
        public int TargetLayers { get; set; } = 1; // 1 = enemy, 2 = team, etc.

        // Prediction tuning
        public bool PredictMovement { get; set; } = true;
        public float PredictionMultiplier { get; set; } = 1.0f;

        // Cooldown logic
        public int CooldownMs { get; set; } = 250;

        public SnapSettings Clone()
        {
            return (SnapSettings)MemberwiseClone();
        }
    }
}
