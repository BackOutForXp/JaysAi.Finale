// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    public class OffsetProfile
    {
        public string GameName { get; set; } = string.Empty;
        public Dictionary<string, IntPtr> Offsets { get; set; } = new();

        public OffsetProfile(string gameName)
        {
            GameName = gameName;
        }

        public void SetOffset(string key, IntPtr value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            Offsets[key] = value;
        }

        public IntPtr GetOffset(string key)
        {
            if (Offsets.TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException($"Offset key '{key}' not found in profile: {GameName}");
        }

        public bool TryGetOffset(string key, out IntPtr value)
        {
            return Offsets.TryGetValue(key, out value);
        }

        public IReadOnlyDictionary<string, IntPtr> GetAll()
        {
            return Offsets;
        }

        public void Clear()
        {
            Offsets.Clear();
        }

        public override string ToString()
        {
            return $"OffsetProfile: {GameName} | {Offsets.Count} offsets";
        }
    }
}
