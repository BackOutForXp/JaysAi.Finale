// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace JaysAi.Finale.Helpers
{
    public static class WindowUtilities
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        public static IntPtr FindGameWindow(string partialTitle)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    if (hWnd == IntPtr.Zero || !IsWindowVisible(hWnd)) continue;

                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) continue;

                    StringBuilder sb = new(length + 1);
                    GetWindowText(hWnd, sb, sb.Capacity);
                    string title = sb.ToString();

                    if (title.Contains(partialTitle, StringComparison.OrdinalIgnoreCase))
                        return hWnd;
                }
                catch
                {
                    continue;
                }
            }

            return IntPtr.Zero;
        }

        public static bool TryGetWindowBounds(IntPtr hWnd, out (int X, int Y, int Width, int Height) bounds)
        {
            if (GetWindowRect(hWnd, out RECT rect))
            {
                bounds = (rect.Left, rect.Top, rect.Width, rect.Height);
                return true;
            }

            bounds = default;
            return false;
        }

        public static bool IsWindowInFocus(IntPtr hWnd)
        {
            return GetForegroundWindow() == hWnd;
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            if (length == 0) return string.Empty;

            StringBuilder sb = new(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }
    }
}
