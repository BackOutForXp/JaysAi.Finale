// Neural v3.1 — EspDrawer.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Features;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class EspDrawer
    {
        private readonly ESPModule _espModule;

        public EspDrawer(ESPModule espModule)
        {
            _espModule = espModule;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!_espModule.IsEnabled || _espModule.Targets == null) return;

            using var paint = new SKPaint
            {
                IsAntialias = UserSettings.Current.EspAntiAlias,
                StrokeWidth = UserSettings.Current.EspBoxThickness,
                Style = SKPaintStyle.Stroke,
                Color = UserSettings.Current.EspBoxColor
            };

            foreach (var target in _espModule.Targets)
            {
                if (!target.IsVisible || target.ScreenBox == null)
                    continue;

                var box = target.ScreenBox.Value;

                // Optional box fill
                if (UserSettings.Current.EspBoxFillEnabled)
                {
                    using var fillPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = UserSettings.Current.EspBoxFillColor
                    };
                    canvas.DrawRect(box.X, box.Y, box.Width, box.Height, fillPaint);
                }

                // Draw ESP outline box
                canvas.DrawRect(box.X, box.Y, box.Width, box.Height, paint);
            }
        }
    }
}
