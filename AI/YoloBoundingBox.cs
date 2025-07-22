//heavenly v3.0
using System;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class YoloBoundingBox
    {
        public Rect BoundingBox { get; set; }
        public string Label { get; set; }
        public float Confidence { get; set; }
        public bool IsEnemy { get; set; }
        public bool IsFriendly { get; set; }
        public int Id { get; set; }

        public Point Center => new(
            BoundingBox.X + BoundingBox.Width / 2,
            BoundingBox.Y + BoundingBox.Height / 2
        );

        public int Area => BoundingBox.Width * BoundingBox.Height;

        public YoloBoundingBox(int id, Rect box, string label, float confidence)
        {
            Id = id;
            BoundingBox = box;
            Label = label;
            Confidence = confidence;
            IsEnemy = false;
            IsFriendly = false;
        }

        public void Classify(Func<string, bool> isEnemyLabel, Func<string, bool> isFriendlyLabel)
        {
            IsEnemy = isEnemyLabel(Label);
            IsFriendly = isFriendlyLabel(Label);
        }

        public override string ToString()
        {
            return $"[ID:{Id}] {Label} ({Confidence:P1}) Enemy: {IsEnemy}, Friendly: {IsFriendly}";
        }
    }
}
