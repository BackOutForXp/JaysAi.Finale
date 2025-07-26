// neural v3.0
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public class SnapAssist
    {
        private readonly SnapConfig config;
        private readonly InputInjector injector;
        private readonly TargetSelector selector;

        public SnapAssist(SnapConfig config, InputInjector injector, TargetSelector selector)
        {
            this.config = config;
            this.injector = injector;
            this.selector = selector;
        }

        public void Execute(TargetInfo target, bool isFiring)
        {
            if (target == null || !target.IsValid || !isFiring || !config.IsEnabled)
                return;

            var (dx, dy) = CalculateSnapOffset(target);
            injector.MoveMouse(dx * config.Sensitivity, dy * config.Sensitivity);
        }

        private (float dx, float dy) CalculateSnapOffset(TargetInfo target)
        {
            var screenCenter = ScreenManager.GetScreenCenter();
            float deltaX = target.ScreenPosition.X - screenCenter.X;
            float deltaY = target.ScreenPosition.Y - screenCenter.Y;

            return (deltaX, deltaY);
        }

        public void AutoSnapIfReady(bool isFiring)
        {
            var bestTarget = selector.GetBestTarget();
            Execute(bestTarget, isFiring);
        }
    }
}
