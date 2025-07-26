// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;
using System.Numerics;
using static Unity.Storage.RegistrationSet;

namespace JaysAi.Finale.Aim
{
    public class RecoilHandler
    {
        private Vector2 _currentRecoilOffset;
        private Vector2 _targetRecoilOffset;
        private float _recoilSmoothFactor = 0.12f;

        public void ApplyRecoilCompensation(Entity player, Vector2 weaponRecoil, float deltaTime)
        {
            if (weaponRecoil == Vector2.Zero || player == null)
                return;

            _targetRecoilOffset = weaponRecoil;
            _currentRecoilOffset = Vector2.Lerp(_currentRecoilOffset, _targetRecoilOffset, _recoilSmoothFactor * deltaTime);

            Vector2 compensation = -_currentRecoilOffset;
            InputAdjuster.ApplyMouseOffset(compensation);
        }

        public void Reset()
        {
            _currentRecoilOffset = Vector2.Zero;
            _targetRecoilOffset = Vector2.Zero;
        }

        public void SetSmoothFactor(float smooth)
        {
            _recoilSmoothFactor = Math.Clamp(smooth, 0.01f, 1f);
        }

        public Vector2 GetCompensationVector()
        {
            return -_currentRecoilOffset;
        }
    }
}
