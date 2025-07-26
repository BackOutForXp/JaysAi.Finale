// neural v3.0
using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.SystemLogic
{
    public static class GameProcessHelper
    {
        private static Process? _cachedProcess;
        private static string _cachedProcessName = string.Empty;
        private static readonly object _lock = new();

        public static Process? GetProcess(string processName, bool forceRefresh = false)
        {
            lock (_lock)
            {
                if (!forceRefresh && _cachedProcess != null &&
                    !_cachedProcess.HasExited &&
                    string.Equals(_cachedProcessName, processName, StringComparison.OrdinalIgnoreCase))
                {
                    return _cachedProcess;
                }

                var processes = Process.GetProcessesByName(processName);
                _cachedProcess = processes.FirstOrDefault();
                _cachedProcessName = processName;

                return _cachedProcess;
            }
        }

        public static bool IsRunning(string processName)
        {
            var process = GetProcess(processName);
            return process != null && !process.HasExited;
        }

        public static int? GetProcessId(string processName)
        {
            var process = GetProcess(processName);
            return process?.Id;
        }

        public static IntPtr? GetMainModuleBaseAddress(string processName)
        {
            var process = GetProcess(processName);
            return process?.MainModule?.BaseAddress;
        }

        public static string? GetProcessPath(string processName)
        {
            try
            {
                var process = GetProcess(processName);
                return process?.MainModule?.FileName;
            }
            catch
            {
                return null;
            }
        }

        public static string? GetWindowTitle(string processName)
        {
            var process = GetProcess(processName);
            return process?.MainWindowTitle;
        }
    }
}
