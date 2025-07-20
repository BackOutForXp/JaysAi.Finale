//monarch v2.1 – Fully Refactored & Synced

namespace JaysAi.Finale.Modules
{
    public class DetectedObject
    {
        public string Name { get; set; }
        public bool IsEnemy { get; set; }
        public bool IsVisible { get; set; }

        public float ScreenX { get; set; }
        public float ScreenY { get; set; }

        public float ScreenDistance => CalculateScreenDistance(ScreenX, ScreenY);

        private float CalculateScreenDistance(float x, float y)
        {
            float centerX = 960; // assume 1920x1080 for now (replace with dynamic center)
            float centerY = 540;
            return (float)System.Math.Sqrt(System.Math.Pow(centerX - x, 2) + System.Math.Pow(centerY - y, 2));
        }
    }
}
