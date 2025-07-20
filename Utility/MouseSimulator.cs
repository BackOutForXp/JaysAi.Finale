//monarch v1.0
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Utility
{
    public static class MouseSimulator
    {
        [Flags]
        private enum MouseEventFlags : uint
        {
            MOVE = 0x0001,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            ABSOLUTE = 0x8000
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, uint dwData, nint dwExtraInfo);

        public static void Move(int deltaX, int deltaY)
        {
            mouse_event(MouseEventFlags.MOVE, deltaX, deltaY, 0, nint.Zero);
        }

        public static void LeftClick()
        {
            mouse_event(MouseEventFlags.LEFTDOWN, 0, 0, 0, nint.Zero);
            Thread.Sleep(10);
            mouse_event(MouseEventFlags.LEFTUP, 0, 0, 0, nint.Zero);
        }

        public static void RightClick()
        {
            mouse_event(MouseEventFlags.RIGHTDOWN, 0, 0, 0, nint.Zero);
            Thread.Sleep(10);
            mouse_event(MouseEventFlags.RIGHTUP, 0, 0, 0, nint.Zero);
        }
    }
}
