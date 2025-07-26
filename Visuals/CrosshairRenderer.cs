// Neural v3.0 — CrosshairRenderer.cs
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;

namespace JaysAi.Finale.Overlay
{
    public class CrosshairRenderer
    {
        private readonly CrosshairDrawer _drawer;
        public bool IsEnabled { get; private set; } = true;

        public CrosshairRenderer()
        {
            _drawer = new CrosshairDrawer();
        }

        public void Enable() => IsEnabled = true;

        public void Disable() => IsEnabled = false;

        public void Toggle() => IsEnabled = !IsEnabled;

        public void SetColor(SKColor color) => _drawer.Color = color;

        public void SetSize(float size) => _drawer.Size = size;

        public void SetThickness(float thickness) => _drawer.Thickness = thickness;

        public void SetStyle(CrosshairStyle style) => _drawer.Style = style;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsEnabled || canvas == null) return;

            _drawer.Draw(canvas, screenWidth, screenHeight);
        }
    }
}
