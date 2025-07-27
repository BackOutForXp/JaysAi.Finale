// Neural v3.1
namespace JaysAi.Finale.Utility
{
    public static class AimSmoother
    {
        public static float ApplySmoothing(float current, float target, float smoothing)
        {
            if (smoothing <= 0f) return target;
            return current + (target - current) * smoothing;
        }
    }
}
