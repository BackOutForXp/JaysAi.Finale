//neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public static class USBHelper
    {
        public static event Action<string>? DeviceConnected;
        public static event Action<string>? DeviceDisconnected;

        private static readonly HashSet<string> _knownDevices = new();

        public static void Initialize()
        {
            try
            {
                StartWatcher("__InstanceCreationEvent", OnDeviceConnected);
                StartWatcher("__InstanceDeletionEvent", OnDeviceDisconnected);
                Logger.Info("[USBHelper] USB event watchers initialized.");
            }
            catch (Exception ex)
            {
                Logger.Error($"[USBHelper] Initialization failed: {ex.Message}");
            }
        }

        private static void StartWatcher(string eventType, Action<string> callback)
        {
            var query = new WqlEventQuery($@"
                SELECT * FROM {eventType} WITHIN 2 
                WHERE TargetInstance ISA 'Win32_USBControllerDevice'");

            var watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += (s, e) =>
            {
                try
                {
                    var target = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                    var devicePath = target["Dependent"]?.ToString();
                    if (devicePath != null)
                    {
                        var cleanPath = ExtractDeviceId(devicePath);
                        callback(cleanPath);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn($"[USBHelper] Event handling failed: {ex.Message}");
                }
            };

            watcher.Start();
        }

        private static void OnDeviceConnected(string deviceId)
        {
            if (_knownDevices.Add(deviceId))
            {
                Logger.Debug($"[USBHelper] Device connected: {deviceId}");
                DeviceConnected?.Invoke(deviceId);
            }
        }

        private static void OnDeviceDisconnected(string deviceId)
        {
            if (_knownDevices.Remove(deviceId))
            {
                Logger.Debug($"[USBHelper] Device disconnected: {deviceId}");
                DeviceDisconnected?.Invoke(deviceId);
            }
        }

        private static string ExtractDeviceId(string raw)
        {
            // Extracts something like: "USB\\VID_1234&PID_5678"
            var start = raw.IndexOf("USB\\", StringComparison.OrdinalIgnoreCase);
            var end = raw.IndexOf("\"", start, StringComparison.OrdinalIgnoreCase);
            if (start >= 0 && end > start)
                return raw.Substring(start, end - start);

            return raw;
        }

        public static List<string> GetCurrentlyConnectedDevices()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub");
                return searcher.Get()
                    .Cast<ManagementObject>()
                    .Select(obj => obj["DeviceID"]?.ToString() ?? string.Empty)
                    .Where(id => !string.IsNullOrWhiteSpace(id))
                    .ToList();
            }
            catch (Exception ex)
            {
                Logger.Warn($"[USBHelper] Device fetch failed: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
