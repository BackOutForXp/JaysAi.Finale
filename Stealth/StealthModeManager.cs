// Neural v3.1 — StealthModeManager.cs
using System;
using System.Timers;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Stealth
{
    public class StealthModeManager
    {
        private readonly Timer _checkTimer;
        private readonly IntPtr _overlayWindowHandle;

        public bool IsActive { get; private set; }

        public StealthModeManager(IntPtr overlayWindowHandle)
        {
            _overlayWindowHandle = overlayWindowHandle;

            _checkTimer = new Timer(500); // Check every 500ms
            _checkTimer.Elapsed += OnTimerElapsed;
            _checkTimer.AutoReset = true;
        }

        public void Enable()
        {
            IsActive = true;
            _checkTimer.Start();
        }

        public void Disable()
        {
            IsActive = false;
            ScreenshotInterceptor.ShowOverlayWindow(_overlayWindowHandle);
            _checkTimer.Stop();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (!IsActive) return;

            if (ScreenshotInterceptor.IsScreenCaptureActive())
                ScreenshotInterceptor.HideOverlayWindow(_overlayWindowHandle);
            else
                ScreenshotInterceptor.ShowOverlayWindow(_overlayWindowHandle);
        }
    }
}
