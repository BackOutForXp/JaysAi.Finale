// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Security
{
    public static class StealthMode
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out _);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private static bool _isActive = false;
        private static Timer? _windowScanTimer;

        public static bool IsActive => _isActive;

        public static void Activate()
        {
            if (_isActive)
                return;

            _isActive = true;

            try
            {
                HideConsole();
                ConcealProcessMetadata();
                StartWindowScan();
                Logger.Log("Stealth Mode Activated.");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to activate Stealth Mode.", ex);
            }
        }

        public static void Deactivate()
        {
            if (!_isActive)
                return;

            _isActive = false;

            try
            {
                ShowConsole();
                _windowScanTimer?.Dispose();
                Logger.Log("Stealth Mode Deactivated.");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to deactivate Stealth Mode.", ex);
            }
        }

        private static void HideConsole()
        {
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
                ShowWindow(handle, SW_HIDE);
        }

        private static void ShowConsole()
        {
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
                ShowWindow(handle, SW_SHOW);
        }

        private static void ConcealProcessMetadata()
        {
            try
            {
                var current = Process.GetCurrentProcess();
                current.PriorityClass = ProcessPriorityClass.BelowNormal;
                current.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            catch (Exception ex)
            {
                Logger.Warn("Concealing process metadata failed.", ex);
            }
        }

        private static void StartWindowScan()
        {
            _windowScanTimer = new System.Timers.Timer(_ =>
            {
                // Future enhancement: scan for suspicious windows or analysis tools
                // Currently placeholder for stealth monitoring routine
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        }
    }
}
