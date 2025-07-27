// Neural v3.1 — TeamFilter.cs
using JaysAi.Finale.AI.Models;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.AI
{
    public class TeamFilter
    {
        public bool IsTeamFilterEnabled => UserSettings.Instance.Get("TeamFilterEnabled", true);

        public List<TrackedTarget> FilterEnemies(List<TrackedTarget> allTargets)
        {
            if (!IsTeamFilterEnabled)
                return allTargets;

            return allTargets.Where(t => !IsTeammate(t)).ToList();
        }

        private bool IsTeammate(TrackedTarget target)
        {
            if (target.Profile.TeamId == null)
                return false;

            var localTeamId = MemoryScanner.LocalPlayer?.TeamId;
            return localTeamId != null && localTeamId == target.Profile.TeamId;
        }
    }
}
