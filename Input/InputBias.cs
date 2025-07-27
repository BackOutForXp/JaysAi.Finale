// Neural v3.1
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public static class InputBias
    {
        private static readonly Random _random = new();

        public static Vector2 AddBias(Vector2 original, float maxOffset = 1.0f)
        {
            float biasX = ((float)_random.NextDouble() - 0.5f) * 2f * maxOffset;
            float biasY = ((float)_random.NextDouble() - 0.5f) * 2f * maxOffset;

            return new Vector2(original.X + biasX, original.Y + biasY);
        }

        public static float AddBias(float value, float maxOffset = 0.75f)
        {
            float bias = ((float)_random.NextDouble() - 0.5f) * 2f * maxOffset;
            return value + bias;
        }
    }
}
