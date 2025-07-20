// Monarch v1.0 – TrackedTarget.cs
// ✅ Monarch Fix Checklist
// [x] Holds detection box, label, confidence
// [x] Supports predicted position
// [x] Supports team ID and lock status
// [x] Used across Memory, ESP, Prediction, Aimbot

using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class TrackedTarget
    {
        public Rect Bounds { get; set; }                  // Raw bounding box from YOLO
        public string Label { get; set; }                 // Object label (e.g., "Enemy", "Player")
        public float Confidence { get; set; }             // Detection confidence score (0.0–1.0)

        public int TeamId { get; set; } = 0;              // 0 = enemy, 1 = ally, etc.
        public bool IsLocked { get; set; } = false;       // Is this the current lock-on target?

        public Point2f PredictedPosition { get; set; }    // Used for predictive aim
        public float VelocityX { get; set; }              // For motion extrapolation
        public float VelocityY { get; set; }

        public Point2f Center2D => new(
            Bounds.X + Bounds.Width / 2f,
            Bounds.Y + Bounds.Height / 2f
        );

        public TrackedTarget(Rect bounds, string label, float confidence)
        {
            Bounds = bounds;
            Label = label;
            Confidence = confidence;
        }
    }
}
