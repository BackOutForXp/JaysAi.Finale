// Neural v3.1 — ScreenshotInterceptor.cs
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Stealth
{
    public static class ScreenshotInterceptor
    {
        private static readonly string[] SuspiciousProcesses = new[]
        {
            "screenshot", "snippingtool", "gyazo", "sharex", "lightshot", "nimbus", "screenrec", "flameshot"
        };

        public static bool IsScreenCaptureActive()
        {
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    string name = process.ProcessName.ToLower();
                    foreach (var s in SuspiciousProcesses)
                    {
                        if (name.Contains(s))
                            return true;
                    }
                }
                catch
                {
                    // Some processes may not be accessible (AccessDenied), skip
                }
            }
            return false;
        }

        public static void HideOverlayWindow(IntPtr windowHandle)
        {
            ShowWindow(windowHandle, SW_HIDE);
        }

        public static void ShowOverlayWindow(IntPtr windowHandle)
        {
            ShowWindow(windowHandle, SW_SHOW);
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
