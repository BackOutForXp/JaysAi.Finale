// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        /// <summary>
        /// Gets the bounds of a window given its handle.
        /// </summary>
        public static Rect GetWindowBounds(IntPtr hWnd)
        {
            if (!GetWindowRect(hWnd, out RECT rect))
                return Rect.Empty;

            return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        /// <summary>
        /// Finds a window by its title.
        /// </summary>
        public static IntPtr FindWindowByTitle(string windowTitle)
        {
            return FindWindow(null, windowTitle);
        }

        /// <summary>
        /// Attempts to focus the target window.
        /// </summary>
        public static bool BringToFront(IntPtr hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Returns the title of the currently focused window.
        /// </summary>
        public static string GetActiveWindowTitle()
        {
            IntPtr hwnd = GetForegroundWindow();
            StringBuilder sb = new(256);
            _ = GetWindowText(hwnd, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>
        /// Returns true if the given process has a visible window.
        /// </summary>
        public static bool IsWindowVisibleForProcess(Process process)
        {
            try
            {
                return IsWindowVisible(process.MainWindowHandle);
            }
            catch


