// Neural v3.1 — PlayerContext.cs
using System.Numerics;

namespace JaysAi.Finale.Data
{
    public class PlayerContext
    {
        public Vector3 Position { get; set; }
        public Vector3 ViewDirection { get; set; }
        public float FieldOfView { get; set; } = 90f;

        public bool IsInitialized => Position != Vector3.Zero;

        public float DistanceTo(Vector3 target)
        {
            return Vector3.Distance(Position, target);
        }

        public Vector3 DirectionTo(Vector3 target)
        {
            return Vector3.Normalize(target - Position);
        }
    }
}
