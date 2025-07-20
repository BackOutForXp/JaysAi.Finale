//monarch v1.9
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Utility
{
    /// <summary>
    /// Contains native Win32 API methods used internally for window and process control.
    /// </summary>
    internal static class NativeMethods
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetForegroundWindow(nint hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(nint hWnd, out uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern nint OpenProcess(uint dwDesiredAccess, bool inheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(nint hObject);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint GetWindow(nint hWnd, uint uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool IsWindowVisible(nint hWnd);
    }
}
