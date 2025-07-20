//monarch v2.1 – Progressive Memory Snapshot Engine
using System;
using System.Collections.Generic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public class ProgressMemoryReader
    {
        private readonly IMemoryProvider _memory;
        private readonly Dictionary<string, IntPtr> _cachedAddresses;

        public ProgressMemoryReader(IMemoryProvider memoryProvider)
        {
            _memory = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            _cachedAddresses = new Dictionary<string, IntPtr>();
        }

        public T Read<T>(string label, IntPtr basePtr, int[] offsets) where T : struct
        {
            if (!_cachedAddresses.TryGetValue(label, out var targetAddress))
            {
                targetAddress = ResolveAddress(basePtr, offsets);
                _cachedAddresses[label] = targetAddress;
            }

            return _memory.Read<T>(targetAddress);
        }

        public void ForceRefresh(string label, IntPtr basePtr, int[] offsets)
        {
            _cachedAddresses[label] = ResolveAddress(basePtr, offsets);
        }

        private IntPtr ResolveAddress(IntPtr baseAddress, int[] offsets)
        {
            var currentAddress = _memory.Read<IntPtr>(baseAddress);

            foreach (var offset in offsets)
            {
                currentAddress = _memory.Read<IntPtr>(currentAddress + offset);
            }

            return currentAddress;
        }

        public bool IsValid(IntPtr address)
        {
            return address != IntPtr.Zero && _memory.IsReadable(address);
        }

        public void ClearCache()
        {
            _cachedAddresses.Clear();
        }
    }
}
