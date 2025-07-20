// File: Visuals/OverlayState.cs
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Visuals
{
    public class OverlayState
    {
        public bool IsActive { get; set; } = true;

        public List<OverlayEnemyState> Enemies { get; set; } = new();

        public Vector2 ScreenSize { get; set; }

        public bool ShowCrosshair { get; set; }
        public bool ShowESP { get; set; }
    }

    public class OverlayEnemyState
    {
        public string Name { get; set; } = "Enemy";
        public Vector2 ScreenPosition { get; set; }
        public float Health { get; set; }
        public bool IsVisible { get; set; }
        public bool IsHighlighted { get; set; } = false;
    }
}
