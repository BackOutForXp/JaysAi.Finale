// Neural v3.1 — ZoomHotkeyHandler.cs
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using System.Windows.Input;

namespace JaysAi.Finale.Features
{
    public class ZoomHotkeyHandler
    {
        private readonly ZoomAssist _zoomAssist;
        private readonly KeybindWatcher _keybindWatcher;

        public ZoomHotkeyHandler(ZoomAssist zoomAssist, KeybindWatcher keybindWatcher)
        {
            _zoomAssist = zoomAssist;
            _keybindWatcher = keybindWatcher;
        }

        public void Update()
        {
            if (!UserSettings.Current.ZoomEnabled)
                return;

            var key = UserSettings.Current.ZoomHotkey;

            if (_keybindWatcher.IsKeyPressed(key))
            {
                _zoomAssist.StartZoom(UserSettings.Current.ZoomFov);
            }
            else
            {
                _zoomAssist.ResetZoom();
            }

            _zoomAssist.Update();
        }
    }
}
