// Neural v3.0 — OverlayDrawer.cs
using SkiaSharp;
using JaysAi.Finale.Overlay;

namespace JaysAi.Finale.Overlay
{
    public class OverlayDrawer
    {
        private readonly CrosshairRenderer _crosshairRenderer;
        private readonly FovRingRenderer _fovRingRenderer;
        private readonly EspDrawer _espDrawer;
        private readonly DebugConsoleOverlay? _debugOverlay;

        public bool IsActive { get; set; } = true;

        public OverlayDrawer(
            CrosshairRenderer crosshairRenderer,
            FovRingRenderer fovRingRenderer,
            EspDrawer espDrawer,
            DebugConsoleOverlay? debugOverlay = null)
        {
            _crosshairRenderer = crosshairRenderer;
            _fovRingRenderer = fovRingRenderer;
            _espDrawer = espDrawer;
            _debugOverlay = debugOverlay;
        }

        public void DrawAll(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || canvas == null)
                return;

            // Draw FOV ring first
            _fovRingRenderer?.Draw(canvas, screenWidth, screenHeight);

            // Draw ESP
            _espDrawer?.Draw(canvas, screenWidth, screenHeight);

            // Draw crosshair last (centered)
            _crosshairRenderer?.Render(canvas, screenWidth, screenHeight);

            // Optional debug overlay - handled in WPF, not Skia
            // This is a placeholder for future expansion if we render debug overlays via Skia
        }

        public void SetOverlayState(bool enabled)
        {
            IsActive = enabled;
        }

        public void ToggleOverlay()
        {
            IsActive = !IsActive;
        }
    }
}
