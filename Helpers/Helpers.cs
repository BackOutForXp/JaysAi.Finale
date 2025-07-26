// Neural v3.0 — CoordinateConverter.cs
using System.Numerics;
using System.Windows;
using OpenCvSharp;

namespace JaysAi.Finale.Helpers
{
    public static class CoordinateConverter
    {
        /// <summary>
        /// Converts OpenCvSharp Point to System.Windows Point.
        /// </summary>
        public static Point ToWpfPoint(Point2f point) => new(point.X, point.Y);

        /// <summary>
        /// Converts System.Windows Point to OpenCvSharp Point2f.
        /// </summary>
        public static Point2f ToCvPoint(Point point) => new((float)point.X, (float)point.Y);

        /// <summary>
        /// Converts Vector2 to OpenCvSharp Point2f.
        /// </summary>
        public static Point2f ToCvPoint(Vector2 vector) => new(vector.X, vector.Y);

        /// <summary>
        /// Converts Vector2 to System.Windows Point.
        /// </summary>
        public static Point ToWpfPoint(Vector2 vector) => new(vector.X, vector.Y);

        /// <summary>
        /// Converts OpenCvSharp Point2f to Vector2.
        /// </summary>
        public static Vector2 ToVector(Point2f point) => new(point.X, point.Y);

        /// <summary>
        /// Converts System.Windows Point to Vector2.
        /// </summary>
        public static Vector2 ToVector(Point point) => new((float)point.X, (float)point.Y);
    }
}
