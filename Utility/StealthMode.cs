// File: Utility\StealthMode.cs

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Utility
{
    public static class StealthMode
    {
        public static bool IsRunningInSandbox()
        {
            try
            {
                string[] suspiciousProcesses = new[]
                {
                    "vmsrvc.exe", "vmusrvc.exe",    // VMWare
                    "xenservice.exe",               // Xen
                    "vboxservice.exe", "vboxtray.exe", // VirtualBox
                    "wireshark.exe", "fiddler.exe", // Debugging/packet capture
                    "ollydbg.exe", "idaq.exe",      // Reverse engineering tools
                    "cheatengine.exe"
                };

                foreach (var process in Process.GetProcesses())
                {
                    foreach (var name in suspiciousProcesses)
                    {
                        if (process.ProcessName.ToLower().Contains(name.Replace(".exe", "")))
                            return true;
                    }
                }
            }
            catch
            {
                // Silent fail — better safe than throwing in stealth mode
                return true;
            }

            return false;
        }

        public static void TryEvade()
        {
            if (IsRunningInSandbox())
            {
                Environment.Exit(0); // Auto-terminate in suspicious environments
            }
        }
    }
}
