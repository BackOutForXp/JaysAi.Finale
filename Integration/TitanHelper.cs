using System;
using System.IO.Ports;
using System.Linq;

namespace JaysAi.Finale.Integration
{
    public static class TitanHelper
    {
        public static bool IsConnected()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();

                foreach (var port in ports)
                {
                    if (port.ToLower().Contains("com"))
                    {
                        Console.WriteLine($"[TitanHelper] Found port: {port}");

                        if (IsLikelyTitanDevice(port))
                        {
                            Console.WriteLine("[TitanHelper] Titan device likely connected.");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TitanHelper] Detection failed: {ex.Message}");
            }

            return false;
        }

        private static bool IsLikelyTitanDevice(string port)
        {
            // TODO: Match based on serial descriptor, VID/PID, or device name
            return port.Contains("4") || port.Contains("Titan"); // Placeholder logic
        }

        public static void Initialize()
        {
            if (IsConnected())
            {
                Console.WriteLine("[TitanHelper] Initializing Titan integration...");
                // TODO: Inject profile, start monitoring, or bind inputs
            }
            else
            {
                Console.WriteLine("[TitanHelper] No Titan detected. Skipping init.");
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Prepares Titan One/Two detection (COM-based)
// ✅ Modular init avoids crashing if not connected
// ✅ Future-safe with placeholder logic
// - [ ] Add Titan COM descriptor parsing
// - [ ] Link to GPC loader or input injector
// - [ ] Add support to AutoDetectionHelper.cs
// ===================================================================
