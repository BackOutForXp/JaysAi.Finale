// neural v3.0
using System;
using System.Diagnostics;
using JaysAi.Finale.Logging;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public class CronusZenBridge
    {
        private readonly string _zenScriptPath;
        private Process? _zenProcess;

        public CronusZenBridge(string scriptPath)
        {
            if (string.IsNullOrWhiteSpace(scriptPath))
                throw new ArgumentException("Script path cannot be null or empty.", nameof(scriptPath));

            _zenScriptPath = scriptPath;
        }

        public void Launch()
        {
            try
            {
                if (!FileVerifier.Exists(_zenScriptPath))
                {
                    AppLogger.LogWarning($"[CronusZenBridge] Script file not found: {_zenScriptPath}");
                    return;
                }

                _zenProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _zenScriptPath,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    }
                };

                _zenProcess.Start();
                AppLogger.LogInfo("[CronusZenBridge] Zen script launched.");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("[CronusZenBridge] Failed to launch script.", ex);
            }
        }

        public void Terminate()
        {
            try
            {
                if (_zenProcess is { HasExited: false })
                {
                    _zenProcess.Kill(true);
                    _zenProcess.Dispose();
                    AppLogger.LogInfo("[CronusZenBridge] Zen process terminated.");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("[CronusZenBridge] Error while terminating process.", ex);
            }
        }
    }
}
