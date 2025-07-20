//monarch v2.0
using System;
using JaysAi.InputSystem;
using JaysAi.Finale.src.Aim;
using JaysAi.AI;
using JaysAi.Finale.src.Visuals;

namespace JaysAi.Finale.AI
{
    public class AiManager
    {
        private readonly ESPModule _esp;
        private readonly SnapAssist _snap;
        private readonly PredictionEngine _predictor;
        private readonly AiMemory _memory;

        private readonly float _screenCenterX;
        private readonly float _screenCenterY;

        public AiManager(float screenWidth, float screenHeight)
        {
            _esp = new ESPModule();
            _snap = new SnapAssist();
            _predictor = new PredictionEngine();
            _memory = new AiMemory();

            _screenCenterX = screenWidth / 2f;
            _screenCenterY = screenHeight / 2f;
        }

        public void UpdateFrameTargets(System.Collections.Generic.List<ESPModule.DetectedTarget> targets)
        {
            _esp.UpdateTargets(targets);
        }

        public void Execute()
        {
            var best = _esp.GetBestTarget(_screenCenterX, _screenCenterY);

            if (best is not ESPModule.DetectedTarget target)
                return;

            _predictor.Record(target.ScreenX, target.ScreenY);
            var predicted = _predictor.PredictNextPosition();

            float dx = predicted.x - _screenCenterX;
            float dy = predicted.y - _screenCenterY;
            float errorMagnitude = MathF.Sqrt(dx * dx + dy * dy);
            _memory.LogError(errorMagnitude);

            if (_memory.IsOvercompensating(50f))
            {
                _snap.SetSnapSpeed(0.75f); // Reduce snapping force
            }

            _snap.SnapToTarget(predicted.x, predicted.y, _screenCenterX, _screenCenterY);
        }

        public void Reset()
        {
            _memory.Clear();
            _predictor.Reset();
            _esp.Clear();
            _snap.Reset();
        }
    }
}
