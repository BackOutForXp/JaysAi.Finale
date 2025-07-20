// File: System\HardwareScanner.cs
using System;
using System.Collections.Generic;
using System.Management;

namespace JaysAi.Finale.SystemLogic
{
    public static class HardwareScanner
    {
        public static string GetGPUName()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Name"]?.ToString() ?? "Unknown GPU";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return "Unknown GPU";
        }

        public static string GetCPUName()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Name"]?.ToString() ?? "Unknown CPU";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return "Unknown CPU";
        }

        public static List<string> GetMonitors()
        {
            var monitors = new List<string>();

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    monitors.Add(obj["Name"]?.ToString() ?? "Unnamed Monitor");
                }
            }
            catch
            {
                monitors.Add("Error retrieving monitors");
            }

            return monitors;
        }

        public static string GetTotalRAM()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    double totalBytes = Convert.ToDouble(obj["TotalPhysicalMemory"]);
                    return $"{Math.Round(totalBytes / (1024 * 1024 * 1024), 2)} GB";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return "Unknown RAM";
        }
    }
}
