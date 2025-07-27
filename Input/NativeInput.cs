// Neural v3.1
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class NativeInput
    {
        [Flags]
        private enum MouseEventFlags : uint
        {
            MOVE = 0x0001,
            ABSOLUTE = 0x8000,
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        public static void MoveMouseBy(int dx, int dy)
        {
            mouse_event(MouseEventFlags.MOVE, dx, dy, 0, UIntPtr.Zero);
        }

        public static void MoveMouseAbsolute(int x, int y, int screenWidth, int screenHeight)
        {
            int absX = (int)(x * 65535.0f / screenWidth);
            int absY = (int)(y * 65535.0f / screenHeight);
            mouse_event(MouseEventFlags.MOVE | MouseEventFlags.ABSOLUTE, absX, absY, 0, UIntPtr.Zero);
        }
    }
}
