// File: Enemy.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Data
{
    /// <summary>
    /// Represents a tracked enemy in game memory or visual capture.
    /// Used by AimAssist, ESP, TargetSelector, and more.
    /// </summary>
    public class Enemy
    {
        public int Id { get; set; }

        /// <summary>3D world position of the enemy (from memory or estimation)</summary>
        public Vector3 WorldPosition { get; set; }

        /// <summary>2D screen-space position of the enemy (projected or tracked)</summary>
        public Vector2 ScreenPosition { get; set; }

        /// <summary>3D movement velocity (game space or estimated)</summary>
        public Vector3 Velocity { get; set; }

        /// <summary>2D screen-space velocity (pixels/sec)</summary>
        public Vector2 ScreenVelocity { get; set; }

        /// <summary>Whether the enemy is currently visible on screen</summary>
        public bool IsVisible { get; set; }

        /// <summary>Whether this enemy is on the opposing team</summary>
        public bool IsEnemy { get; set; }

        /// <summary>Whether this enemy can currently be locked onto</summary>
        public bool IsTargetable { get; set; } = true;

        /// <summary>Whether this target is marked as high-priority (e.g. low HP, tagged)</summary>
        public bool IsPriorityTarget { get; set; }

        /// <summary>Enemy's current health (0–100)</summary>
        public int Health { get; set; }

        /// <summary>Optional team ID (used to avoid targeting teammates)</summary>
        public int TeamId { get; set; }

        /// <summary>Display name or debug name of the enemy</summary>
        public string Name { get; set; } = "Unknown";

        /// <summary>Optional attached bone data (for head/chest targeting)</summary>
        public BoneData? Bones { get; set; }

        /// <summary>Calculated distance from player to target in 2D screen space</summary>
        public float Distance => ScreenPosition.Length();

        public Enemy() { }

        public Enemy(int id)
        {
            Id = id;
        }
    }
}
