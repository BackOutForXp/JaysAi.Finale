// neural v3.0
using System;
using System.Numerics;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class YoloBoundingBox
    {
        public int ClassId { get; set; }
        public string Label { get; set; }
        public float Confidence { get; set; }
        public Rect BoundingBox { get; set; }

        public Vector2 Center => new Vector2(
            BoundingBox.X + BoundingBox.Width / 2f,
            BoundingBox.Y + BoundingBox.Height / 2f
        );

        public float Width => BoundingBox.Width;
        public float Height => BoundingBox.Height;
        public float Area => Width * Height;

        public YoloBoundingBox(int classId, string label, float confidence, Rect box)
        {
            ClassId = classId;
            Label = label;
            Confidence = confidence;
            BoundingBox = box;
        }

        public override string ToString()
        {
            return $"[YoloBox] Class: {ClassId} ({Label}), Conf: {Confidence:P1}, Box: {BoundingBox}";
        }

        public bool OverlapsWith(YoloBoundingBox other)
        {
            return BoundingBox.IntersectsWith(other.BoundingBox);
        }

        public float IoU(YoloBoundingBox other)
        {
            var intersect = BoundingBox & other.BoundingBox;
            if (intersect.Width <= 0 || intersect.Height <= 0)
                return 0f;

            float intersectionArea = intersect.Width * intersect.Height;
            float unionArea = this.Area + other.Area - intersectionArea;

            return intersectionArea / unionArea;
        }
    }
}
