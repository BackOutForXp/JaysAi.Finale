// heavenly v3.0 – EntryPoint Injection and Init Sequence
using JaysAi.Finale.Security;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Core;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Core
{
    public static class MainInjection
    {
        private static bool _hasInjected = false;

        public static void Initialize()
        {
            if (_hasInjected) return;

            LogManager.Initialize("JaysAi.Finale");
            CrashLogger.AttachGlobalHandler();

            StealthScanner.ScanForThreats();
            AntiDebug.ApplyPatches();
            HardwareScanner.VerifySystem();

            GameProcessHelper.LocateTargetGame();
            AiOrchestrator.Start();

            FeatureToggleManager.ApplyFeatureToggles();
            LicenseValidator.Validate();

            Logger.Info("MainInjection complete. System is now active.");
            _hasInjected = true;
        }
    }
}
