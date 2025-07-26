// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Helpers
{
    public static class Win32Helper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// Checks if the current application window is in the foreground.
        /// </summary>
        public static bool IsForegroundApp()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            GetWindowThreadProcessId(foregroundWindow, out uint processId);
            return processId == (uint)Process.GetCurrentProcess().Id;
        }

        /// <summary>
        /// Returns true if the specified key is currently pressed.
        /// </summary>
        public static bool IsKeyPressed(int vKey)
        {
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }

        /// <summary>
        /// Returns the process name that currently has focus.
        /// </summary>
        public static string GetActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out uint pid);

            try
            {
                Process proc = Process.GetProcessById((int)pid);
                return proc.ProcessName;
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Returns true if the specified process is currently active.
        /// </summary>
        public static bool IsProcessInFocus(string processName)
        {
            return string.Equals(GetActiveProcessName(), processName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Logs basic window focus diagnostics for debug purposes.
        /// </summary>
        public static void LogFocusInfo()
        {
            string current = GetActiveProcessName();
            Console.WriteLine($"[Focus Check] Active Process: {current}, Expected: {Process.GetCurrentProcess().ProcessName}");
        }
    }
}
