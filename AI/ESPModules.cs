//heavenly v3.0.0 – Dynamic ESP Module Dispatcher
using System.Collections.Generic;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.AI
{
    public static class ESPModules
    {
        private static readonly List<IEnemyProvider> _providers = new List<IEnemyProvider>();
        private static bool _initialized;

        public static void RegisterProvider(IEnemyProvider provider)
        {
            if (!_providers.Contains(provider))
                _providers.Add(provider);
        }

        public static void Initialize()
        {
            if (_initialized) return;

            // Register default enemy providers
            RegisterProvider(new DummyEnemyProvider());
            _initialized = true;
        }

        public static List<DetectedObject> GetAllEnemies()
        {
            if (!_initialized)
                Initialize();

            var allEnemies = new List<DetectedObject>();
            foreach (var provider in _providers)
            {
                var enemies = provider.GetEnemies();
                if (enemies != null)
                    allEnemies.AddRange(enemies);
            }
            return allEnemies;
        }
    }
}
