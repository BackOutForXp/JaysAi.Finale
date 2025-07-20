// File: BoneData.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    /// <summary>
    /// Represents 3D bone positions for an enemy character.
    /// </summary>
    public class BoneData
    {
        public Vector3 Head { get; set; } = Vector3.Zero;
        public Vector3 Chest { get; set; } = Vector3.Zero;
        public Vector3 Stomach { get; set; } = Vector3.Zero;
        public Vector3 Feet { get; set; } = Vector3.Zero;

        /// <summary>
        /// Time the bone data was last updated (for interpolation or decay).
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional: confidence score for AI-based detection (0 = invalid, 1 = perfect).
        /// </summary>
        public float ConfidenceScore { get; set; } = 1.0f;

        /// <summary>
        /// Returns true if bone data is present and usable.
        /// </summary>
        public bool HasValidBones =>
            Head != Vector3.Zero &&
            Chest != Vector3.Zero &&
            (DateTime.UtcNow - LastUpdated).TotalSeconds < 2; // stale data cutoff

        /// <summary>
        /// Resets bone positions to default and clears timestamps.
        /// </summary>
        public void Invalidate()
        {
            Head = Chest = Stomach = Feet = Vector3.Zero;
            ConfidenceScore = 0f;
            LastUpdated = DateTime.MinValue;
        }
    }
}
