// neural v3.0
using System;
using System.Diagnostics;
using JaysAi.Finale.Logging;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public class TitanTwoBridge
    {
        private readonly string _scriptPath;
        private Process? _titanProcess;

        public TitanTwoBridge(string scriptPath)
        {
            if (string.IsNullOrWhiteSpace(scriptPath))
                throw new ArgumentException("Script path cannot be null or empty.", nameof(scriptPath));

            _scriptPath = scriptPath;
        }

        public void Launch()
        {
            try
            {
                if (!FileVerifier.Exists(_scriptPath))
                {
                    AppLogger.LogWarning($"[TitanTwoBridge] Script not found: {_scriptPath}");
                    return;
                }

                _titanProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _scriptPath,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    }
                };

                _titanProcess.Start();
                AppLogger.LogInfo("[TitanTwoBridge] Titan Two script launched.");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("[TitanTwoBridge] Failed to launch script.", ex);
            }
        }

        public void Terminate()
        {
            try
            {
                if (_titanProcess is { HasExited: false })
                {
                    _titanProcess.Kill(true);
                    _titanProcess.Dispose();
                    AppLogger.LogInfo("[TitanTwoBridge] Titan process terminated.");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("[TitanTwoBridge] Error terminating process.", ex);
            }
        }
    }
}
