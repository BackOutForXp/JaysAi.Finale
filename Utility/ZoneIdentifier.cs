// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public enum ZoneType
    {
        Center,
        InnerRing,
        OuterRing,
        Edge,
        Unknown
    }

    public static class ZoneIdentifier
    {
        public static ZoneType Identify(Vector2 position, Vector2 screenCenter, float screenRadius)
        {
            float distance = Vector2.Distance(position, screenCenter);
            float normalized = distance / screenRadius;

            if (normalized < 0.2f)
                return ZoneType.Center;
            if (normalized < 0.5f)
                return ZoneType.InnerRing;
            if (normalized < 0.8f)
                return ZoneType.OuterRing;
            if (normalized <= 1.0f)
                return ZoneType.Edge;

            return ZoneType.Unknown;
        }

        public static string Describe(ZoneType zone)
        {
            return zone switch
            {
                ZoneType.Center => "Center Zone (High Priority)",
                ZoneType.InnerRing => "Inner Ring (Focus Area)",
                ZoneType.OuterRing => "Outer Ring (Engagement Zone)",
                ZoneType.Edge => "Edge (Peripheral Detection)",
                _ => "Unknown Zone"
            };
        }

        public static ZoneType IdentifyFromScreenPercentage(float xPercent, float yPercent)
        {
            float dx = Math.Abs(xPercent - 0.5f);
            float dy = Math.Abs(yPercent - 0.5f);
            float radial = (float)Math.Sqrt(dx * dx + dy * dy);

            if (radial < 0.2f)
                return ZoneType.Center;
            if (radial < 0.5f)
                return ZoneType.InnerRing;
            if (radial < 0.8f)
                return ZoneType.OuterRing;
            if (radial <= 1.0f)
                return ZoneType.Edge;

            return ZoneType.Unknown;
        }
    }
}
