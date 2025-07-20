// File: System\OffsetMap.cs

using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    /// <summary>
    /// Represents a set of memory offsets for one game or engine version.
    /// This allows switching profiles easily if needed for updates or multi-game support.
    /// </summary>
    public static class OffsetMap
    {
        public static readonly Dictionary<string, int> Current = new()
        {
            // Example offsets – replace with real game offsets if using memory reading
            { "EntityList", 0x18AC7B8 },
            { "LocalPlayer", 0x18C52D8 },
            { "Team", 0xF4 },
            { "Health", 0x100 },
            { "Position", 0x138 },
            { "ViewAngles", 0x4D90 },
            { "BoneMatrix", 0x26A8 },
            { "Velocity", 0x140 },
            { "IsDormant", 0xED },
            { "Flags", 0x104 }
        };

        // Optional: Define per-profile if supporting more games
        public static Dictionary<string, Dictionary<string, int>> Profiles = new()
        {
            ["Default"] = Current,
            // ["CSGO"] = new Dictionary<string, int> { ... },
            // ["Valorant"] = new Dictionary<string, int> { ... }
        };
    }
}
