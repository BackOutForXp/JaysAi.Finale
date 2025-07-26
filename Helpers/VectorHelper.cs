// Neural v3.0 — VectorHelper.cs
using System;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class VectorHelper
    {
        public static double GetDistance(Point p1, Point p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Point NormalizeVector(Point vector)
        {
            double magnitude = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return magnitude > 0
                ? new Point(vector.X / magnitude, vector.Y / magnitude)
                : new Point(0, 0);
        }

        public static Point ClampToBounds(Point point, double width, double height)
        {
            double clampedX = Math.Max(0, Math.Min(point.X, width));
            double clampedY = Math.Max(0, Math.Min(point.Y, height));
            return new Point(clampedX, clampedY);
        }

        public static Point Add(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point Subtract(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static double GetAngleBetweenPoints(Point from, Point to)
        {
            double dx = to.X - from.X;
            double dy = to.Y - from.Y;
            return Math.Atan2(dy, dx) * (180.0 / Math.PI);
        }

        public static Point Multiply(Point point, double scalar)
        {
            return new Point(point.X * scalar, point.Y * scalar);
        }

        public static Point RotateAround(Point point, Point center, double angleDegrees)
        {
            double angleRadians = angleDegrees * (Math.PI / 180);
            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            double dx = point.X - center.X;
            double dy = point.Y - center.Y;

            double rotatedX = center.X + dx * cos - dy * sin;
            double rotatedY = center.Y + dx * sin + dy * cos;

            return new Point(rotatedX, rotatedY);
        }
    }
}
