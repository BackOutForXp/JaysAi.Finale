//monarch v2.1 – Universal Visual Overlay Instruction
namespace JaysAi.Finale.Visuals
{
    public enum OverlayShape
    {
        Rectangle,
        Circle
    }

    public enum OverlayColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        White
    }

    public struct OverlayCommand
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public string Label { get; set; }
        public OverlayColor Color { get; set; }
        public OverlayShape Shape { get; set; }

        public bool HasLabel => !string.IsNullOrEmpty(Label);
    }
}
