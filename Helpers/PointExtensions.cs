//heavenly v3.0 – Modern Point Extensions
using System;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class PointExtensions
    {
        public static Point Clamp(this Point point, double minX, double maxX, double minY, double maxY)
        {
            double x = Math.Max(minX, Math.Min(maxX, point.X));
            double y = Math.Max(minY, Math.Min(maxY, point.Y));
            return new Point(x, y);
        }

        public static Vector ToVector(this Point point)
        {
            return new Vector(point.X, point.Y);
        }

        public static double DistanceTo(this Point point, Point other)
        {
            double dx = point.X - other.X;
            double dy = point.Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Point Offset(this Point point, double dx, double dy)
        {
            return new Point(point.X + dx, point.Y + dy);
        }

        public static bool IsNear(this Point point, Point other, double threshold)
        {
            return point.DistanceTo(other) <= threshold;
        }
    }
}
