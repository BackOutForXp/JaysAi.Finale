// Neural v3.1 — OverlayHotkeyManager.cs
using JaysAi.Finale.Settings;
using JaysAi.Finale.Input;
using System.Windows.Input;

namespace JaysAi.Finale.Overlay
{
    public class OverlayHotkeyManager
    {
        public void RegisterHotkeys()
        {
            HotkeyWatcher.Register(Key.F1, ToggleEsp);
            HotkeyWatcher.Register(Key.F2, ToggleFovCircle);
            HotkeyWatcher.Register(Key.F3, ToggleDebugOverlay);
            HotkeyWatcher.Register(Key.F4, ToggleFpsCounter);
        }

        private void ToggleEsp()
        {
            bool current = UserSettings.Instance.Get("EspEnabled", true);
            UserSettings.Instance.Set("EspEnabled", !current);
        }

        private void ToggleFovCircle()
        {
            bool current = UserSettings.Instance.Get("FovCircleEnabled", true);
            UserSettings.Instance.Set("FovCircleEnabled", !current);
        }

        private void ToggleDebugOverlay()
        {
            bool current = UserSettings.Instance.Get("DebugOverlayEnabled", false);
            UserSettings.Instance.Set("DebugOverlayEnabled", !current);
        }

        private void ToggleFpsCounter()
        {
            bool current = UserSettings.Instance.Get("ShowFpsCounter", false);
            UserSettings.Instance.Set("ShowFpsCounter", !current);
        }
    }
}
