//neural v3.0
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Modules
{
    public sealed class SnapZoneConfig
    {
        public List<SnapZone> Zones { get; private set; } = new();

        public void AddZone(Vector3 center, Vector3 size, string name = "Unnamed", bool isPriority = false)
        {
            Zones.Add(new SnapZone
            {
                Center = center,
                Dimensions = size,
                ZoneName = name,
                IsPriorityZone = isPriority
            });
        }

        public void ClearZones() => Zones.Clear();

        public SnapZone? GetPrimaryZone()
        {
            foreach (var zone in Zones)
            {
                if (zone.IsPriorityZone)
                    return zone;
            }

            return Zones.Count > 0 ? Zones[0] : null;
        }

        public IEnumerable<SnapZone> GetAllZones() => Zones;
    }
}
