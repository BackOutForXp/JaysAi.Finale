// Monarch v1.0 – HardwareInfo.cs

using System;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace JaysAi.Finale.Utility
{
    public static class HardwareInfo
    {
        public static string GetHardwareId()
        {
            try
            {
                string cpuId = GetWMI("Win32_Processor", "ProcessorId");
                string motherboardSerial = GetWMI("Win32_BaseBoard", "SerialNumber");
                string biosSerial = GetWMI("Win32_BIOS", "SerialNumber");

                string combined = $"{cpuId}-{motherboardSerial}-{biosSerial}";
                return HashString(combined);
            }
            catch
            {
                return "HWID_ERROR";
            }
        }

        private static string GetWMI(string className, string property)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {className}");
                foreach (var obj in searcher.Get().Cast<ManagementObject>())
                {
                    return obj[property]?.ToString()?.Trim() ?? string.Empty;
                }
            }
            catch { }

            return string.Empty;
        }

        private static string HashString(string input)
        {
            using var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
