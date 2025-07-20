//monarch v2.0
namespace JaysAi.Finale.Aim
{
    public static class SnapSettings
    {
        public static float MaxSnapRange = 450f;
        public static float Smoothing = 0.85f;
        public static bool EnablePrediction = true;
        public static bool RequireLineOfSight = false;
        public static bool IgnoreTeamTargets = true;

        // Future toggles
        public static bool EnableStickyTargeting = false;
        public static float SnapCooldownMs = 0f;
    }
}
