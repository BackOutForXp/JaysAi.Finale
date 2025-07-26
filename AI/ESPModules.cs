// neural v3.0
using JaysAi.Finale.Features;
using JaysAi.Finale.Visuals;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class ESPModules
    {
        private static readonly Dictionary<string, bool> _modules = new()
        {
            { "BoundingBox", true },
            { "Skeleton", true },
            { "HealthBar", true },
            { "SnapLine", true },
            { "NameTag", true },
            { "Distance", false }
        };

        public static bool IsEnabled(string moduleName)
        {
            return _modules.TryGetValue(moduleName, out bool enabled) && enabled;
        }

        public static void SetEnabled(string moduleName, bool enabled)
        {
            if (_modules.ContainsKey(moduleName))
                _modules[moduleName] = enabled;
        }

        public static Dictionary<string, bool> GetAllModules()
        {
            return new Dictionary<string, bool>(_modules);
        }

        public static void ToggleModule(string moduleName)
        {
            if (_modules.ContainsKey(moduleName))
                _modules[moduleName] = !_modules[moduleName];
        }

        public static void ResetDefaults()
        {
            _modules["BoundingBox"] = true;
            _modules["Skeleton"] = true;
            _modules["HealthBar"] = true;
            _modules["SnapLine"] = true;
            _modules["NameTag"] = true;
            _modules["Distance"] = false;
        }
    }
}
