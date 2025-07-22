//heavenly v3.0
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Aimbot
{
    public static class RecoilManager
    {
        private static float _horizontalCompensation;
        private static float _verticalCompensation;
        private static DateTime _lastShotTime;
        private static bool _isRecoiling;

        public static void UpdateRecoil(bool isFiring)
        {
            if (isFiring)
            {
                ApplyRecoilCompensation();
                _isRecoiling = true;
                _lastShotTime = DateTime.UtcNow;
            }
            else if (_isRecoiling && TimeSinceLastShot() > 0.15)
            {
                ResetRecoil();
            }
        }

        private static void ApplyRecoilCompensation()
        {
            _horizontalCompensation = WeaponProfile.Active.HorizontalRecoil;
            _verticalCompensation = WeaponProfile.Active.VerticalRecoil;

            MouseEmulator.MoveMouseRelative(
                x: (int)(-_horizontalCompensation),
                y: (int)(-_verticalCompensation)
            );

            Logger.LogDebug($"[RecoilManager] Applied recoil compensation: H={_horizontalCompensation}, V={_verticalCompensation}");
        }

        private static double TimeSinceLastShot()
        {
            return (DateTime.UtcNow - _lastShotTime).TotalSeconds;
        }

        private static void ResetRecoil()
        {
            _horizontalCompensation = 0;
            _verticalCompensation = 0;
            _isRecoiling = false;

            Logger.LogDebug("[RecoilManager] Recoil reset.");
        }
    }
}
