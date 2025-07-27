// Neural v3.1 — ScreenHelper.cs
using System;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class ScreenHelper
    {
        public static int ScreenWidth => (int)SystemParameters.PrimaryScreenWidth;
        public static int ScreenHeight => (int)SystemParameters.PrimaryScreenHeight;
        public static Point ScreenCenter => new(ScreenWidth / 2.0, ScreenHeight / 2.0);

        public static bool IsOnScreen(Point point)
        {
            return point.X >= 0 && point.X <= ScreenWidth &&
                   point.Y >= 0 && point.Y <= ScreenHeight;
        }

        public static bool IsPointVisible(Point? point)
        {
            if (!point.HasValue) return false;
            return IsOnScreen(point.Value);
        }

        public static bool IsRectVisible(Rect rect)
        {
            return rect.Right > 0 && rect.Left < ScreenWidth &&
                   rect.Bottom > 0 && rect.Top < ScreenHeight;
        }

        public static Rect ClampToScreen(Rect rect)
        {
            double x = Math.Max(0, rect.X);
            double y = Math.Max(0, rect.Y);
            double width = Math.Min(ScreenWidth - x, rect.Width);
            double height = Math.Min(ScreenHeight - y, rect.Height);

            return new Rect(x, y, width, height);
        }
    }
}
