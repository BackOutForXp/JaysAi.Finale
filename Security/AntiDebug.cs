using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Security
{
    public static class AntiDebug
    {
        [DllImport("kernel32.dll")]
        private static extern bool IsDebuggerPresent();

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryInformationProcess(nint processHandle, int processInformationClass,
            ref int debugPort, int processInformationLength, ref int returnLength);

        public static bool IsBeingDebugged()
        {
            if (Debugger.IsAttached || IsDebuggerPresent())
                return true;

            int debugPort = 0;
            int returnLength = 0;

            int status = NtQueryInformationProcess(
                Process.GetCurrentProcess().Handle,
                7, // ProcessDebugPort
                ref debugPort,
                sizeof(int),
                ref returnLength
            );

            return debugPort != 0;
        }

        public static bool IsRunningInVM()
        {
            string[] knownVMVendors = { "VMware", "VirtualBox", "Xen", "QEMU" };
            string biosInfo = GetBiosManufacturer();

            return knownVMVendors.Any(v => biosInfo.IndexOf(v, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static string GetBiosManufacturer()
        {
            try
            {
                using var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (var obj in searcher.Get())
                {
                    return obj["Manufacturer"]?.ToString() ?? string.Empty;
                }
            }
            catch { }

            return string.Empty;
        }

        public static void KillIfDebugged()
        {
            if (IsBeingDebugged())
            {
                Console.WriteLine("[AntiDebug] Debugger detected. Exiting.");
                Environment.Exit(202);
            }

            if (IsRunningInVM())
            {
                Console.WriteLine("[AntiDebug] Virtual machine detected. Exiting.");
                Environment.Exit(203);
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [x] Add AntiDebug.KillIfDebugged() to App.xaml.cs (Startup)
// - [ ] Add tier toggle: AllowPublicDebugMode = false (in Config)
// - [ ] Optional: Hook into crash report sender or obfuscator
// - [ ] Optional: Kill if dnSpy or ILSpy is running in tasklist
// ===================================================================
