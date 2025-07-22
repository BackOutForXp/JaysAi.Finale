//heavenly v3.0
using JaysAi.Finale.Loader;
using JaysAi.Finale.Security;
using JaysAi.Finale.SystemLogic;
using JaysAi.Loader;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace JaysAi.Finale
{
    public static class MainLauncher
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception ex = e.ExceptionObject as Exception;
                LogManager.LogCritical("Unhandled Exception", ex?.ToString() ?? "Unknown");
            };

            if (args.Length > 0 && args[0].Equals("--stealth", StringComparison.OrdinalIgnoreCase))
            {
                StealthController.LaunchInStealth();
                return;
            }

            if (!ProcessHandler.IsOnlyInstance())
            {
                MessageBox.Show("JaysAi is already running.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StartupManager.PerformPreLaunchChecks();
            ModuleManager.InitializeCoreModules();

            LogManager.LogInfo("JaysAi Loader is launching...");

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
