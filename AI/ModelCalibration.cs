//monarch v2.1
namespace JaysAi.Finale.AI
{
    public class ModelCalibration
    {
        public int InputWidth { get; set; }
        public int InputHeight { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public ModelCalibration(int inputW, int inputH, int screenW, int screenH)
        {
            InputWidth = inputW;
            InputHeight = inputH;
            ScreenWidth = screenW;
            ScreenHeight = screenH;
        }

        public (float X, float Y) Calibrate(float x, float y)
        {
            float calibratedX = x * ScreenWidth / InputWidth;
            float calibratedY = y * ScreenHeight / InputHeight;
            return (calibratedX, calibratedY);
        }

        public (float Width, float Height) CalibrateSize(float width, float height)
        {
            float calibratedWidth = width * ScreenWidth / InputWidth;
            float calibratedHeight = height * ScreenHeight / InputHeight;
            return (calibratedWidth, calibratedHeight);
        }
    }
}
