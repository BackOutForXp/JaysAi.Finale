using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace JaysAi.Finale.SystemLogic
{
    public static class StartupManager
    {
        public static async Task InitializeAsync()
        {
            Console.WriteLine("[StartupManager] Preparing application launch...");

            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            // Optional: Delay to bypass anti-injection checks
            await Task.Delay(200);

            HandleRestartIfNeeded();

            Console.WriteLine("[StartupManager] Startup complete.");
        }

        private static void HandleRestartIfNeeded()
        {
            string restartFile = "restart.flag";
            if (File.Exists(restartFile))
            {
                File.Delete(restartFile);
                MessageBox.Show("Loader restarted after crash", "JaysAi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void MarkForRestart()
        {
            File.WriteAllText("restart.flag", "1");
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }

        public static void LaunchMinimized(Window window)
        {
            window.WindowState = WindowState.Minimized;
            window.ShowInTaskbar = false;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [x] Call await StartupManager.InitializeAsync() in App.xaml.cs
// - [ ] Add setting to ConfigManager.Config: LaunchMinimized
// - [ ] Add task scheduler or registry integration for auto-boot (optional)
// - [ ] Trigger MarkForRestart() on crash or remote reboot
// ===================================================================
