// neural v3.0
using System;
using System.Runtime.InteropServices;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public static class SystemStatus
    {
        public static string MachineName => Environment.MachineName;
        public static string OSVersion => RuntimeInformation.OSDescription;
        public static string Architecture => RuntimeInformation.OSArchitecture.ToString();
        public static string Framework => RuntimeInformation.FrameworkDescription;
        public static string User => Environment.UserName;

        public static bool Is64BitProcess => Environment.Is64BitProcess;
        public static bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;
        public static DateTime Uptime => DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount64);

        public static void LogSystemInfo()
        {
            Log.Info("===== SYSTEM STATUS =====");
            Log.Info($"Machine Name       : {MachineName}");
            Log.Info($"User               : {User}");
            Log.Info($"OS Version         : {OSVersion}");
            Log.Info($"Architecture       : {Architecture}");
            Log.Info($".NET Framework     : {Framework}");
            Log.Info($"64-Bit OS          : {Is64BitOperatingSystem}");
            Log.Info($"64-Bit Process     : {Is64BitProcess}");
            Log.Info($"System Uptime      : {Uptime}");
        }

        public static string GetStatusSummary()
        {
            return $"User: {User} | OS: {OSVersion} | Arch: {Architecture} | Uptime: {Uptime}";
        }
    }
}
