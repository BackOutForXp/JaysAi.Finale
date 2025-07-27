// Neural v3.1 — SkiaBrushCache.cs
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public static class SkiaBrushCache
    {
        private static readonly Dictionary<uint, SKPaint> _brushCache = new();

        public static SKPaint GetBrush(SKColor color, float strokeWidth = 2f, SKPaintStyle style = SKPaintStyle.Stroke)
        {
            uint key = ComputeKey(color, strokeWidth, style);

            if (_brushCache.TryGetValue(key, out var cachedPaint))
                return cachedPaint;

            var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = strokeWidth,
                IsAntialias = true,
                Style = style
            };

            _brushCache[key] = paint;
            return paint;
        }

        private static uint ComputeKey(SKColor color, float strokeWidth, SKPaintStyle style)
        {
            unchecked
            {
                uint hash = 17;
                hash = hash * 31 + color.Red;
                hash = hash * 31 + color.Green;
                hash = hash * 31 + color.Blue;
                hash = hash * 31 + color.Alpha;
                hash = hash * 31 + (uint)(strokeWidth * 100);
                hash = hash * 31 + (uint)style;
                return hash;
            }
        }

        public static void Clear()
        {
            _brushCache.Clear();
        }
    }
}
