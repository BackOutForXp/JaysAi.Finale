//monarch v2.1 – Process checker and handle grabber
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Utility
{
    public static class GameProcessHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static Process GetTargetProcess(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                if (!process.HasExited)
                    return process;
            }
            return null;
        }

        public static IntPtr GetWindowHandle(string processName)
        {
            var process = GetTargetProcess(processName);
            return process?.MainWindowHandle ?? IntPtr.Zero;
        }

        public static bool IsTargetProcessActive(string processName)
        {
            var foregroundWindow = GetForegroundWindow();
            GetWindowThreadProcessId(foregroundWindow, out uint processId);

            var target = GetTargetProcess(processName);
            return target != null && target.Id == processId;
        }
    }
}
