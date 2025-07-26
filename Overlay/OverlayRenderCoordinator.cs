// neural v3.0
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System;
using System.Windows.Controls;

namespace JaysAi.Finale.Overlay
{
    public class OverlayRenderCoordinator
    {
        private readonly SKElement _overlayCanvas;

        public OverlayRenderCoordinator(SKElement canvas)
        {
            _overlayCanvas = canvas;
            _overlayCanvas.PaintSurface += OnPaintSurface;
        }

        public void DrawFrame()
        {
            _overlayCanvas.InvalidateVisual();
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            try
            {
                // Call to main overlay render system
                OverlayDrawingUtils.DrawESP(canvas, e.Info.Width, e.Info.Height);
                OverlayDrawingUtils.DrawCrosshair(canvas, e.Info.Width, e.Info.Height);
                OverlayDrawingUtils.DrawBoneLines(canvas);
                OverlayDrawingUtils.DrawSystemStats(canvas);
            }
            catch (Exception ex)
            {
                // Log if overlay drawing fails
                Console.WriteLine($"[OverlayRender] Draw error: {ex.Message}");
            }
        }
    }
}
