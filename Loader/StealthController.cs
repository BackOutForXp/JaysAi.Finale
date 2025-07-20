//monarch v2.0
using System;
using System.Diagnostics;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Loader
{
    public static class StealthController
    {
        public static bool IsStealthModeActive { get; private set; } = false;

        public static void EnableStealth(IntPtr hwnd)
        {
            NativeMethods.ShowWindow(hwnd, 0); // Hide window
            IsStealthModeActive = true;
            LogManager.Log("Stealth mode ENABLED.");
        }

        public static void DisableStealth(IntPtr hwnd)
        {
            NativeMethods.ShowWindow(hwnd, 1); // Show window (SW_SHOWNORMAL)
            IsStealthModeActive = false;
            LogManager.Log("Stealth mode DISABLED.");
        }

        public static void ToggleStealth(IntPtr hwnd)
        {
            if (IsStealthModeActive)
                DisableStealth(hwnd);
            else
                EnableStealth(hwnd);
        }

        public static void HideLoaderFromTaskbar(Process loaderProcess)
        {
            try
            {
                var handle = loaderProcess.MainWindowHandle;
                NativeMethods.ShowWindow(handle, 0); // Hide main window
                NativeMethods.SetWindowLong(handle, -20, 0x80); // WS_EX_TOOLWINDOW
            }
            catch (Exception ex)
            {
                LogManager.Log($"Stealth error: {ex.Message}");
            }
        }
    }
}
