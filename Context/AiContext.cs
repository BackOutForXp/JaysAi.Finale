// Neural v3.1
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Context
{
    public static class AiContext
    {
        public static TrackTarget? Tracker { get; set; }
        public static PredictionEngine? Prediction { get; set; }
        public static TargetingSystem? Targeting { get; set; }
        public static RuntimeBehaviorLog? Log { get; set; }

        public static void Register(AiManager ai)
        {
            Tracker = ai?.GetTracker();
            Prediction = ai?.GetPredictionEngine();
            Targeting = ai?.GetTargetingSystem();
            Log = ai?.GetRuntimeLog();
        }

        public static void Clear()
        {
            Tracker = null;
            Prediction = null;
            Targeting = null;
            Log = null;
        }
    }
}
