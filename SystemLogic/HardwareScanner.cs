// neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace JaysAi.Finale.SystemLogic
{
    public static class HardwareScanner
    {
        public static Dictionary<string, string> GetHardwareIdentifiers()
        {
            return new Dictionary<string, string>
            {
                ["CPU"] = GetCpuId(),
                ["GPU"] = GetGpuId(),
                ["Motherboard"] = GetMotherboardId(),
                ["Disk"] = GetDiskId()
            };
        }

        private static string GetCpuId()
        {
            return QueryWmi("Win32_Processor", "ProcessorId");
        }

        private static string GetGpuId()
        {
            return QueryWmi("Win32_VideoController", "PNPDeviceID");
        }

        private static string GetMotherboardId()
        {
            return QueryWmi("Win32_BaseBoard", "SerialNumber");
        }

        private static string GetDiskId()
        {
            return QueryWmi("Win32_DiskDrive", "SerialNumber");
        }

        private static string QueryWmi(string wmiClass, string wmiProperty)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher($"SELECT {wmiProperty} FROM {wmiClass}");
                foreach (ManagementObject obj in searcher.Get())
                {
                    var value = obj[wmiProperty]?.ToString();
                    if (!string.IsNullOrWhiteSpace(value))
                        return value.Trim();
                }
            }
            catch
            {
                // Silent fail – fallback logic can be implemented if needed
            }
            return "UNKNOWN";
        }
    }
}

