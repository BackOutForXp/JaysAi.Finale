//monarch v2.1 – AI Tactical Zone Visual Overlay
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using JaysAi.Finale.AI;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Visuals
{
    public class MapOverlay
    {
        private readonly ZoneManager _zoneManager;

        public MapOverlay(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }

        public void DrawZones(DrawingContext dc)
        {
            if (dc == null || _zoneManager == null) return;

            var zones = _zoneManager.GetDangerZones();

            foreach (var zone in zones)
            {
                var color = GetZoneColor(zone.DangerLevel);
                var rect = new Rect(zone.X, zone.Y, zone.Width, zone.Height);

                dc.DrawRectangle(new SolidColorBrush(color), null, rect);

                FormattedText text = new FormattedText(
                    zone.Name,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"),
                    10,
                    Brushes.White,
                    1.25);

                dc.DrawText(text, new Point(zone.X + 4, zone.Y + 4));
            }
        }

        private Color GetZoneColor(int dangerLevel)
        {
            return dangerLevel switch
            {
                >= 9 => Colors.Red,
                >= 7 => Colors.Orange,
                >= 5 => Colors.Yellow,
                _ => Colors.Green
            };
        }
    }
}
