//neural v3.0
using JaysAi.Finale.Security.Core;
using JaysAi.Finale.Security.Cryptography;
using JaysAi.Finale.Security.Diagnostics;
using JaysAi.Finale.Security.Licensing;
using JaysAi.Finale.Security.Validation;
using JaysAi.Finale.Utility;
using System;
using System.Security.RightsManagement;

namespace JaysAi.Finale.Security
{
    public static class Security
    {
        public static AntiDebugHook AntiDebug { get; } = new();
        public static AuthManager Auth { get; } = new();
        public static LicenseValidator License { get; } = new();
        public static FeatureManager Features { get; } = new();
        public static CryptoProvider Crypto { get; } = new();
        public static FingerprintValidator Fingerprint { get; } = new();

        public static void Initialize()
        {
            try
            {
                AntiDebug.Hook();
                Crypto.Initialize();
                License.Validate();
                Auth.Initialize();
                Features.Initialize();
            }
            catch (Exception ex)
            {
                Logger.LogCritical("Security initialization failed: " + ex.Message);
                Environment.FailFast("Security enforcement failure.");
            }
        }

        public static bool IsSecure =>
            !AntiDebug.IsDebuggerAttached &&
            License.IsValid &&
            Fingerprint.IsSystemValid();
    }
}
