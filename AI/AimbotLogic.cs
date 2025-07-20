//monarch v2.1 – Core Aimbot Engine
using System;
using System.Collections.Generic;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public static class AimbotLogic
    {
        public static bool IsActive => FeatureToggle.AimbotEnabled;

        public static void Run(List<TargetInfo> currentTargets, int screenWidth, int screenHeight)
        {
            if (!IsActive || currentTargets == null || currentTargets.Count == 0)
                return;

            var target = SnapAssist.GetBestTarget(currentTargets, screenWidth, screenHeight, FeatureToggle.SnapFOV);

            if (target == null) return;

            AimAtTarget(target, screenWidth, screenHeight);
        }

        private static void AimAtTarget(TargetInfo target, int screenWidth, int screenHeight)
        {
            double centerX = screenWidth / 2;
            double centerY = screenHeight / 2;

            double deltaX = target.CenterX - centerX;
            double deltaY = target.CenterY - centerY;

            if (FeatureToggle.SmoothAim)
            {
                deltaX /= FeatureToggle.Smoothness;
                deltaY /= FeatureToggle.Smoothness;
            }

            InputInjector.MoveMouseBy((int)deltaX, (int)deltaY);
        }
    }
}
