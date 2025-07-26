//neural v3.0
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Security
{
    public static class TamperDetector
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr bytesRead);

        public static bool IsFunctionTampered(string moduleName, IntPtr functionPtr, byte[] expectedBytes)
        {
            IntPtr moduleBase = GetModuleHandle(moduleName);
            if (moduleBase == IntPtr.Zero) return true;

            byte[] buffer = new byte[expectedBytes.Length];
            IntPtr processHandle = Process.GetCurrentProcess().Handle;

            bool success = ReadProcessMemory(processHandle, functionPtr, buffer, buffer.Length, out _);
            if (!success) return true;

            return !buffer.SequenceEqual(expectedBytes);
        }

        public static bool IsDebuggerInjectedDllPresent(string[] knownDebugDlls)
        {
            return Process.GetCurrentProcess().Modules
                .Cast<ProcessModule>()
                .Any(module => knownDebugDlls.Any(dll => module.ModuleName.Contains(dll, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
