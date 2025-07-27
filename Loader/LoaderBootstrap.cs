using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.Collections.Generic;

namespace JaysAi.Finale.Loader
{
    public static class LoaderBootstrap
    {
        public static AppSettings Settings { get; private set; }
        public static SettingsManager SettingsManager { get; private set; }

        private static MainLoop _mainLoop;
        private static List<IModule> _modules = new();

        public static void Initialize()
        {
            // Load or create config
            SettingsManager = new SettingsManager("JaysAi", "Finale", "AppSettings.json");
            Settings = SettingsManager.Load<AppSettings>();

            // Initialize main loop
            _mainLoop = new MainLoop();
            _mainLoop.SetSettings(Settings); // Inject manually

            // Register all modules
            _modules = ModuleRegistry.GetModules(Settings);

            foreach (var module in _modules)
                module.Initialize();

            MainLoop.Start(); // static call if required
        }

        public static void Shutdown()
        {
            foreach (var module in _modules)
                module.Shutdown();

            SettingsManager.Save(Settings);
        }

        public static void SaveSettings() => SettingsManager.Save(Settings);
    }
}
