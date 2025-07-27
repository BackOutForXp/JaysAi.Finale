using System;
using System.Numerics;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.AI
{
    public interface IWorldToScreen
    {
        bool TryProject(Vector3 worldPos, out Vector2 screenPos);
    }

    public class WorldToScreenConverter : IWorldToScreen
    {
        private readonly AppSettings _settings;

        public WorldToScreenConverter(AppSettings settings)
        {
            _settings = settings;
        }

        public bool TryProject(Vector3 worldPos, out Vector2 screenPos)
        {
            screenPos = default;

            // Placeholder logic — replace with your memory-based camera matrix projection
            var fakeCameraPos = new Vector3(0, 0, 0); // assume camera is at origin
            var dir = worldPos - fakeCameraPos;

            if (dir.Z <= 0.01f)
                return false;

            float x = (_settings.ScreenWidth / 2f) + (dir.X / dir.Z) * (_settings.ScreenWidth / 2f);
            float y = (_settings.ScreenHeight / 2f) - (dir.Y / dir.Z) * (_settings.ScreenHeight / 2f);

            screenPos = new Vector2(x, y);
            return true;
        }
    }
}
