// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Features
{
    public class ESPModule : IModule
    {
        public string Name => "ESP";

        public bool IsEnabled => UserSettings.Current.EnableESP;

        public void Enable()
        {
            ESP.SetEnabled(true);
        }

        public void Disable()
        {
            ESP.SetEnabled(false);
        }

        public void Update()
        {
            if (!IsEnabled) return;

            var enemies = EntityCache.GetVisibleEnemies();
            ESP.UpdateObjects(enemies);
        }

        public void OnGUI()
        {
            if (!IsEnabled) return;

            ESPDrawer.RenderAll();
        }
    }
}
