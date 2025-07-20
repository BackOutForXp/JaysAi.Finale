// File: System/MainLauncher.cs
using JaysAi.Finale.Core;
using JaysAi.Finale.Security;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System;
using System.IO;
using System.Windows.Shapes;

namespace JaysAi.Finale.System
{
    public static class MainLauncher
    {
        /// <summary>
        /// Launches the loader after validating environment and initializing core systems.
        /// </summary>
        public static void Launch(string[] args)
        {
            Console.Title = "JaysAi.Finale - Monarch Mode Loader";

            Logger.Log("[MainLauncher] Initializing...");

            SetupAppFolders();
            StealthMode.Enable();

            AppSettings settings = SettingsManager<AppSettings>.Load("default") ?? new AppSettings();
            SettingsManager<AppSettings>.Save("default", settings); // Save to ensure it's initialized

            StartUI();
        }

        private static void SetupAppFolders()
        {
            string baseDir = Paths.BaseDirectory;
            string logs = Path.Combine(baseDir, "Logs");
            string profiles = Path.Combine(baseDir, "Profiles");

            Directory.CreateDirectory(logs);
            Directory.CreateDirectory(profiles);
        }

        private static void StartUI()
        {
            // Launch UI via WPF entry point
            System.Windows.Application app = new System.Windows.Application();
            var window = new Loader.LoaderGUI();
            app.Run(window);
        }
    }
}
