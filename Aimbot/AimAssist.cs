// Neural v3.1 — AimAssist.cs
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public class AimAssist
    {
        private readonly PredictionEngine _predictionEngine;
        private readonly TargetSelector _targetSelector;
        private readonly InputEmulator _inputEmulator;
        private readonly PIDController _aimSmoother;
        private readonly AppSettings _settings;

        private SnapTarget? _currentTarget;
        private bool _isActive;

        public AimAssist(PredictionEngine predictionEngine, TargetSelector targetSelector, InputEmulator inputEmulator, AppSettings settings)
        {
            _predictionEngine = predictionEngine;
            _targetSelector = targetSelector;
            _inputEmulator = inputEmulator;
            _settings = settings;

            _aimSmoother = new PIDController(0.6f, 0.01f, 0.05f);
            _isActive = true;
        }

        public void Toggle(bool state) => _isActive = state;

        public void Update()
        {
            if (!_isActive || !_settings.AimAssistEnabled)
                return;

            _currentTarget = _targetSelector.GetBestTarget();

            if (_currentTarget == null || !_currentTarget.Value.IsValid())
                return;

            Vector2 predictedPosition = _predictionEngine.PredictTargetPosition(_currentTarget.Value);
            Vector2 aimOffset = _predictionEngine.CalculateAimOffset(predictedPosition);

            Vector2 smoothedOffset = _aimSmoother.ApplySmoothing(Vector2.Zero, aimOffset);
            _inputEmulator.MoveMouseBy(smoothedOffset);
        }
    }
}
