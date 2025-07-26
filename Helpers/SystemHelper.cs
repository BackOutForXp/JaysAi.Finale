// Neural v3.0 — SystemHelper.cs
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace JaysAi.Finale.Helpers
{
    public static class SystemHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static string GetActiveWindowTitle()
        {
            var handle = GetForegroundWindow();
            if (handle == IntPtr.Zero) return null;

            const int maxLength = 256;
            var buffer = new char[maxLength];
            _ = GetWindowText(handle, buffer, maxLength);
            return new string(buffer).TrimEnd('\0');
        }

        public static string GetActiveProcessName()
        {
            var handle = GetForegroundWindow();
            if (handle == IntPtr.Zero) return null;

            _ = GetWindowThreadProcessId(handle, out uint pid);
            try
            {
                var process = Process.GetProcessById((int)pid);
                return process.ProcessName;
            }
            catch
            {
                return null;
            }
        }

        public static void RestartApplication()
        {
            string exePath = Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty;
            if (!string.IsNullOrEmpty(exePath))
            {
                Process.Start(exePath);
                Application.Current.Shutdown();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, char[] lpString, int nMaxCount);
    }
}
