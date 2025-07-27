// Neural v3.1 — WeaponSwayCompensator.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Aimbot
{
    public class WeaponSwayCompensator
    {
        private Vector2 _previousMouseDelta = Vector2.Zero;
        private float _swayFactor = 0.5f;

        public Vector2 Compensate(Vector2 currentDelta)
        {
            var compensation = currentDelta - (_previousMouseDelta * _swayFactor);
            _previousMouseDelta = currentDelta;
            return compensation;
        }

        public void SetSwayFactor(float factor)
        {
            _swayFactor = Math.Clamp(factor, 0f, 1f);
        }

        public void Reset()
        {
            _previousMouseDelta = Vector2.Zero;
        }
    }
}
