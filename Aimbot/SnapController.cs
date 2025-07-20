//monarch v2.1 – Snap logic controller w/ lock threshold & tick gating
using System.Numerics;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Aimbot
{
    public class SnapController
    {
        private readonly SnapAssist _snapAssist;
        private readonly TargetSelector _selector;
        private readonly ControllerInputState _inputState;
        private long _lastSnapTick;

        public int SnapCooldownMs { get; set; } = 100; // Prevent rapid snapping
        public float MinLockDistance { get; set; } = 50f;

        public SnapController()
        {
            _snapAssist = new SnapAssist();
            _selector = new TargetSelector();
            _inputState = new ControllerInputState();
            _lastSnapTick = 0;
        }

        public void TrySnap(Vector2 screenCenter, DetectedTarget[] targets)
        {
            if (!_inputState.IsAiming())
                return;

            var bestTarget = _selector.SelectBestTarget(screenCenter, targets);
            if (bestTarget == null) return;

            float distance = Vector2.Distance(screenCenter, bestTarget.ScreenPosition);
            if (distance > MinLockDistance) return;

            long currentTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (currentTick - _lastSnapTick < SnapCooldownMs)
                return;

            _snapAssist.ExecuteSnap(screenCenter, bestTarget.ScreenPosition);
            _lastSnapTick = currentTick;
        }
    }
}
