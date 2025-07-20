// File: Features/Crosshair.cs
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Visuals;
using SkiaSharp;
using System.Numerics;

namespace JaysAi.Finale.Features
{
    public class Crosshair
    {
        private readonly SettingsManager<AppSettings> _settings;
        private readonly IOverlayRenderer _overlay;

        public Crosshair(SettingsManager<AppSettings> settings, IOverlayRenderer overlay)
        {
            _settings = settings;
            _overlay = overlay;
        }

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            var crosshairEnabled = _settings.Current.Crosshair.Enabled;
            if (!crosshairEnabled) return;

            float size = _settings.Current.Crosshair.Size;
            float thickness = _settings.Current.Crosshair.Thickness;
            var color = _settings.Current.Crosshair.Color;

            var paint = new SKPaint
            {
                Color = new SKColor(color.R, color.G, color.B, color.A),
                StrokeWidth = thickness,
                IsAntialias = true
            };

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            float halfSize = size / 2f;

            // Draw horizontal line
            canvas.DrawLine(centerX - halfSize, centerY, centerX + halfSize, centerY, paint);
            // Draw vertical line
            canvas.DrawLine(centerX, centerY - halfSize, centerX, centerY + halfSize, paint);
        }
    }
}
