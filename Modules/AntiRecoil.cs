// Neural v3.0 — AntiRecoil.cs
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules.Config;

namespace JaysAi.Finale.Modules
{
    public sealed class AntiRecoil : IAimAssistModule
    {
        private float _recoilOffsetX;
        private float _recoilOffsetY;

        public AntiRecoil()
        {
            _recoilOffsetX = AntiRecoilConfig.DefaultOffsetX;
            _recoilOffsetY = AntiRecoilConfig.DefaultOffsetY;
        }

        public void Apply(InputState input, FramePrediction prediction)
        {
            if (!AntiRecoilConfig.Enabled || !input.IsFiring)
                return;

            float adjustedX = -_recoilOffsetX * AntiRecoilConfig.Strength;
            float adjustedY = -_recoilOffsetY * AntiRecoilConfig.Strength;

            input.AdjustStick(adjustedX, adjustedY);

            if (AntiRecoilConfig.DynamicAdjustment && prediction.HasRecoilPattern)
            {
                var dynamic = prediction.RecoilPattern;
                input.AdjustStick(dynamic.X, dynamic.Y);
            }
        }

        public void UpdateOffsets(float x, float y)
        {
            _recoilOffsetX = x;
            _recoilOffsetY = y;
        }
    }

    public static class AntiRecoilConfig
    {
        public static bool Enabled { get; set; } = true;
        public static float Strength { get; set; } = 1.0f;
        public static bool DynamicAdjustment { get; set; } = true;

        public static float DefaultOffsetX { get; set; } = 0.35f;
        public static float DefaultOffsetY { get; set; } = 0.6f;
    }
}
