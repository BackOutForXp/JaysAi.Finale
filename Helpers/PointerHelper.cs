// Neural v3.0 — PointerHelper.cs
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Helpers
{
    public static class PointerHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public static byte[] ReadMemory(IntPtr processHandle, IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(processHandle, address, buffer, size, out _);
            return buffer;
        }

        public static T ReadStructure<T>(IntPtr processHandle, IntPtr address) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = ReadMemory(processHandle, address, size);
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public static IntPtr DereferencePointerChain(IntPtr baseAddress, IntPtr processHandle, params int[] offsets)
        {
            IntPtr currentAddress = baseAddress;
            byte[] buffer = new byte[IntPtr.Size];

            for (int i = 0; i < offsets.Length; i++)
            {
                ReadProcessMemory(processHandle, currentAddress, buffer, buffer.Length, out _);
                currentAddress = (IntPtr)(IntPtr.Size == 4
                    ? BitConverter.ToInt32(buffer, 0)
                    : BitConverter.ToInt64(buffer, 0));
                currentAddress = IntPtr.Add(currentAddress, offsets[i]);
            }

            return currentAddress;
        }
    }
}
