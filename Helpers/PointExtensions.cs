// File: PointExtensions.cs
using System;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
