//monarch v2.1 – Live Visual Debug Console
using SkiaSharp;
using System;
using System.Linq;

namespace JaysAi.Finale.Visuals
{
    public class OverlayConsole
    {
        private readonly SKPaint _paint;
        private readonly float _lineHeight = 18f;
        private readonly int _maxLines = 10;

        public OverlayConsole()
        {
            _paint = new SKPaint
            {
                Color = SKColors.Lime,
                TextSize = 16,
                Typeface = SKTypeface.FromFamilyName("Consolas", SKFontStyle.Bold)
            };
        }

        public void Draw(SKCanvas canvas, float x, float y)
        {
            var entries = AI.RuntimeBehaviorLog.GetEntries().Reverse().Take(_maxLines).ToArray();
            for (int i = 0; i < entries.Length; i++)
            {
                canvas.DrawText(entries[i], x, y + i * _lineHeight, _paint);
            }
        }
    }
}
