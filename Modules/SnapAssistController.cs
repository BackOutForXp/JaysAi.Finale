//monarch v2.1
using JaysAi.AI.Models;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Input;

namespace JaysAi.Modules
{
    public class SnapAssistController
    {
        private readonly InputInjector InputInjector;
        private readonly TargetTracker targetTracker;
        private float snapStrength = 1.0f;
        private float deadzone = 0.02f;

        public SnapAssistController(InputInjector injector, TargetTracker tracker)
        {
            inputInjector = injector;
            targetTracker = tracker;
        }

        public void Update()
        {
            if (!targetTracker.HasTargets()) return;

            var target = targetTracker.GetCurrentTarget();
            if (target == null) return;

            float dx = target.CenterX - 0.5f; // Relative to screen center
            float dy = target.CenterY - 0.5f;

            if (MathF.Abs(dx) > deadzone || MathF.Abs(dy) > deadzone)
            {
                float moveX = dx * snapStrength;
                float moveY = dy * snapStrength;

                inputInjector.MoveAim(moveX, moveY);
            }
        }

        public void SetSnapStrength(float strength) => snapStrength = strength;
        public void SetDeadzone(float zone) => deadzone = zone;
    }
}
