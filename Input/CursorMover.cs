//heavenly v3.0 – Mouse Movement Bridge
using System.Runtime.InteropServices;
using System.Windows;

namespace JaysAi.Finale.Input
{
    public static class CursorMover
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static Point GetCurrentCursorPosition()
        {
            GetCursorPos(out var point);
            return new Point(point.X, point.Y);
        }

        public static void MoveCursorBy(double deltaX, double deltaY)
        {
            var current = GetCurrentCursorPosition();
            int targetX = (int)(current.X + deltaX);
            int targetY = (int)(current.Y + deltaY);
            SetCursorPos(targetX, targetY);
        }

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(Point point)
        {
            SetCursorPos((int)point.X, (int)point.Y);
        }
    }
}
