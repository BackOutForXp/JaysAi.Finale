// neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System.Collections.Generic;

namespace JaysAi.Finale.Features
{
    public static class ESP
    {
        private static bool _isEnabled;
        private static readonly List<ESPObject> _trackedObjects = new();

        public static void Initialize()
        {
            OverlayRenderer.RegisterPersistentDraw(DrawESP);
        }

        public static void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        public static void UpdateObjects(List<Enemy> enemies)
        {
            _trackedObjects.Clear();

            foreach (var enemy in enemies)
            {
                if (enemy.IsVisible)
                {
                    _trackedObjects.Add(new ESPObject(enemy));
                }
            }
        }

        public static void Clear()
        {
            _trackedObjects.Clear();
        }

        private static void DrawESP(SkiaSharp.SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!_isEnabled) return;

            foreach (var obj in _trackedObjects)
            {
                obj.Draw(canvas, screenWidth, screenHeight);
            }
        }
    }
}
