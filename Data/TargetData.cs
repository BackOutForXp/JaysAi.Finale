// Neural v3.0 — TargetData.cs
using SkiaSharp;
using System;

namespace JaysAi.Finale.Data
{
    public class TargetData
    {
        // Visibility for rendering
        public bool IsVisible { get; set; }

        // Detected name or label of the target (e.g., enemy, player)
        public string Name { get; set; } = string.Empty;

        // Health tracking
        public float Health { get; set; }
        public float MaxHealth { get; set; }

        // Bounding box for drawing purposes (in screen space)
        public SKRect? ScreenBox { get; set; }

        // Timestamp of last detection (for fading or timeout logic)
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;

        // Classifier or team identifier (optional)
        public int TeamId { get; set; }

        // Optional confidence score from AI model (0.0 to 1.0)
        public float Confidence { get; set; }

        // Depth or distance from screen (optional, for prioritizing)
        public float Distance { get; set; }

        // Reserved: future use for prediction/direction
        public SKPoint? PredictedPosition { get; set; }

        // Reserved: 2D velocity vector for motion prediction
        public SKPoint? Velocity { get; set; }

        // Adaptive update logic hook
        public void UpdateVisibility(bool visible)
        {
            IsVisible = visible;
            if (visible) LastSeen = DateTime.UtcNow;
        }
    }
}
