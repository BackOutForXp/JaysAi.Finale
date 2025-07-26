// Neural v3.0 — TargetIcon.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class TargetIcon
    {
        public bool IsVisible { get; set; } = true;
        public float Size { get; set; } = 10f;
        public SKColor Color { get; set; } = SKColors.Red;
        public SKPoint Position { get; set; }

        /// <summary>
        /// Draws a target marker icon (circle) at a given screen position.
        /// </summary>
        public void Draw(SKCanvas canvas)
        {
            if (!IsVisible || canvas == null)
                return;

            using var paint = new SKPaint
            {
                Color = Color,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            canvas.DrawCircle(Position, Size, paint);
        }

        /// <summary>
        /// Updates icon location and appearance.
        /// </summary>
        public void Set(SKPoint position, float size, SKColor color)
        {
            Position = position;
            Size = size;
            Color = color;
        }

        /// <summary>
        /// Hides the icon immediately.
        /// </summary>
        public void Hide() => IsVisible = false;

        /// <summary>
        /// Shows the icon with the current state.
        /// </summary>
        public void Show() => IsVisible = true;
    }
}
