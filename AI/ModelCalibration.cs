// neural v3.0
using System;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public static class ModelCalibration
    {
        private static float _modelInputWidth = 320f;
        private static float _modelInputHeight = 320f;
        private static float _screenWidth = 1920f;
        private static float _screenHeight = 1080f;

        public static void SetModelInputSize(float width, float height)
        {
            _modelInputWidth = width;
            _modelInputHeight = height;
        }

        public static void SetScreenSize(float width, float height)
        {
            _screenWidth = width;
            _screenHeight = height;
        }

        public static BoundingBox ScaleBoundingBox(YoloBoundingBox box)
        {
            float x = box.X * (_screenWidth / _modelInputWidth);
            float y = box.Y * (_screenHeight / _modelInputHeight);
            float w = box.Width * (_screenWidth / _modelInputWidth);
            float h = box.Height * (_screenHeight / _modelInputHeight);

            return new BoundingBox
            {
                X = x,
                Y = y,
                Width = w,
                Height = h,
                Confidence = box.Confidence,
                Label = box.Label
            };
        }

        public static void AutoDetectFromSystem()
        {
            var resolution = ScreenManager.GetPrimaryResolution();
            SetScreenSize(resolution.Width, resolution.Height);
        }
    }
}
