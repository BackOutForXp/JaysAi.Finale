// File: Visuals/OverlayWindow.cs
using System.Windows;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Visuals
{
    public class OverlayWindow
    {
        private readonly ESPOverlay _espOverlay;
        private readonly CrosshairOverlay _crosshairOverlay;

        public OverlayWindow(SettingsManager<AppSettings> settingsManager)
        {
            _espOverlay = new ESPOverlay(settingsManager);
            _crosshairOverlay = new CrosshairOverlay(settingsManager);
        }

        public void Show()
        {
            _espOverlay.Show();
            _crosshairOverlay.Show();
        }

        public void Hide()
        {
            _espOverlay.Hide();
            _crosshairOverlay.Hide();
        }

        public void RefreshCrosshair(AppSettings settings)
        {
            _crosshairOverlay.UpdateSettings(settings);
        }

        public ESPOverlay GetESP() => _espOverlay;
        public CrosshairOverlay GetCrosshair() => _crosshairOverlay;
    }
}
