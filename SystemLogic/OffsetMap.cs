// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    public static class OffsetMap
    {
        private static readonly Dictionary<string, IntPtr> Offsets = new();

        public static void SetOffset(string key, IntPtr value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            Offsets[key] = value;
        }

        public static IntPtr GetOffset(string key)
        {
            if (Offsets.TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException($"Offset not found for key: {key}");
        }

        public static bool TryGetOffset(string key, out IntPtr offset)
        {
            return Offsets.TryGetValue(key, out offset);
        }

        public static void Clear()
        {
            Offsets.Clear();
        }

        public static IReadOnlyDictionary<string, IntPtr> GetAllOffsets()
        {
            return Offsets;
        }

        public static void InitializeDefaultOffsets()
        {
            Offsets.Clear();

            // Example placeholders – customize these per game memory structure
            Offsets["PlayerBase"] = new IntPtr(0x01A2B3C4);
            Offsets["ViewMatrix"] = new IntPtr(0x02F4D1E8);
            Offsets["EntityList"] = new IntPtr(0x03C1F780);
            Offsets["LocalPlayer"] = new IntPtr(0x0198F0DC);
        }
    }
}
