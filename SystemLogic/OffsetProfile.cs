// File: System\OffsetProfile.cs

using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    /// <summary>
    /// Represents a named offset profile used by the loader.
    /// You can define multiple profiles for different games or patch versions.
    /// </summary>
    public class OffsetProfile
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, int> Offsets { get; set; } = new();

        public OffsetProfile() { }

        public OffsetProfile(string name, Dictionary<string, int> offsets)
        {
            Name = name;
            Offsets = offsets;
        }

        public int GetOffset(string key)
        {
            return Offsets.TryGetValue(key, out int value) ? value : -1;
        }
    }
}
