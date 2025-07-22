//heavenly v3.0
using System;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Aim
{
    public class RecoilHandler
    {
        private readonly RecoilPattern _pattern;
        private int _shotCount;
        private float _smoothingFactor;

        public RecoilHandler(RecoilPattern pattern, float smoothingFactor = 0.85f)
        {
            _pattern = pattern;
            _smoothingFactor = Math.Clamp(smoothingFactor, 0.5f, 1f);
            _shotCount = 0;
        }

        public void ApplyRecoil()
        {
            var offset = _pattern.GetOffset(_shotCount);
            offset = Smooth(offset);
            InputInjector.MoveMouse(offset.X, offset.Y);
            _shotCount++;
        }

        public void ResetRecoil()
        {
            _shotCount = 0;
        }

        private Offset Smooth(Offset raw)
        {
            return new Offset(
                raw.X * _smoothingFactor,
                raw.Y * _smoothingFactor
            );
        }
    }

    public struct Offset
    {
        public float X, Y;
        public Offset(float x, float y) => (X, Y) = (x, y);
    }
}
