// Neural v3.1
using System;
using System.Numerics;
using System.Windows;

namespace JaysAi.Finale.Data
{
    public class TargetInfo
    {
        public int ID { get; set; }
        public string Label { get; set; } = "Unknown";

        // 3D world space
        public Vector3 WorldPosition { get; set; }
        public Vector3 HeadPosition { get; set; }
        public Vector3 ChestPosition { get; set; }

        // 2D screen space
        public Point? ScreenPosition { get; set; }
        public Rect? ScreenBox { get; set; }

        public bool IsVisible { get; set; }
        public bool IsTracked { get; set; }

        public float Distance { get; set; }
        public float Confidence { get; set; }

        public DateTime LastSeen { get; set; } = DateTime.UtcNow;

        public bool IsValid =>
            IsVisible && ScreenPosition.HasValue && Confidence >= 0.5f;
    }
}
