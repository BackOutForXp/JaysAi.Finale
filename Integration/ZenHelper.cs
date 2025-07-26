//neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public static class ZenHelper
    {
        private const string ZenVid = "1A86"; // Example: USB Vendor ID for Zen
        private const string ZenPid = "7523"; // Example: USB Product ID for Zen
        private const string ZenIdentifier = $"VID_{ZenVid}&PID_{ZenPid}";

        public static event Action? ZenConnected;
        public static event Action? ZenDisconnected;

        private static bool _zenPreviouslyConnected;

        public static void Initialize()
        {
            Logger.Info("[ZenHelper] Starting Zen device monitoring...");

            USBHelper.DeviceConnected += HandleUsbConnected;
            USBHelper.DeviceDisconnected += HandleUsbDisconnected;

            var currentDevices = USBHelper.GetCurrentlyConnectedDevices();
            foreach (var deviceId in currentDevices)
            {
                if (IsZenDevice(deviceId))
                {
                    _zenPreviouslyConnected = true;
                    Logger.Info("[ZenHelper] Zen device detected on startup.");
                    ZenConnected?.Invoke();
                    break;
                }
            }
        }

        private static void HandleUsbConnected(string deviceId)
        {
            if (!_zenPreviouslyConnected && IsZenDevice(deviceId))
            {
                _zenPreviouslyConnected = true;
                Logger.Info($"[ZenHelper] Zen device connected: {deviceId}");
                ZenConnected?.Invoke();
            }
        }

        private static void HandleUsbDisconnected(string deviceId)
        {
            if (_zenPreviouslyConnected && IsZenDevice(deviceId))
            {
                _zenPreviouslyConnected = false;
                Logger.Info($"[ZenHelper] Zen device disconnected: {deviceId}");
                ZenDisconnected?.Invoke();
            }
        }

        private static bool IsZenDevice(string deviceId)
        {
            return deviceId.Contains(ZenIdentifier, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsZenCurrentlyConnected()
        {
            return USBHelper.GetCurrentlyConnectedDevices()
                .Any(IsZenDevice);
        }

        public static string? GetZenPortName()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
                foreach (var port in searcher.Get().Cast<ManagementObject>())
                {
                    var deviceId = port["PNPDeviceID"]?.ToString() ?? "";
                    if (IsZenDevice(deviceId))
                        return port["DeviceID"]?.ToString(); // e.g., "COM5"
                }
            }
            catch (Exception ex)
            {
                Logger.Warn($"[ZenHelper] Failed to locate Zen COM port: {ex.Message}");
            }

            return null;
        }
    }
}
