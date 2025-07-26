// neural v3.0
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class InterceptionHelper
    {
        private const string InterceptionDll = "interception.dll";

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr interception_create_context();

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void interception_destroy_context(IntPtr context);

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void interception_set_filter(IntPtr context, Predicate predicate, ushort filter);

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int interception_receive(IntPtr context, int device, ref Stroke stroke, uint n);

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int interception_send(IntPtr context, int device, ref Stroke stroke, uint n);

        [DllImport(InterceptionDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort interception_get_hardware_id(IntPtr context, int device, byte[] hardwareIdBuffer, uint bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Predicate(int device);

        [StructLayout(LayoutKind.Explicit)]
        public struct Stroke
        {
            [FieldOffset(0)] public MouseStroke Mouse;
            [FieldOffset(0)] public KeyStroke Key;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseStroke
        {
            public ushort State;
            public ushort Flags;
            public short Rolling;
            public short X;
            public short Y;
            public uint Information;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyStroke
        {
            public ushort Code;
            public ushort State;
            public uint Information;
        }

        public enum InterceptionFilter : ushort
        {
            None = 0,
            Mouse = 1,
            Keyboard = 2
        }
    }
}
