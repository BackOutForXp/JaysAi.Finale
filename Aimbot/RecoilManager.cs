// neural v3.0
using System;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public class RecoilManager
    {
        private RecoilPattern activePattern;
        private int currentShotIndex;
        private DateTime lastShotTime;

        public bool IsRecoilActive => activePattern != null;

        public void LoadPattern(RecoilPattern pattern)
        {
            activePattern = pattern;
            currentShotIndex = 0;
            lastShotTime = DateTime.MinValue;
            LogManager.Log("[RecoilManager] Loaded recoil pattern.");
        }

        public void Reset()
        {
            currentShotIndex = 0;
            lastShotTime = DateTime.MinValue;
        }

        public (float x, float y) GetRecoilOffset()
        {
            if (activePattern == null || currentShotIndex >= activePattern.Steps.Count)
                return (0, 0);

            var (x, y) = activePattern.Steps[currentShotIndex];
            return (x, y);
        }

        public void AdvanceShot()
        {
            if (activePattern == null)
                return;

            lastShotTime = DateTime.UtcNow;
            currentShotIndex = Math.Min(currentShotIndex + 1, activePattern.Steps.Count - 1);
        }

        public void ApplyRecoil(InputInjector injector)
        {
            var offset = GetRecoilOffset();
            injector.MoveMouse(offset.x, offset.y);
            AdvanceShot();
        }

        public void UnloadPattern()
        {
            activePattern = null;
            Reset();
            LogManager.Log("[RecoilManager] Recoil pattern unloaded.");
        }
    }
}
