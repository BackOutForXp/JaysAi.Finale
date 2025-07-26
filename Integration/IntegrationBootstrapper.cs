//neural v3.0
using System;
using JaysAi.Finale.Integration;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public static class IntegrationBootstrapper
    {
        private static bool _bootstrapped = false;

        public static void Bootstrap()
        {
            if (_bootstrapped)
                return;

            Console.WriteLine("[Bootstrapper] Bootstrapping integration layer...");

            try
            {
                Logger.Initialize(); // Optional, but good for global logging first
                Integration.Initialize();
                Console.WriteLine("[Bootstrapper] Integration successfully bootstrapped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bootstrapper Error] Failed to bootstrap: {ex.Message}");
                // Consider fallback logic or crash-safe state here
            }

            _bootstrapped = true;
        }

        public static void Shutdown()
        {
            if (!_bootstrapped)
                return;

            Console.WriteLine("[Bootstrapper] Shutting down integration layer...");
            Integration.Shutdown();
            _bootstrapped = false;
        }
    }
}
