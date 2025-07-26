// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using System;
using static Unity.Storage.RegistrationSet;

namespace JaysAi.Finale.Aim
{
    public class LineOfSightChecker
    {
        private readonly float maxScanDistance;
        private readonly Func<Entity, bool> visibilityFilter;

        public LineOfSightChecker(float maxDistance = 250f, Func<Entity, bool>? customFilter = null)
        {
            maxScanDistance = maxDistance;
            visibilityFilter = customFilter ?? DefaultVisibilityFilter;
        }

        private bool DefaultVisibilityFilter(Entity entity)
        {
            return entity.Health > 0 && entity.IsEnemy && entity.IsVisible;
        }

        public bool HasLineOfSight(Entity localPlayer, Entity target)
        {
            if (localPlayer == null || target == null)
                return false;

            if (!visibilityFilter(target))
                return false;

            float distance = VectorMathHelper.Distance(localPlayer.Position, target.Position);
            return distance <= maxScanDistance;
        }

        public bool IsBlockedByCover(Entity target)
        {
            // Placeholder: In future, raycast or occlusion logic can be applied
            return !target.IsVisible;
        }
    }
}
