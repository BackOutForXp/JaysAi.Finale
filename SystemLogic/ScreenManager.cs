//monarch v2.1 – Screen Dimension Utility
using System;
using System.Windows;

namespace JaysAi.Finale.SystemLogic
{
    public static class ScreenManager
    {
        private static int _width = 1920;
        private static int _height = 1080;

        public static void SetResolution(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public static int GetWidth() => _width;
        public static int GetHeight() => _height;

        public static PointF GetScreenCenter()
        {
            return new PointF(_width / 2f, _height / 2f);
        }

        public static Point ConvertToPoint(PointF pointF)
        {
            return new Point((int)pointF.X, (int)pointF.Y);
        }

        public static PointF ConvertToPointF(Point point)
        {
            return new PointF(point.X, point.Y);
        }
    }

    public struct PointF
    {
        public float X;
        public float Y;

        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static PointF operator -(PointF a, PointF b)
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }
    }
}
