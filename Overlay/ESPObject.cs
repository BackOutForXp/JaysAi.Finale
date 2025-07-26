// Neural v3.0 — EspObject.cs
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace JaysAi.Finale.Overlay
{
    public class EspObject
    {
        // Display Name (e.g., "Enemy1")
        public string Name { get; set; }

        // Health Values
        public float Health { get; set; }
        public float MaxHealth { get; set; }

        // Screen Coordinates (2D bounding box)
        public Rect? ScreenBox { get; set; }

        // World Coordinates (optional 3D data, not drawn here)
        public Vector3D? WorldPosition { get; set; }

        // Whether the object should be rendered this frame
        public bool IsVisible { get; set; }

        // Skeleton rendering points (for experimental rendering)
        public List<SKPoint> SkeletonPoints { get; set; }

        // Optional tracking ID
        public int? ID { get; set; }

        // Optional confidence score (for YOLO or AI models)
        public float Confidence { get; set; }

        // Constructor
        public EspObject()
        {
            SkeletonPoints = new List<SKPoint>();
        }
    }

    // Optional 3D vector struct (placeholder if not already defined)
    public struct Vector3D
    {
        public float X, Y, Z;

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() => $"({X:F1}, {Y:F1}, {Z:F1})";
    }
}
