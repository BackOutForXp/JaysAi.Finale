// File: Settings/AppSettings.cs
using System.Drawing;

namespace JaysAi.Finale.Settings
{
    public class AppSettings
    {
        public CrosshairSettings Crosshair { get; set; } = new();
        public ESPSettings ESP { get; set; } = new();
        public AimAssistSettings AimAssist { get; set; } = new();
        public StickAssistSettings StickAssist { get; set; } = new();
        public KeybindSettings Keybinds { get; set; } = new();
        public string ActiveProfile { get; set; } = "Default";
    }

    public class CrosshairSettings
    {
        public bool Enabled { get; set; } = true;
        public float Size { get; set; } = 30f;
        public float Thickness { get; set; } = 2f;
        public Color Color { get; set; } = Color.Red;
    }

    public class ESPSettings
    {
        public bool Enabled { get; set; } = false;
        public bool ShowBoxes { get; set; } = true;
        public bool ShowNames { get; set; } = true;
        public Color EnemyColor { get; set; } = Color.LimeGreen;
    }

    public class AimAssistSettings
    {
        public bool Enabled { get; set; } = false;
        public float Smoothness { get; set; } = 0.8f;
        public float AimRadius { get; set; } = 150f;
        public bool PredictMovement { get; set; } = true;
    }

    public class StickAssistSettings
    {
        public bool Enabled { get; set; } = false;
        public float SnapStrength { get; set; } = 1.0f;
        public float DeadZoneThreshold { get; set; } = 0.2f;
    }

    public class KeybindSettings
    {
        public string ToggleESP { get; set; } = "F1";
        public string ToggleAim { get; set; } = "F2";
        public string ToggleCrosshair { get; set; } = "F3";
        public string ToggleStickAssist { get; set; } = "F4";
    }
}
