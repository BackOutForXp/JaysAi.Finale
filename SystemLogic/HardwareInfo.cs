// neural v3.0
using System;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.SystemLogic
{
    public static class HardwareInfo
    {
        public static string GetProcessorId()
        {
            return QueryWmi("Win32_Processor", "ProcessorId");
        }

        public static string GetMotherboardSerial()
        {
            return QueryWmi("Win32_BaseBoard", "SerialNumber");
        }

        public static string GetBiosSerial()
        {
            return QueryWmi("Win32_BIOS", "SerialNumber");
        }

        public static string GetGPUName()
        {
            return QueryWmi("Win32_VideoController", "Name");
        }

        public static string GetTotalRAM()
        {
            return QueryWmi("Win32_ComputerSystem", "TotalPhysicalMemory");
        }

        public static string GetDriveSerial(string driveLetter = "C")
        {
            return QueryWmi($"Win32_LogicalDisk.DeviceID='{driveLetter.ToUpper()}:'", "VolumeSerialNumber");
        }

        public static string GetMachineGuid()
        {
            try
            {
                using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
                return key?.GetValue("MachineGuid")?.ToString() ?? "UNKNOWN";
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        private static string QueryWmi(string className, string property)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {className}");
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj[property]?.ToString()?.Trim() ?? "UNKNOWN";
                }
            }
            catch
            {
                // Swallow errors for stealth/compatibility
            }
            return "UNKNOWN";
        }

        public static string GenerateHardwareFingerprint()
        {
            return string.Join("-",
                Normalize(GetProcessorId()),
                Normalize(GetMotherboardSerial()),
                Normalize(GetBiosSerial()),
                Normalize(GetMachineGuid()));
        }

        private static string Normalize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "NULL" : value.ToUpperInvariant();
        }
    }
}
