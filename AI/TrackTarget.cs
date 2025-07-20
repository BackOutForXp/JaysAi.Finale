// Monarch v1.0 – TrackedTarget.cs
// ✅ Monarch Fix Checklist
// [x] Unified structure for all AI targets
// [x] Compatible with ESP, prediction, memory, and aim logic
// [x] Future-proof with extensibility for team ID, visibility, health, etc.

using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class TrackedTarget
    {
        public int Id { get; set; }                    // Unique ID (assigned by YOLO or internal memory)
        public string Label { get; set; }              // Class label (e.g., "enemy")
        public float Confidence { get; set; }          // Detection confidence
        public Point2f Center2D { get; set; }          // On-screen center position (2D)
        public float Size { get; set; }                // Width/height or bounding box magnitude

        public Point2f PredictedPosition { get; set; } // Filled in by PredictionEngine.cs
        public bool IsLocked { get; set; }             // If currently selected as locked-on target
        public bool IsVisible { get; set; } = true;    // Optional: set by future occlusion module

        // Optional metadata for expansion
        public int TeamId { get; set; } = -1;          // -1 = unknown, 0 = enemy, 1 = teammate
        public float Health { get; set; } = 100f;      // Placeholder for injected health data
    }
}
