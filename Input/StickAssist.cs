//monarch v2.1

//monarch v2.1
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class StickAssist
    {
        private readonly StickAssistConfig config;
        private float integralX, integralY;
        private float previousErrorX, previousErrorY;

        public StickAssist(StickAssistConfig config)
        {
            this.config = config;
        }

        public (short x, short y) ComputeStickOutput(FrameSnapshot target, float currentX, float currentY)
        {
            float errorX = target.X - currentX;
            float errorY = target.Y - currentY;

            integralX += errorX;
            integralY += errorY;

            float derivativeX = errorX - previousErrorX;
            float derivativeY = errorY - previousErrorY;

            float outputX = config.Kp * errorX + config.Ki * integralX + config.Kd * derivativeX;
            float outputY = config.Kp * errorY + config.Ki * integralY + config.Kd * derivativeY;

            previousErrorX = errorX;
            previousErrorY = errorY;

            short stickX = ApplyDeadzone(outputX);
            short stickY = ApplyDeadzone(outputY);

            return (stickX, config.VerticalOnly ? (short)0 : stickY);
        }

        private short ApplyDeadzone(float value)
        {
            return Math.Abs(value) < config.Deadzone ? (short)0 : (short)Math.Clamp(value, -config.MaxStrength, config.MaxStrength);
        }
    }
}
