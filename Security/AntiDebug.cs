// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Security
{
    public static class AntiDebug
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref int processInformation, int processInformationLength, ref int returnLength);

        public static bool IsDebuggerAttached()
        {
            bool isDebuggerPresent = Debugger.IsAttached;
            return isDebuggerPresent || IsDebuggerPresentAPI() || HasNtDebugFlags();
        }

        private static bool IsDebuggerPresentAPI()
        {
            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
            return isDebuggerPresent;
        }

        private static bool HasNtDebugFlags()
        {
            int isDebugged = 0;
            int returnLength = 0;
            int result = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, 0x1F, ref isDebugged, sizeof(int), ref returnLength);
            return result == 0 && isDebugged == 1;
        }
    }
}
