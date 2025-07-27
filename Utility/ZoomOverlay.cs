using SkiaSharp;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Overlay
{
    public class ZoomOverlay
    {
        private readonly AppSettings _settings;
        private readonly ZoomController _zoomController;

        public ZoomOverlay(AppSettings settings, ZoomController zoomController)
        {
            _settings = settings;
            _zoomController = zoomController;
        }

        public void Draw(SKCanvas canvas, SKPaint paint, int screenWidth, int screenHeight)
        {
            if (!_settings.ShowZoomCircle)
                return;

            float zoom = _zoomController.CurrentZoom;
            float radius = _settings.ZoomOverlayRadius * zoom;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            paint.IsAntialias = true;
            paint.Style = SKPaintStyle.Stroke;
            paint.StrokeWidth = 2;
            paint.Color = SKColors.DeepSkyBlue;

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }
    }
}
