// Neural v3.1
using JaysAi.Finale.Data;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class EspDrawLayer : IOverlayRenderer
    {
        public bool IsActive => ESP.IsEnabled;

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!ESP.IsEnabled)
                return;

            List<ESPObject> objects = ESP.GetObjects();
            if (objects == null || objects.Count == 0)
                return;

            foreach (var obj in objects)
            {
                if (!obj.IsVisible || obj.ScreenBox == null)
                    continue;

                var box = obj.ScreenBox.Value;

                using var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2f,
                    Color = SKColors.Red,
                    IsAntialias = true
                };

                canvas.DrawRect(box, paint);

                // Optional: Add name label
                if (!string.IsNullOrWhiteSpace(obj.Name))
                {
                    using var textPaint = new SKPaint
                    {
                        Color = SKColors.White,
                        TextSize = 16,
                        IsAntialias = true
                    };

                    canvas.DrawText(obj.Name, box.Left, box.Top - 5, textPaint);
                }
            }
        }
    }
}
