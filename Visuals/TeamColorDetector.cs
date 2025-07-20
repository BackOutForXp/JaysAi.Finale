//monarch v2.1
using JaysAi.AI;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class TeamColorDetector
    {
        // Customize these based on your game’s UI enemy/friendly HUD tint
        private static readonly SKColor EnemyColor = new SKColor(255, 0, 0);   // red
        private static readonly SKColor FriendlyColor = new SKColor(0, 255, 0); // green

        private const int ColorTolerance = 40;

        public static bool IsFriendly(EntityData entity)
        {
            if (entity == null) return false;

            SKBitmap frame = FrameCapture.GetCurrentFrame();
            if (frame == null) return false;

            int x = (int)entity.ScreenPosition.X;
            int y = (int)entity.ScreenPosition.Y;

            if (!frame.Contains(x, y)) return false;

            SKColor pixelColor = frame.GetPixel(x, y);

            return IsColorMatch(pixelColor, FriendlyColor);
        }

        public static bool IsEnemy(EntityData entity)
        {
            if (entity == null) return false;

            SKBitmap frame = FrameCapture.GetCurrentFrame();
            if (frame == null) return false;

            int x = (int)entity.ScreenPosition.X;
            int y = (int)entity.ScreenPosition.Y;

            if (!frame.Contains(x, y)) return false;

            SKColor pixelColor = frame.GetPixel(x, y);

            return IsColorMatch(pixelColor, EnemyColor);
        }

        private static bool IsColorMatch(SKColor a, SKColor b)
        {
            return Math.Abs(a.Red - b.Red) < ColorTolerance &&
                   Math.Abs(a.Green - b.Green) < ColorTolerance &&
                   Math.Abs(a.Blue - b.Blue) < ColorTolerance;
        }

        private static bool Contains(this SKBitmap bitmap, int x, int y)
        {
            return x >= 0 && y >= 0 && x < bitmap.Width && y < bitmap.Height;
        }
    }
}
