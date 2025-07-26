// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Modules
{
    public class RecoilCompensator
    {
        private readonly IInputSource _inputSource;
        private readonly Timer _compensateTimer = new();
        private float _intensity;
        private bool _isActive;

        public float CompensationX { get; set; } = 0f;
        public float CompensationY { get; set; } = 0f;
        public int CompensationIntervalMs { get; set; } = 10;

        public RecoilCompensator(IInputSource inputSource)
        {
            _inputSource = inputSource;
        }

        public void SetIntensity(float value)
        {
            _intensity = Math.Clamp(value, 0f, 1f);
        }

        public void Start()
        {
            _isActive = true;
            _compensateTimer.Restart();
        }

        public void Stop()
        {
            _isActive = false;
            _compensateTimer.Stop();
        }

        public void Update()
        {
            if (!_isActive || !_compensateTimer.HasElapsed(CompensationIntervalMs)) return;

            int moveX = (int)(CompensationX * _intensity);
            int moveY = (int)(CompensationY * _intensity);

            _inputSource.InjectMouseMovement(moveX, moveY);
            _compensateTimer.Restart();
        }
    }
}
