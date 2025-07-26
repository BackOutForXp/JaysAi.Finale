// Neural v3.1 — CrosshairRenderer.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class CrosshairRenderer : IOverlayRenderer
    {
        private readonly CrosshairDrawer _drawer;

        public bool IsActive { get; set; } = true;

        public CrosshairRenderer()
        {
            _drawer = new CrosshairDrawer();
        }

        public void Enable() => IsActive = true;
        public void Disable() => IsActive = false;
        public void Toggle() => IsActive = !IsActive;

        public void SetColor(SKColor color) => _drawer.Color = color;
        public void SetSize(float size) => _drawer.Size = size;
        public void SetThickness(float thickness) => _drawer.Thickness = thickness;
        public void SetStyle(CrosshairStyle style) => _drawer.Style = style;

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || canvas == null) return;
            _drawer.Draw(canvas, screenWidth, screenHeight);
        }
    }
}
