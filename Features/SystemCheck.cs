// neural v3.0
using System;
using System.IO;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Security;

namespace JaysAi.Finale.Features
{
    public static class SystemCheck
    {
        public static bool RunAllChecks()
        {
            return IsOSCompatible() &&
                   AreRequiredFilesPresent() &&
                   IsSecureEnvironment() &&
                   IsHardwareApproved();
        }

        private static bool IsOSCompatible()
        {
            Version current = Environment.OSVersion.Version;
            return current.Major >= 10; // Windows 10+
        }

        private static bool AreRequiredFilesPresent()
        {
            string[] requiredFiles = new[]
            {
                "JaysAi.Core.dll",
                "OpenCvSharpExtern.dll",
                "config.json"
            };

            foreach (string file in requiredFiles)
            {
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)))
                {
                    Logger.Log($"[SystemCheck] Missing file: {file}", LogLevel.Error);
                    return false;
                }
            }

            return true;
        }

        private static bool IsSecureEnvironment()
        {
            return !DebuggerHelper.IsDebugging() &&
                   !Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING")?.Equals("1") == true;
        }

        private static bool IsHardwareApproved()
        {
            string hardwareId = HardwareIdGenerator.Generate();
            return SecurityWhitelist.IsWhitelisted(hardwareId);
        }
    }
}
