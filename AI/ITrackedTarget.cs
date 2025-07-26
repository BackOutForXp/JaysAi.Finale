// Neural v3.0 — ITrackedTarget.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public interface ITrackedTarget
    {
        /// <summary>
        /// Unique identifier for the target (could be entity ID, hash, etc.)
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Current world position of the target.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// The last time this target was seen or updated.
        /// </summary>
        DateTime LastSeen { get; }

        /// <summary>
        /// Is the target currently visible on screen or in field of view.
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Screen-space projected position if available.
        /// </summary>
        Vector2? ScreenPosition { get; }

        /// <summary>
        /// Optional team ID or classification tag for friend/foe logic.
        /// </summary>
        int TeamId { get; }

        /// <summary>
        /// Confidence score (0–1) based on AI model or tracking certainty.
        /// </summary>
        float Confidence { get; }

        /// <summary>
        /// Estimated velocity in world-space units/sec.
        /// </summary>
        Vector3 Velocity { get; }

        /// <summary>
        /// Marks the target as stale or expired from tracking system.
        /// </summary>
        void Invalidate();

        /// <summary>
        /// Updates the internal data for this tracked target.
        /// </summary>
        void Update(Vector3 position, Vector3 velocity, bool isVisible, Vector2? screenPos, float confidence, DateTime seenTime);
    }
}
