// File: Visuals/OverlayController.cs
using JaysAi.Finale.Core;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using System;
using System.Windows;
using System.Windows.Threading;

namespace JaysAi.Finale.Visuals
{
    public class OverlayController
    {
        private readonly OverlayWindow _overlayWindow;
        private readonly ESPOverlay _espOverlay;
        private readonly CrosshairOverlay _crosshairOverlay;
        private readonly DispatcherTimer _updateTimer;

        public OverlayController(AppSettings settings)
        {
            _espOverlay = new ESPOverlay(settings);
            _crosshairOverlay = new CrosshairOverlay(settings);
            _overlayWindow = new OverlayWindow(_espOverlay, _crosshairOverlay);

            _updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(33) // ~30 FPS for performance
            };
            _updateTimer.Tick += (s, e) => Update();
        }

        public void Start()
        {
            _overlayWindow.Show();
            _updateTimer.Start();
        }

        public void Stop()
        {
            _updateTimer.Stop();
            _overlayWindow.Hide();
        }

        public void Update()
        {
            // Optionally implement dynamic visibility or game-window resizing here
            _overlayWindow.RefreshCrosshair(); // Ensure visuals are up to date
        }

        public void RefreshSettings(AppSettings newSettings)
        {
            _espOverlay.ApplySettings(newSettings);
            _crosshairOverlay.ApplySettings(newSettings);
        }
    }
}
