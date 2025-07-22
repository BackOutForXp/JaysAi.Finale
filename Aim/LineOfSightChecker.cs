//heavenly v3.0
using System;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aim
{
    public static class LineOfSightChecker
    {
        public static bool IsTargetVisible(TargetInfo target)
        {
            if (target == null || target.Position == null)
                return false;

            // Raycast or bounding box visibility logic (placeholder)
            return !target.IsObstructed; // This field should be updated by ESP module or vision model
        }

        public static bool IsLineOfSightClear(TargetInfo from, TargetInfo to)
        {
            if (from == null || to == null) return false;

            // Placeholder for line trace, should be connected to prediction model or overlay logic
            return !from.IsObstructed && !to.IsObstructed;
        }

        public static bool IsTargetBehindCover(TargetInfo target)
        {
            if (target == null) return false;

            return target.IsBehindCover; // Should be dynamically updated by ESPScanner or camera logic
        }
    }
}
