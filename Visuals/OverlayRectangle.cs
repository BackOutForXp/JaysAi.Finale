//monarch v2.1 – Rectangle Data Structure
namespace JaysAi.Finale.Visuals
{
    public class OverlayRectangle
    {
        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }
        public string Label { get; }
        public object Brush { get; }

        public OverlayRectangle(double x, double y, double width, double height, string label, object brush)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Label = label;
            Brush = brush;
        }
    }
}
