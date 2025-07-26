// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class FrameSnapshot
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Entity tracking at the moment of capture
        public List<TrackedTarget> TrackedEnemies { get; set; } = new();

        // Player location and aim direction at this frame
        public Vector3 PlayerPosition { get; set; }
        public Vector3 PlayerViewDirection { get; set; }

        // Mouse + aim data (for recoil or compensation correction)
        public Vector2 CursorPosition { get; set; }
        public Vector2 AimDelta { get; set; }

        // Prediction data (for aim assist + training)
        public Dictionary<Guid, Vector3> PredictedEnemyPositions { get; set; } = new();
        public Dictionary<Guid, Vector3> LastSeenEnemyPositions { get; set; } = new();

        // ESP metadata
        public bool ESPActive { get; set; }
        public string ActiveAimProfile { get; set; }

        // For ML logging / training replay
        public string FrameSource { get; set; } = "live"; // e.g., "live", "simulated", "replay"

        public FrameSnapshot Clone()
        {
            return new FrameSnapshot
            {
                Timestamp = this.Timestamp,
                TrackedEnemies = new List<TrackedTarget>(this.TrackedEnemies),
                PlayerPosition = this.PlayerPosition,
                PlayerViewDirection = this.PlayerViewDirection,
                CursorPosition = this.CursorPosition,
                AimDelta = this.AimDelta,
                PredictedEnemyPositions = new Dictionary<Guid, Vector3>(this.PredictedEnemyPositions),
                LastSeenEnemyPositions = new Dictionary<Guid, Vector3>(this.LastSeenEnemyPositions),
                ESPActive = this.ESPActive,
                ActiveAimProfile = this.ActiveAimProfile,
                FrameSource = this.FrameSource
            };
        }
    }
}
