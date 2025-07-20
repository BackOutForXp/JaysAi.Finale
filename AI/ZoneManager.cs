//monarch v2.1 – AI Zone Intelligence Manager
using JaysAi.Finale.AI.Models;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.AI
{
    public class ZoneManager
    {
        private readonly List<Zone> _zones = new();

        public void AddZone(string name, int x, int y, int width, int height, int dangerLevel)
        {
            _zones.Add(new Zone
            {
                Name = name,
                X = x,
                Y = y,
                Width = width,
                Height = height,
                DangerLevel = dangerLevel
            });
        }

        public void ClearZones() => _zones.Clear();

        public Zone GetZoneForTarget(YoloTarget target)
        {
            return _zones.FirstOrDefault(zone =>
                target.X >= zone.X &&
                target.X <= zone.X + zone.Width &&
                target.Y >= zone.Y &&
                target.Y <= zone.Y + zone.Height);
        }

        public List<Zone> GetDangerZones(int threshold = 7)
        {
            return _zones.Where(z => z.DangerLevel >= threshold).ToList();
        }

        public void LogZones()
        {
            foreach (var zone in _zones)
            {
                Logger.Debug($"Zone: {zone.Name} | Danger: {zone.DangerLevel}");
            }
        }
    }

    public class Zone
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DangerLevel { get; set; }
    }
}
