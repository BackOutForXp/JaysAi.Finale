// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace JaysAi.Finale.Features
{
    public static class ESP
    {
        private static bool _isEnabled;
        private static List<ESPObject> _trackedObjects = new();

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

        private static void DrawESP(DrawingContext context, double screenWidth, double screenHeight)
        {
            if (!_isEnabled) return;

            foreach (var obj in _trackedObjects)
            {
                obj.Draw(context, screenWidth, screenHeight);
            }
        }

        public static void Clear()
        {
            _trackedObjects.Clear();
        }
    }
}
