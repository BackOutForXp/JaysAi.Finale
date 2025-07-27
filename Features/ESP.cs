using JaysAi.Finale.AI;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Modules
{
    public class ESP : IModule
    {
        private readonly AppSettings _settings;
        private readonly EnemyScanner _scanner;
        private readonly EspObject _espObject;
        private readonly IWorldToScreen _w2s;
        private List<Enemy> _visibleEnemies = new();

        public ESP(AppSettings settings, EnemyScanner scanner)
        {
            _settings = settings;
            _scanner = scanner;
            _espObject = new EspObject(settings);
            _w2s = settings.WorldToScreenConverter;
        }

        public void Initialize()
        {
            // Optionally preload scanner data
        }

        public void Shutdown()
        {
            _visibleEnemies.Clear();
        }

        public void Tick()
        {
            if (!_settings.EnableESP) return;

            _scanner.Scan();
            _visibleEnemies = _scanner.VisibleEnemies;
        }

        public void Draw(SKCanvas canvas, SKPaint paint)
        {
            if (!_settings.EnableESP || _visibleEnemies == null)
                return;

            foreach (var enemy in _visibleEnemies)
            {
                if (!enemy.IsAlive) continue;

                if (_w2s.TryProject(enemy.Position, out var screenPos))
                {
                    enemy.ScreenPosition = screenPos;
                    _espObject.Draw(canvas, paint, enemy, screenPos);
                }
            }
        }
    }
}
