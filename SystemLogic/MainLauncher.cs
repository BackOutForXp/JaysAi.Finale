// Neural v3.0 — MainLauncher.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Features;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Security;
using JaysAi.Finale.Settings;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace JaysAi.Finale.SystemLogic
{
    public static class MainLauncher
    {
        private static bool _initialized = false;

        /// <summary>
        /// Entry point for launching all internal systems and overlay.
        /// </summary>
        public static async Task StartAsync()
        {
            if (_initialized) return;

            try
            {
                // Preflight security
                if (!SecurityGuard.VerifyStartupIntegrity())
                {
                    Console.WriteLine("Security verification failed.");
                    return;
                }

                // Load config
                GlobalConfig.Load();

                // Initialize input hooks
                InputInterceptor.Initialize();

                // Start overlay window (WPF version)
                await Task.Run(() =>
                {
                    Application app = new();
                    var overlayWindow = new OverlayWindow();
                    app.Run(overlayWindow);
                });

                // Initialize modules
                ESPModuleManager.Initialize();
                SnapAssistController.Initialize();
                SignalDelay.Initialize();
                PredictionEngine.Initialize();

                // Log
                Console.WriteLine("JaysAi Neural system launched successfully.");
                _initialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Launcher Error] {ex.Message}");
            }
        }

        /// <summary>
        /// Clean shutdown of all systems.
        /// </summary>
        public static void Shutdown()
        {
            try
            {
                InputInterceptor.Shutdown();
                OverlayFinalizer.FinalizeAll();
                SignalBus.ClearAll();
                Console.WriteLine("JaysAi Neural system has shut down cleanly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Shutdown Error] {ex.Message}");
            }
        }
    }
}
