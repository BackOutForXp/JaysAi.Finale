//heavenly v3.0
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public static class StickXModule
    {
        private static float _smoothingFactor = 0.15f;
        private static float _lastX = 0f;
        private static float _lastY = 0f;

        public static void Reset()
        {
            _lastX = 0f;
            _lastY = 0f;
        }

        public static void ApplyStickAim(float targetX, float targetY)
        {
            float smoothX = Lerp(_lastX, targetX, _smoothingFactor);
            float smoothY = Lerp(_lastY, targetY, _smoothingFactor);

            ControllerInputState.SetRightStick(smoothX, smoothY);

            _lastX = smoothX;
            _lastY = smoothY;
        }

        private static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
