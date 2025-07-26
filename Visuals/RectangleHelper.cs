// Heavenly-tier v3.0
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class RectangleHelper
    {
        public static SKRect Create(float x, float y, float width, float height)
        {
            return new SKRect(x, y, x + width, y + height);
        }

        public static SKRect Inflate(SKRect rect, float amount)
        {
            return new SKRect(
                rect.Left - amount,
                rect.Top - amount,
                rect.Right + amount,
                rect.Bottom + amount
            );
        }

        public static SKRect ClampToScreen(SKRect rect, float screenWidth, float screenHeight)
        {
            float left = Clamp(rect.Left, 0, screenWidth);
            float top = Clamp(rect.Top, 0, screenHeight);
            float right = Clamp(rect.Right, 0, screenWidth);
            float bottom = Clamp(rect.Bottom, 0, screenHeight);

            return new SKRect(left, top, right, bottom);
        }

        public static bool IsInside(SKPoint point, SKRect rect)
        {
            return rect.Contains(point);
        }

        public static SKPoint GetCenter(SKRect rect)
        {
            return new SKPoint(
                rect.Left + rect.Width / 2f,
                rect.Top + rect.Height / 2f
            );
        }

        private static float Clamp(float value, float min, float max)
        {
            return value < min ? min : (value > max ? max : value);
        }
    }
}
