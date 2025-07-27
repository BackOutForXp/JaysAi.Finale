using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using System.Numerics;

namespace JaysAi.Finale.Models
{
    public class StickAssist : IModule
    {
        private readonly AppSettings _settings;
        private readonly EnemyScanner _scanner;
        private readonly InputEmulator _emulator;

        public StickAssist(AppSettings settings)
        {
            _settings = settings;
            _scanner = new EnemyScanner(settings);
            _emulator = new InputEmulator();
        }

        public void Initialize()
        {
            // Optionally log or preload inputs
        }

        public void Shutdown()
        {
            // Cleanup or zero input if needed
        }

        public void Tick()
        {
            if (!_settings.EnableStickAssist) return;

            _scanner.Scan();
            var closest = _scanner.GetClosestVisible();

            if (closest != null)
            {
                Vector2 screenCenter = new(_settings.ScreenWidth / 2f, _settings.ScreenHeight / 2f);
                if (_settings.WorldToScreenConverter.TryProject(closest.Position, out Vector2 targetPos))
                {
                    Vector2 delta = targetPos - screenCenter;
                    Vector2 adjusted = delta * _settings.StickAssistStrength;

                    _emulator.MoveAnalog(adjusted);
                }
            }
        }
    }
}
