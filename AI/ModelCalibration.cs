//heavenly v3.0 – Calibration Interface for Model Alignment
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class ModelCalibration
    {
        public static CalibrationSettings Current { get; private set; } = new CalibrationSettings();

        public static void LoadCalibration()
        {
            string path = FilePathHelper.GetConfigPath("model_calibration.json");
            if (FileHelper.Exists(path))
            {
                Current = FileHelper.ReadJson<CalibrationSettings>(path);
            }
        }

        public static void SaveCalibration()
        {
            string path = FilePathHelper.GetConfigPath("model_calibration.json");
            FileHelper.WriteJson(path, Current);
        }

        public static void ApplyCalibration(ref float x, ref float y, float width, float height)
        {
            x += Current.XOffset;
            y += Current.YOffset;

            x *= Current.XScale;
            y *= Current.YScale;
        }
    }

    public class CalibrationSettings
    {
        public float XOffset { get; set; } = 0f;
        public float YOffset { get; set; } = 0f;
        public float XScale { get; set; } = 1f;
        public float YScale { get; set; } = 1f;
    }
}
