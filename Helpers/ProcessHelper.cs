// Neural v3.0 — ProcessHelper.cs
using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.Helpers
{
    public static class ProcessHelper
    {
        public static Process? GetProcessByName(string name)
        {
            return Process.GetProcessesByName(name).FirstOrDefault();
        }

        public static IntPtr GetModuleBaseAddress(Process process, string moduleName)
        {
            if (process == null || process.HasExited)
                return IntPtr.Zero;

            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                    return module.BaseAddress;
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetProcessHandle(string processName)
        {
            var process = GetProcessByName(processName);
            return process?.Handle ?? IntPtr.Zero;
        }

        public static bool IsProcessRunning(string name)
        {
            return GetProcessByName(name) != null;
        }

        public static bool WaitForProcess(string name, int timeoutMs = 10000)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (IsProcessRunning(name))
                    return true;

                System.Threading.Thread.Sleep(250);
            }

            return false;
        }
    }
}
