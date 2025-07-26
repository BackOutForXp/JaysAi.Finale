// neural v3.0
using System;
using JaysAi.Finale.MathHelpers;

namespace JaysAi.Finale.Input
{
    public class StickAssistConfig
    {
        public float SensitivityX { get; set; } = 1.0f;
        public float SensitivityY { get; set; } = 1.0f;
        public float Deadzone { get; set; } = 0.1f;

        public PIDSettings PID_X { get; set; } = new PIDSettings(0.3f, 0f, 0.05f);
        public PIDSettings PID_Y { get; set; } = new PIDSettings(0.3f, 0f, 0.05f);

        public StickAssistConfig Clone()
        {
            return new StickAssistConfig
            {
                SensitivityX = this.SensitivityX,
                SensitivityY = this.SensitivityY,
                Deadzone = this.Deadzone,
                PID_X = this.PID_X.Clone(),
                PID_Y = this.PID_Y.Clone()
            };
        }
    }

    public class PIDSettings
    {
        public float Kp { get; set; }
        public float Ki { get; set; }
        public float Kd { get; set; }

        public PIDSettings(float kp, float ki, float kd)
        {
            Kp = kp;
            Ki = ki;
            Kd = kd;
        }

        public PIDSettings Clone()
        {
            return new PIDSettings(Kp, Ki, Kd);
        }
    }
}
