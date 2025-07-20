using System;
using System.Collections.Generic;
using DirectShowLib;

namespace JaysAi.Finale.Integration
{
    public static class CaptureCardHelper
    {
        public static List<string> GetConnectedCaptureDevices()
        {
            var devices = new List<string>();

            try
            {
                DsDevice[] videoDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

                foreach (DsDevice device in videoDevices)
                {
                    if (IsCaptureCard(device.Name))
                    {
                        Console.WriteLine($"[CaptureCardHelper] Found capture device: {device.Name}");
                        devices.Add(device.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CaptureCardHelper] Error detecting capture cards: {ex.Message}");
            }

            return devices;
        }

        private static bool IsCaptureCard(string deviceName)
        {
            // Basic match; can expand with known vendor list
            string[] knownCaptureBrands = { "Elgato", "AVerMedia", "Game Capture", "Live Gamer" };

            foreach (var brand in knownCaptureBrands)
            {
                if (deviceName.ToLower().Contains(brand.ToLower()))
                    return true;
            }

            return false;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Detects capture cards using DirectShow (Elgato, AVerMedia)
// ✅ Logs connected devices
// ✅ Future-ready for visual ESP or overlay stream injection
// - [ ] Add capture preview or scene parsing via Skia or AI
// - [ ] Link detection to AutoDetectionHelper.cs
// - [ ] Filter out webcams and virtual cams
// ===================================================================
