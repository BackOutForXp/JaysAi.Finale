using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Features;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using System.Collections.Generic;

namespace JaysAi.Finale.Loader
{
    public static class ModuleRegistry
    {
        public static List<IModule> GetModules(AppSettings settings)
        {
            var modules = new List<IModule>();

            if (settings.EnableESP)
                modules.Add(new ESP(settings, new EnemyScanner(settings)));

            if (settings.EnableAimAssist)
                modules.Add(new AimAssist(settings, new EnemyScanner(settings)));

            if (settings.EnableStickAssist)
                modules.Add(new StickAssist(settings)); // Placeholder if implemented

            // Add overlay only if you want overlay lifecycle as module
            modules.Add(new OverlayModule(settings));

            return modules;
        }
    }
}
