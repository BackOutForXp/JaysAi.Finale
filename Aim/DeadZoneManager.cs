//monarch v2.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public static class DeadZoneManager
    {
        // Minimum movement threshold to accept input
        public static float DeadZoneRadius = 1.5f; // Adjust based on input sensitivity

        public static bool IsWithinDeadZone(Vector2 input)
        {
            return input.Length() < DeadZoneRadius;
        }

        public static Vector2 ApplyDeadZone(Vector2 input)
        {
            return IsWithinDeadZone(input) ? Vector2.Zero : input;
        }

        // For debugging
        public static void PrintStatus(Vector2 input)
        {
            Console.WriteLine(IsWithinDeadZone(input)
                ? $"Input {input} is inside dead zone."
                : $"Input {input} is active.");
        }
    }
}
