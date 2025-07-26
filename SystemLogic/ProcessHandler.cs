// neural v3.0
using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.SystemLogic
{
    public static class ProcessHandler
    {
        public static Process? FindProcessByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            try
            {
                return Process.GetProcessesByName(name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ProcessHandler] Error finding process: {ex.Message}");
                return null;
            }
        }

        public static bool IsProcessRunning(string name)
        {
            return FindProcessByName(name) != null;
        }

        public static IntPtr GetMainModuleBaseAddress(string name)
        {
            var process = FindProcessByName(name);
            if (process == null || process.MainModule == null)
                return IntPtr.Zero;

            try
            {
                return process.MainModule.BaseAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ProcessHandler] Failed to get base address: {ex.Message}");
                return IntPtr.Zero;
            }
        }

        public static int? GetProcessId(string name)
        {
            var process = FindProcessByName(name);
            return process?.Id;
        }

        public static bool TryGetHandle(string name, out Process? process)
        {
            process = FindProcessByName(name);
            return process != null;
        }
    }
}
