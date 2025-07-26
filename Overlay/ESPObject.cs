// neural v3.1
using JaysAi.Finale.Data;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class ESPObject
    {
        public string Name { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public SKRect? ScreenBox { get; set; }
        public Vector3D? WorldPosition { get; set; }
        public bool IsVisible { get; set; }
        public List<SKPoint> SkeletonPoints { get; set; }
        public int? ID { get; set; }
        public float Confidence { get; set; }

        public ESPObject()
        {
            SkeletonPoints = new List<SKPoint>();
        }

        public ESPObject(Enemy enemy)
        {
            Name = enemy.Name;
            Health = enemy.Health;
            MaxHealth = enemy.MaxHealth;
            ScreenBox = enemy.ScreenBox;
            WorldPosition = enemy.WorldPosition;
            IsVisible = enemy.IsVisible;
            ID = enemy.ID;
            Confidence = enemy.Confidence;
            SkeletonPoints = enemy.SkeletonPoints ?? new List<SKPoint>();
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsVisible || ScreenBox == null)
                return;

            var box = ScreenBox.Value;

            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                Color = SKColors.Red,
                IsAntialias = true
            };

            canvas.DrawRect(box, paint);
        }
    }

    // Consider moving this to Data/Shared/
    public struct Vector3D
    {
        public float X, Y, Z;

        public Vector3D(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public override string ToString() => $"({X:F1}, {Y:F1}, {Z:F1})";
    }
}
