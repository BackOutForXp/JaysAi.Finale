// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public enum StickZoneType
    {
        Deadzone,
        Active,
        Outer
    }

    public class StickZone
    {
        public StickZoneType Type { get; }
        public float Radius { get; }

        public StickZone(StickZoneType type, float radius)
        {
            Type = type;
            Radius = radius;
        }

        public bool Contains(Vector2 position)
        {
            return position.Length() <= Radius;
        }
    }

    public class StickZoneProfile
    {
        public string ProfileName { get; set; }
        public List<StickZone> Zones { get; set; }

        public StickZoneProfile(string profileName)
        {
            ProfileName = profileName;
            Zones = new List<StickZone>();
        }

        public StickZoneType EvaluatePosition(Vector2 stickInput)
        {
            foreach (var zone in Zones)
            {
                if (zone.Contains(stickInput))
                    return zone.Type;
            }

            return StickZoneType.Outer;
        }

        public static StickZoneProfile Default => new StickZoneProfile("Default")
        {
            Zones = new List<StickZone>
            {
                new StickZone(StickZoneType.Deadzone, 0.1f),
                new StickZone(StickZoneType.Active, 0.85f),
                new StickZone(StickZoneType.Outer, 1.0f)
            }
        };
    }
}
