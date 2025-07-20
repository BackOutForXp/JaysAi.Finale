using System;
using System.IO.Ports;
using System.Linq;

namespace JaysAi.Finale.Integration
{
    public static class ZenHelper
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
                        // NOTE: In a real scenario, you’d probe for device identity here
                        Console.WriteLine($"[ZenHelper] Found port: {port}");

                        // Placeholder for actual detection logic
                        if (IsLikelyZenDevice(port))
                        {
                            Console.WriteLine("[ZenHelper] Cronus Zen likely connected.");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ZenHelper] Detection failed: {ex.Message}");
            }

            return false;
        }

        private static bool IsLikelyZenDevice(string port)
        {
            // TODO: Identify Zen by known VID/PID or serial descriptor
            return port.Contains("3") || port.Contains("Zen"); // Placeholder
        }

        public static void Initialize()
        {
            if (IsConnected())
            {
                Console.WriteLine("[ZenHelper] Initializing Zen integration...");
                // TODO: Add real initialization logic here
            }
            else
            {
                Console.WriteLine("[ZenHelper] No Zen detected. Skipping init.");
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Prepares Cronus Zen detection via COM port scan
// ✅ Detects and logs connected ports
// ✅ Future-safe placeholder logic to avoid crashes
// - [ ] Implement VID/PID scan for true Zen identification
// - [ ] Add GPC profile loader or macro injector
// - [ ] Link to AutoDetectionHelper → DetectHardwareIntegration()
// ===================================================================
