//monarch v2.1 – YOLO Model Output Container
namespace JaysAi.Finale.AI
{
    public class YoloBoundingBox
    {
        public int Id { get; set; }                // Unique ID for tracking across frames
        public string Label { get; set; }          // Object class name
        public float Confidence { get; set; }      // Detection confidence score (0.0 – 1.0)

        public float X { get; set; }               // Top-left X
        public float Y { get; set; }               // Top-left Y
        public float Width { get; set; }
        public float Height { get; set; }

        public float Right => X + Width;
        public float Bottom => Y + Height;

        public bool IsValid =>
            !string.IsNullOrEmpty(Label) &&
            Confidence >= 0.4f &&
            Width > 0 &&
            Height > 0;
    }
}
