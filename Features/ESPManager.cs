// neural v3.0
using System.Collections.Generic;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Features
{
    public static class ESPManager
    {
        private static bool _isInitialized;

        public static void Initialize()
        {
            if (_isInitialized) return;

            ESP.Initialize();
            ESP.SetEnabled(UserSettings.Current.EnableESP);
            _isInitialized = true;
        }

        public static void Update(List<Enemy> enemyList)
        {
            if (!UserSettings.Current.EnableESP) return;
            ESP.UpdateObjects(enemyList);
        }

        public static void ToggleESP(bool enabled)
        {
            ESP.SetEnabled(enabled);
            Logger.Log($"ESP toggled {(enabled ? "on" : "off")}");
        }

        public static void Disable()
        {
            ESP.SetEnabled(false);
            ESP.Clear();
        }
    }
}
