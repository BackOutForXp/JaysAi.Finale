//monarch v2.1 – Object detection data structure
namespace JaysAi.Finale.AI
{
    public enum TargetType
    {
        Enemy,
        Ally,
        Neutral,
        Object
    }

    public class TargetInfo
    {
        // Screen-space bounding box
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        // Center point (can be precomputed or derived)
        public double CenterX => X + Width / 2;
        public double CenterY => Y + Height / 2;

        // Optional additional logic
        public float Distance { get; set; } // from player/camera
        public float Confidence { get; set; } // from detector model

        public bool IsVisible { get; set; } = true;
        public bool IsPriority { get; set; } = false;

        public TargetType Type { get; set; } = TargetType.Enemy;

        // Optional per-frame FOV radius override
        public float FovRadius { get; set; } = 40f;
    }
}
