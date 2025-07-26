//neural v3.0
using System;
using JaysAi.Finale.Integration.Modules;
using JaysAi.Finale.Integration.Signals;
using JaysAi.Finale.Input;
using JaysAi.Finale.Security;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public static class Integration
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized)
                return;

            Console.WriteLine("[Integration] Initializing all subsystems...");

            // System Setup
            AntiTamper.Initialize();
            ProcessValidator.EnforceWhitelist();
            GameDetector.LogDetectedGame();

            // Input Setup
            ControllerBridge.Instance.Attach();  // Assumes extension pattern
            ControllerSignalBus.Initialize();

            // Integration Modules
            OverlaySyncBridge.Initialize();
            AutoDetectionHelper.StartMonitoring();
            CaptureCardHelper.Initialize();

            // Diagnostics and Logging
            TelemetryLogger.StartSession();
            HealthMonitor.RegisterCallbacks();

            _initialized = true;
            Console.WriteLine("[Integration] Initialization complete.");
        }

        public static void Shutdown()
        {
            if (!_initialized)
                return;

            Console.WriteLine("[Integration] Shutting down subsystems...");

            HealthMonitor.Cleanup();
            TelemetryLogger.EndSession();
            ControllerBridge.Instance.Detach();

            _initialized = false;
        }
    }
}
