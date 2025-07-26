// neural v3.0
using System;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class SnapScore
    {
        public Enemy Target { get; }
        public float DistanceScore { get; }
        public float MovementScore { get; }
        public float VisibilityScore { get; }
        public float AlignmentScore { get; }
        public float TotalScore => DistanceScore * 0.25f +
                                   MovementScore * 0.25f +
                                   VisibilityScore * 0.25f +
                                   AlignmentScore * 0.25f;

        public SnapScore(Enemy target, float distanceScore, float movementScore, float visibilityScore, float alignmentScore)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            DistanceScore = distanceScore;
            MovementScore = movementScore;
            VisibilityScore = visibilityScore;
            AlignmentScore = alignmentScore;
        }

        public override string ToString()
        {
            return $"[SnapScore] ID: {Target.ID}, Total: {TotalScore:F2}, D:{DistanceScore:F2}, M:{MovementScore:F2}, V:{VisibilityScore:F2}, A:{AlignmentScore:F2}";
        }
    }
}
