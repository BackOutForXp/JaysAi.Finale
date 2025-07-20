//monarch v2.1
using System;

namespace JaysAi.Finale.Input
{
    public class StickZoneProfile
    {
        public float Deadzone { get; set; } = 0.1f;
        public float Sensitivity { get; set; } = 1.0f;
        public CurveType Curve { get; set; } = CurveType.Linear;

        public float ApplyCurve(float input)
        {
            float absInput = Math.Abs(input);
            if (absInput < Deadzone)
                return 0f;

            float scaled = (absInput - Deadzone) / (1f - Deadzone);
            float curved = Curve switch
            {
                CurveType.Linear => scaled,
                CurveType.Exponential => scaled * scaled,
                CurveType.Logarithmic => (float)Math.Log10(9 * scaled + 1),
                _ => scaled
            };

            return Math.Sign(input) * curved * Sensitivity;
        }

        public enum CurveType
        {
            Linear,
            Exponential,
            Logarithmic
        }
    }
}
