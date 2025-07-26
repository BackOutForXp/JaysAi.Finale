// neural v3.0
using JaysAi.Finale.Input.Models;
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public static class InputUtils
    {
        /// <summary>
        /// Applies a radial deadzone to the stick input.
        /// </summary>
        public static Vector2 ApplyDeadzone(Vector2 input, float threshold)
        {
            float magnitude = input.Length();
            if (magnitude < threshold)
                return Vector2.Zero;

            return Vector2.Normalize(input) * ((magnitude - threshold) / (1 - threshold));
        }

        /// <summary>
        /// Clamps a vector to ensure the values remain within valid gamepad range.
        /// </summary>
        public static Vector2 ClampVector(Vector2 vector, float min = -1f, float max = 1f)
        {
            return new Vector2(
                Math.Clamp(vector.X, min, max),
                Math.Clamp(vector.Y, min, max)
            );
        }

        /// <summary>
        /// Converts raw input (0-255) to normalized float (-1 to 1).
        /// </summary>
        public static float NormalizeAxis(byte value)
        {
            return (value - 128f) / 127f;
        }

        /// <summary>
        /// Converts normalized float (-1 to 1) back to byte (0-255).
        /// </summary>
        public static byte DenormalizeAxis(float value)
        {
            return (byte)Math.Clamp((value * 127f) + 128f, 0, 255);
        }
    }
}
