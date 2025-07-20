// File: Modules/AimAssistModule.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JaysAi.Finale.Modules
{
    public class AimAssistModule : IModule
    {
        public bool IsEnabled { get; private set; }

        private AppSettings _settings => SettingsManager<AppSettings>.Current;

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;

        public void Update(Vector2 playerScreenPos, IEnumerable<Enemy> enemies)
        {
            if (!IsEnabled || !_settings.AimAssistEnabled || enemies == null)
                return;

            var validTargets = enemies
                .Where(e => e.IsVisible && e.IsEnemy && e.ScreenPosition != Vector2.Zero)
                .ToList();

            if (!validTargets.Any())
                return;

            var bestTarget = TargetSelector.GetBestTarget(validTargets, playerScreenPos, _settings.AimAssistFov);

            if (bestTarget == null)
                return;

            var predictedPos = PredictionHelper.Predict2D(
                bestTarget.ScreenPosition,
                bestTarget.ScreenVelocity,
                playerScreenPos,
                _settings.AimAssistBulletSpeed
            );

            if (!MonarchAimAI.IsTargetWithinFov(playerScreenPos, predictedPos))
                return;

            var aimPoint = MonarchAimAI.GetCorrectedAim(playerScreenPos, predictedPos);

            InputEmulator.MoveMouseTo(aimPoint, playerScreenPos, _settings.AimAssistSmoothing);
        }
    }
}
