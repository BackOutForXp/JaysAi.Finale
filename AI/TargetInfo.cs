// Neural v3.1 — TargetInfo.cs
using System;
using System.Numerics;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class TargetInfo
    {
        public Enemy Target { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 LastKnownDirection { get; set; }

        public float Distance { get; set; }
        public bool IsVisible { get; set; }
        public bool IsAlive { get; set; }
        public DateTime LastSeen { get; set; }

        // Scoring for prioritization
        public float AimWeightScore { get; set; }
        public float VisibilityScore { get; set; }

        // Extra prediction variables
        public Vector3 PredictedPosition { get; set; }
        public float LeadTime { get; set; }

        public TargetInfo(Enemy target, Vector3 position)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Position = position;
            Velocity = Vector3.Zero;
            LastKnownDirection = Vector3.Zero;
            Distance = 0f;
            IsVisible = false;
            IsAlive = true;
            LastSeen = DateTime.UtcNow;

            AimWeightScore = 0f;
            VisibilityScore = 0f;
            PredictedPosition = Vector3.Zero;
            LeadTime = 0f;
        }

        public override string ToString()
        {
            return $"Target[{Target.ID}] Pos={Position}, Visible={IsVisible}, Distance={Distance:F1}, Score={AimWeightScore:F2}";
        }
    }
}
