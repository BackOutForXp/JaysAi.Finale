// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Modules
{
    public class RecoilAssist
    {
        private readonly IInputSource _inputSource;
        private readonly RecoilProfile _profile;
        private readonly Timer _recoilTimer;
        private int _stepIndex;
        private bool _active;

        public RecoilAssist(IInputSource inputSource, RecoilProfile profile)
        {
            _inputSource = inputSource;
            _profile = profile;
            _recoilTimer = new Timer();
        }

        public void Start()
        {
            _active = true;
            _stepIndex = 0;
            _recoilTimer.Restart();
        }

        public void Stop()
        {
            _active = false;
            _stepIndex = 0;
            _recoilTimer.Stop();
        }

        public void Update()
        {
            if (!_active || !_recoilTimer.HasElapsed(_profile.DelayBetweenSteps)) return;

            if (_stepIndex >= _profile.Steps.Length)
            {
                Stop();
                return;
            }

            var step = _profile.Steps[_stepIndex];
            _inputSource.InjectMouseMovement(step.X, step.Y);
            _stepIndex++;
            _recoilTimer.Restart();
        }

        public void SetProfile(RecoilProfile profile)
        {
            _profile.Steps = profile.Steps;
            _profile.DelayBetweenSteps = profile.DelayBetweenSteps;
        }
    }

    public class RecoilProfile
    {
        public RecoilStep[] Steps { get; set; } = Array.Empty<RecoilStep>();
        public int DelayBetweenSteps { get; set; } = 15; // ms
    }

    public struct RecoilStep
    {
        public int X;
        public int Y;

        public RecoilStep(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
