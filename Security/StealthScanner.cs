//neural v3.0
using JaysAi.Finale.Security.Diagnostics;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace JaysAi.Finale.Security
{
    public sealed class StealthScanner : IDisposable
    {
        private static readonly Lazy<StealthScanner> _instance = new(() => new StealthScanner());
        private readonly Timer _scanTimer;
        private readonly HashSet<string> _blacklistedProcessNames;

        public static StealthScanner Instance => _instance.Value;

        private StealthScanner()
        {
            _blacklistedProcessNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "ollydbg", "x64dbg", "ida64", "ida32", "dnspy", "scylla", "procmon", "procexp",
                "cheatengine", "charles", "httpdebugger", "wireshark", "processhacker"
            };

            _scanTimer = new Timer(5000); // scan every 5 seconds
            _scanTimer.Elapsed += (_, _) => ScanProcesses();
            _scanTimer.Start();
        }

        private void ScanProcesses()
        {
            try
            {
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    if (_blacklistedProcessNames.Contains(process.ProcessName))
                    {
                        Logger.LogCritical($"⚠️ Blacklisted process detected: {process.ProcessName}");
                        SecurityManager.ForceLogout($"Unauthorized tool: {process.ProcessName}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("StealthScanner scan failed: " + ex.Message);
            }
        }

        public void AddToBlacklist(string processName)
        {
            _blacklistedProcessNames.Add(processName);
        }

        public void Dispose()
        {
            _scanTimer?.Stop();
            _scanTimer?.Dispose();
        }
    }
}
