//neural v3.0
using JaysAi.Finale.Security.Diagnostics;
using JaysAi.Finale.Security.Licensing;
using JaysAi.Finale.Security.Validation;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Security
{
    public static class SecurityManager
    {
        public static bool IsAuthenticated { get; private set; }
        public static bool IsLicenseValid => LicenseValidator.Instance.IsValid;
        public static bool IsSystemFingerprintValid => FingerprintValidator.Instance.IsSystemValid();
        public static bool IsDebuggerSafe => !AntiDebugHook.Instance.IsDebuggerAttached;

        public static void Initialize()
        {
            try
            {
                Logger.Log("Initializing security checks...");

                AntiDebugHook.Instance.Hook();

                if (!IsDebuggerSafe)
                    throw new InvalidOperationException("Debugger detected!");

                if (!IsLicenseValid)
                    throw new UnauthorizedAccessException("License invalid.");

                if (!IsSystemFingerprintValid)
                    throw new UnauthorizedAccessException("System fingerprint mismatch.");

                IsAuthenticated = true;

                Logger.Log("SecurityManager: All checks passed. System secure.");
            }
            catch (Exception ex)
            {
                Logger.LogError("SecurityManager Init Failed: " + ex.Message);
                IsAuthenticated = false;
                Environment.Exit(1); // Immediate shutdown
            }
        }

        public static void ForceLogout(string reason = "Security violation.")
        {
            Logger.LogCritical($"Forced logout: {reason}");
            IsAuthenticated = false;
            Environment.Exit(1);
        }
    }
}
