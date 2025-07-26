// neural v3.0
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class MouseButtonHelper
    {
        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(
            MouseEventFlags dwFlags,
            uint dx,
            uint dy,
            uint dwData,
            IntPtr dwExtraInfo
        );

        public static void ClickLeft()
        {
            mouse_event(MouseEventFlags.LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.LEFTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void ClickRight()
        {
            mouse_event(MouseEventFlags.RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.RIGHTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void ClickMiddle()
        {
            mouse_event(MouseEventFlags.MIDDLEDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventFlags.MIDDLEUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void HoldLeft()
        {
            mouse_event(MouseEventFlags.LEFTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void ReleaseLeft()
        {
            mouse_event(MouseEventFlags.LEFTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void HoldRight()
        {
            mouse_event(MouseEventFlags.RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void ReleaseRight()
        {
            mouse_event(MouseEventFlags.RIGHTUP, 0, 0, 0, IntPtr.Zero);
        }
    }
}
