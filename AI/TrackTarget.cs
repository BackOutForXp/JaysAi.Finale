// Neural v3.1 — TrackTarget.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.AI
{
    public class TrackTarget
    {
        private readonly List<TrackedTarget> _targets = new();

        public void Initialize()
        {
            _targets.Clear();
        }

        public void UpdateTracking()
        {
            var rawObjects = EnemyScanner.Scan();

            _targets.Clear();
            foreach (var obj in rawObjects)
            {
                if (!WorldToScreenConverter.TryConvert(obj.WorldPosition, out var screenPos))
                    continue;

                var target = new TrackedTarget
                {
                    Id = obj.Id,
                    Position3D = obj.WorldPosition,
                    ScreenPosition = screenPos,
                    IsVisible = obj.IsVisible,
                    Health = obj.Health,
                    Distance = obj.Distance,
                    Velocity = obj.Velocity,
                    ScreenBox = BoundingBoxHelper.GetBoundingBox(screenPos, UserSettings.Instance.Get<float>("EspBoxSize", 50f))
                };

                _targets.Add(target);
            }
        }

        public List<TrackedTarget> GetTargets()
        {
            return _targets.ToList();
        }

        public TrackedTarget? GetNearestVisible()
        {
            return _targets
                .Where(t => t.IsVisible)
                .OrderBy(t => t.Distance)
                .FirstOrDefault();
        }
    }
}
