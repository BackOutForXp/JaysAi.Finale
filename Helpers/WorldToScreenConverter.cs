// Neural v3.1 — WorldToScreenConverter.cs
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class WorldToScreenConverter
    {
        public static Vector2? Project(Vector3 worldPosition)
        {
            // Placeholder projection logic — replace with actual game matrix later
            if (worldPosition.Z <= 0.1f)
                return null;

            float screenX = worldPosition.X / worldPosition.Z * 100 + 960; // assume screen center at 960x540
            float screenY = worldPosition.Y / worldPosition.Z * 100 + 540;

            return new Vector2(screenX, screenY);
        }
    }
}
