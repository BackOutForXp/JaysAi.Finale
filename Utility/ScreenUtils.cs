//monarch v2.1
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class ScreenUtils
    {
        public static int ScreenWidth { get; set; } = 1920;
        public static int ScreenHeight { get; set; } = 1080;

        public static Vector2 ScreenCenter => new(ScreenWidth / 2f, ScreenHeight / 2f);

        public static Vector2 Normalize(Vector2 position)
        {
            return new Vector2(position.X / ScreenWidth, position.Y / ScreenHeight);
        }

        public static Vector2 Denormalize(Vector2 normalized)
        {
            return new Vector2(normalized.X * ScreenWidth, normalized.Y * ScreenHeight);
        }

        public static float DistanceFromCenter(Vector2 point)
        {
            return Vector2.Distance(point, ScreenCenter);
        }

        public static bool IsOnScreen(Vector2 point)
        {
            return point.X >= 0 && point.X <= ScreenWidth && point.Y >= 0 && point.Y <= ScreenHeight;
        }

        public static void SetResolution(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }
    }
}
