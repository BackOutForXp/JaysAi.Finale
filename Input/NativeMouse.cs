// neural v3.0
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JaysAi.Finale.Input
{
    public static class NativeMouse
    {
        [Flags]
        private enum MouseEventFlags : uint
        {
            MOVE = 0x0001,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            WHEEL = 0x0800,
            ABSOLUTE = 0x8000
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static void LeftClick()
        {
            mouse_event(MouseEventFlags.LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.LEFTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void RightClick()
        {
            mouse_event(MouseEventFlags.RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.RIGHTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void MiddleClick()
        {
            mouse_event(MouseEventFlags.MIDDLEDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.MIDDLEUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void Scroll(int amount)
        {
            mouse_event(MouseEventFlags.WHEEL, 0, 0, amount, IntPtr.Zero);
        }

        public static Point GetCursorPosition()
        {
            return GetCursorPos(out POINT point)
                ? new Point(point.X, point.Y)
                : Point.Empty;
        }
    }
}
