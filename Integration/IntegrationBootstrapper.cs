using System;
using System.Threading.Tasks;

namespace JaysAi.Finale.Integration
{
    public static class IntegrationBootstrapper
    {
        public static async Task InitializeAllAsync()
        {
            Console.WriteLine("[Bootstrapper] Beginning async integration sequence...");

            await Task.Run(() =>
            {
                try
                {
                    AutoIntegrationManager.RunFullScan();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Bootstrapper] Error during integration scan: {ex.Message}");
                }
            });

            Console.WriteLine("[Bootstrapper] All integrations initialized.");
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Final async wrapper for all hardware/game profile integration
// ✅ Can be launched after GUI init to reduce delay
// ✅ Handles AutoIntegrationManager internally
// - [ ] Tie into GUI “Launch” or “Auto Mode” toggle
// - [ ] Eventually monitor device disconnects (Zen, Titan, etc.)
// ===================================================================
