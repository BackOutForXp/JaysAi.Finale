// neural v3.0
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Helpers
{
    public static class Win32
    {
        // Get handle to foreground window (used for focus check)
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        // Get the process ID from a window handle
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Used to determine if a key is currently being pressed
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        // Used to simulate key input
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // Used to simulate mouse movement/clicks
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        public const int KEYEVENTF_KEYUP = 0x0002;

        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        public const int VK_LBUTTON = 0x01;
        public const int VK_RBUTTON = 0x02;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_SPACE = 0x20;
        public const int VK_END = 0x23;

        /// <summary>
        /// Determines if a specific key is currently being pressed down.
        /// </summary>
        public static bool IsKeyDown(int virtualKey)
        {
            return (GetAsyncKeyState(virtualKey) & 0x8000) != 0;
        }

        /// <summary>
        /// Simulate key press and release
        /// </summary>
        public static void SendKey(byte keyCode)
        {
            keybd_event(keyCode, 0, 0, 0);
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// Simulate a mouse click at the current position
        /// </summary>
        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// Simulate a right mouse click at the current position
        /// </summary>
        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// Move mouse to absolute screen coordinates
        /// </summary>
        public static void MoveCursor(int x, int y)
        {
            SetCursorPos(x, y);
        }
    }
}
