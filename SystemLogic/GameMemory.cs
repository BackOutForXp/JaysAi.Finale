// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.SystemLogic
{
    public static class GameMemory
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        private static IntPtr _processHandle = IntPtr.Zero;
        private static Process? _targetProcess;

        public static bool Attach(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
                return false;

            _targetProcess = processes[0];
            _processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, _targetProcess.Id);

            return _processHandle != IntPtr.Zero;
        }

        public static bool ReadBytes(IntPtr address, byte[] buffer, out int bytesRead)
        {
            bytesRead = 0;
            if (_processHandle == IntPtr.Zero)
                return false;

            return ReadProcessMemory(_processHandle, address, buffer, buffer.Length, out bytesRead);
        }

        public static bool WriteBytes(IntPtr address, byte[] data, out int bytesWritten)
        {
            bytesWritten = 0;
            if (_processHandle == IntPtr.Zero)
                return false;

            return WriteProcessMemory(_processHandle, address, data, data.Length, out bytesWritten);
        }

        public static int ReadInt32(IntPtr address)
        {
            var buffer = new byte[4];
            ReadBytes(address, buffer, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static float ReadFloat(IntPtr address)
        {
            var buffer = new byte[4];
            ReadBytes(address, buffer, out _);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static bool WriteInt32(IntPtr address, int value)
        {
            var buffer = BitConverter.GetBytes(value);
            return WriteBytes(address, buffer, out _);
        }

        public static bool WriteFloat(IntPtr address, float value)
        {
            var buffer = BitConverter.GetBytes(value);
            return WriteBytes(address, buffer, out _);
        }

        public static bool IsAttached => _processHandle != IntPtr.Zero && _targetProcess != null && !_targetProcess.HasExited;
    }
}
