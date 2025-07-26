// neural v3.0
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JaysAi.Finale.Input
{
    public static class MouseMover
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

        public static void MoveTo(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MoveBy(int deltaX, int deltaY)
        {
            if (GetCursorPos(out POINT currentPos))
            {
                SetCursorPos(currentPos.X + deltaX, currentPos.Y + deltaY);
            }
        }

        public static Point GetPosition()
        {
            return GetCursorPos(out POINT point)
                ? new Point(point.X, point.Y)
                : Point.Empty;
        }
    }
}
