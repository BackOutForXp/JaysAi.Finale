//heavenly v3.0
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.AI
{
    public static class ZoneManager
    {
        private static readonly List<Zone> ActiveZones = new();

        public static void UpdateZones(List<YoloBoundingBox> detectedObjects)
        {
            ActiveZones.Clear();

            foreach (var obj in detectedObjects)
            {
                if (obj.IsEnemy)
                {
                    var zone = new Zone
                    {
                        Type = ZoneType.Danger,
                        Area = new Rect(obj.X - 10, obj.Y - 10, obj.Width + 20, obj.Height + 20),
                        Label = "ENEMY ZONE"
                    };

                    ActiveZones.Add(zone);
                }
            }
        }

        public static bool IsInDangerZone(Point point)
        {
            return ActiveZones.Any(zone =>
                zone.Type == ZoneType.Danger && zone.Area.Contains(point));
        }

        public static void DrawZones()
        {
            foreach (var zone in ActiveZones)
            {
                OverlayDrawer.DrawRectangle(
                    zone.Area.X,
                    zone.Area.Y,
                    zone.Area.Width,
                    zone.Area.Height,
                    OverlayColor.Orange,
                    label: zone.Label
                );
            }
        }
    }

    public enum ZoneType
    {
        Neutral,
        Danger,
        TargetPriority
    }

    public class Zone
    {
        public ZoneType Type { get; set; }
        public Rect Area { get; set; }
        public string Label { get; set; }
    }
}
