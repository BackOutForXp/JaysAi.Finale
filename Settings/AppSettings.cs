// File: Settings/AppSettings.cs
using System;

namespace JaysAi.Finale.Settings
{
    public class AppSettings
    {
        public bool EnableESP { get; set; } = true;
        public bool EnableAimAssist { get; set; } = true;
        public bool EnableCrosshair { get; set; } = true;
        public bool EnableStealth { get; set; } = false;
        public bool EnableStickAssist { get; set; } = false;

        public int TickRate { get; set; } = 60;

        // Crosshair
        public string CrosshairColor { get; set; } = "#FF0000";
        public int CrosshairLength { get; set; } = 10;
        public int CrosshairThickness { get; set; } = 2;
        public bool ShowCenterDot { get; set; } = true;

        // Aim Assist
        public float SmoothingAmount { get; set; } = 4.0f;
        public float FovLimit { get; set; } = 100.0f;

        // Profiles (optional future)
        public string ActiveProfileName { get; set; } = "Default";
    }
}

// ✅ Holds all toggle + config data
// ✅ Used by FeatureManager, MainLoop, UI bindings
// ☐ Expand with saved profiles and hotload logic
