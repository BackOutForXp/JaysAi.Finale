// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Aimbot
{
    public class SnapController
    {
        private readonly SnapConfig config;
        private readonly InputInjector injector;
        private readonly TargetSelector selector;
        private DateTime lastSnapTime;

        public SnapController(SnapConfig config, InputInjector injector, TargetSelector selector)
        {
            this.config = config;
            this.injector = injector;
            this.selector = selector;
            this.lastSnapTime = DateTime.MinValue;
        }

        public void Update()
        {
            if (!config.IsEnabled || selector == null)
                return;

            var target = selector.GetBestTarget(config.SnapFOV, config.HeadOnly, config.PrioritizeVisibleTargets);
            if (target == null || !CanSnapNow())
                return;

            var screenPosition = ViewpointTranslator.WorldToScreen(target.Position);
            if (!screenPosition.IsValid)
                return;

            Vector2 delta = CalculateDelta(screenPosition);
            if (config.SmoothingFactor > 0)
                delta = VectorMathHelper.Smooth(delta, config.SmoothingFactor);

            injector.MoveMouse(delta);
            lastSnapTime = DateTime.UtcNow;
        }

        private Vector2 CalculateDelta(Vector2 screenPos)
        {
            var center = ScreenUtils.GetScreenCenter();
            return new Vector2(screenPos.X - center.X, screenPos.Y - center.Y) * config.Sensitivity;
        }

        private bool CanSnapNow()
        {
            var elapsed = DateTime.UtcNow - lastSnapTime;
            return elapsed.TotalSeconds >= config.SnapCooldown;
        }
    }
}
