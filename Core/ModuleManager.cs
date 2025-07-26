// neural v3.0
using JaysAi.Finale.Features;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Security;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Core
{
    public class ModuleManager
    {
        private readonly Dictionary<string, IModule> _modules;

        public ModuleManager()
        {
            _modules = new Dictionary<string, IModule>();
            RegisterModules();
        }

        private void RegisterModules()
        {
            LogManager.Info("ModuleManager: Registering core modules...");

            AddModule("ESP", new ESPModule());
            AddModule("AimAssist", new AimAssistModule());
            AddModule("AntiRecoil", new RecoilCompensator());
            AddModule("SnapAssist", new SnapAssistController());
            AddModule("SilentAim", new SilentAim());
            AddModule("TriggerBot", new TriggerBot());
            AddModule("MovementAssist", new MovementAssist());
            AddModule("StealthMode", new StealthMode());
            AddModule("StickAssist", new StickInputBridge());

            LogManager.Info($"ModuleManager: Registered {_modules.Count} modules.");
        }

        private void AddModule(string key, IModule module)
        {
            if (!_modules.ContainsKey(key))
            {
                _modules.Add(key, module);
                LogManager.Debug($"ModuleManager: [{key}] added.");
            }
        }

        public void UpdateAll()
        {
            foreach (var module in _modules.Values)
            {
                try
                {
                    if (module.Enabled)
                        module.Update();
                }
                catch (Exception ex)
                {
                    LogManager.Exception($"ModuleManager.Update: {module.GetType().Name}", ex);
                }
            }
        }

        public void ToggleModule(string moduleName, bool enable)
        {
            if (_modules.TryGetValue(moduleName, out var module))
            {
                module.Enabled = enable;
                LogManager.Info($"ModuleManager: [{moduleName}] toggled {(enable ? "ON" : "OFF")}");
            }
            else
            {
                LogManager.Warn($"ModuleManager: [{moduleName}] not found.");
            }
        }

        public T GetModule<T>() where T : class, IModule
        {
            foreach (var module in _modules.Values)
            {
                if (module is T match)
                    return match;
            }
            return null;
        }
    }
}
