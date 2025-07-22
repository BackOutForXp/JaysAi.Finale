// heavenly v3.0 – Central Module Lifecycle Handler
using System;
using System.Collections.Generic;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Core
{
    public static class ModuleManager
    {
        private static readonly List<IModule> _modules = new();

        public static void Register(IModule module)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));

            if (!_modules.Contains(module))
            {
                _modules.Add(module);
                Logger.Debug($"Module registered: {module.GetType().Name}");
            }
        }

        public static void InitializeAll()
        {
            foreach (var module in _modules)
            {
                try
                {
                    module.Initialize();
                    Logger.Info($"Initialized: {module.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Init failed for {module.GetType().Name}: {ex.Message}");
                }
            }
        }

        public static void UpdateAll()
        {
            foreach (var module in _modules)
            {
                try
                {
                    module.Update();
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Update failed for {module.GetType().Name}: {ex.Message}");
                }
            }
        }

        public static void ShutdownAll()
        {
            foreach (var module in _modules)
            {
                try
                {
                    module.Shutdown();
                    Logger.Info($"Shutdown: {module.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Shutdown failed for {module.GetType().Name}: {ex.Message}");
                }
            }

            _modules.Clear();
        }

        public static IReadOnlyList<IModule> GetAllModules() => _modules.AsReadOnly();
    }
}
