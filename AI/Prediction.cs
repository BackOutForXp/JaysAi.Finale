// Monarch v1.0 – PredictionSignal.cs
// ✅ Monarch Fix Checklist
// [x] Global prediction signal container
// [x] Thread-safe data access
// [x] Used by Snap, Overlay, and Assist AI

using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class PredictionSignal
    {
        private static readonly object Lock = new();

        public static Vector2? Predicted2D { get; private set; }
        public static Vector3? Predicted3D { get; private set; }
        public static float Confidence { get; private set; }

        public static void Update(Vector2 new2D, Vector3 new3D, float confidence)
        {
            lock (Lock)
            {
                Predicted2D = new2D;
                Predicted3D = new3D;
                Confidence = confidence;
            }
        }

        public static void Reset()
        {
            lock (Lock)
            {
                Predicted2D = null;
                Predicted3D = null;
                Confidence = 0f;
            }
        }

        public static bool HasValidPrediction()
        {
            lock (Lock)
            {
                return Predicted2D.HasValue && Confidence >= 0.5f;
            }
        }
    }
}
