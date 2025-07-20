//monarch v2.1 – Adaptive Recoil Compensation Engine
using System.Numerics;

namespace JaysAi.Finale.Aimbot
{
    public class RecoilManager
    {
        private float _recoilVertical = 1.2f;
        private float _recoilHorizontal = 0.3f;
        private float _smoothing = 0.65f;

        private Vector2 _previousCompensation = Vector2.Zero;

        public bool IsEnabled { get; set; } = true;
        public float DragScale { get; set; } = 1.0f;

        public Vector2 CalculateCompensation(Vector2 currentRecoil)
        {
            if (!IsEnabled)
                return Vector2.Zero;

            Vector2 compensated = new Vector2(
                -currentRecoil.X * _recoilHorizontal * DragScale,
                -currentRecoil.Y * _recoilVertical * DragScale
            );

            Vector2 smoothed = Vector2.Lerp(_previousCompensation, compensated, _smoothing);
            _previousCompensation = smoothed;

            return smoothed;
        }

        public void Reset()
        {
            _previousCompensation = Vector2.Zero;
        }

        public void SetTuning(float vertical, float horizontal, float smoothing)
        {
            _recoilVertical = vertical;
            _recoilHorizontal = horizontal;
            _smoothing = smoothing;
        }
    }
}
