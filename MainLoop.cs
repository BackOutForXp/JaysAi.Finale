//heavenly v3.0
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Security;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale
{
    public static class MainLoop
    {
        private static CancellationTokenSource _cancellationSource;
        private static Task _mainLoopTask;

        public static void Start()
        {
            _cancellationSource = new CancellationTokenSource();
            _mainLoopTask = Task.Factory.StartNew(() => Run(_cancellationSource.Token),
                _cancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public static void Stop()
        {
            _cancellationSource?.Cancel();
        }

        private static void Run(CancellationToken token)
        {
            Logger.Log("MainLoop started.");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    Thread.Sleep(16); // ~60 FPS tick

                    FeatureToggleManager.ApplyPendingToggles();

                    ModuleManager.TickAllModules();

                    StealthScanner.PerformStealthSweep();

                    UpdateChecker.CheckForScheduledUpdates();

                    PerformanceTracker.RecordSystemUsage();
                }
                catch (Exception ex)
                {
                    Logger.LogError("MainLoop exception: " + ex.Message);
                    LogManager.LogCritical("MainLoop Crash", ex.ToString());
                }
            }

            Logger.Log("MainLoop terminated.");
        }
    }
}
