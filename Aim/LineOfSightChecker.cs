//monarch v2.1
using JaysAi.AI;
using JaysAi.Visuals;
using SkiaSharp;

namespace JaysAi.Finale.Aim
{
    public static class LineOfSightChecker
    {
        public static bool HasClearView(EntityData target)
        {
            if (target == null) return false;

            int scanSize = 3;
            int startX = (int)target.ScreenPosition.X - scanSize / 2;
            int startY = (int)target.ScreenPosition.Y - scanSize / 2;

            SKBitmap frame = FrameCapture.GetCurrentFrame();
            if (frame == null) return false;

            for (int x = startX; x < startX + scanSize; x++)
            {
                for (int y = startY; y < startY + scanSize; y++)
                {
                    if (!frame.Contains(x, y))
                        continue;

                    SKColor color = frame.GetPixel(x, y);
                    if (IsObstruction(color))
                        return false;
                }
            }

            return true;
        }

        private static bool IsObstruction(SKColor color)
        {
            // Adjust this based on your in-game wall/smoke shading
            return color.Red < 40 && color.Green < 40 && color.Blue < 40;
        }

        private static bool Contains(this SKBitmap bitmap, int x, int y)
        {
            return x >= 0 && y >= 0 && x < bitmap.Width && y < bitmap.Height;
        }
    }
}
