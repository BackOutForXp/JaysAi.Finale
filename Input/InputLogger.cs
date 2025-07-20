//monarch v2.0
using System;

namespace JaysAi.Finale.Input
{
    /// <summary>
    /// Logs input states and aim data for debugging or overlay display.
    /// Used to verify behavior of StickX, Prediction, and Snap modules.
    /// </summary>
    public static class InputLogger
    {
        public static bool DebugEnabled { get; set; } = true;

        /// <summary>
        /// Logs a formatted string to console or output stream.
        /// </summary>
        public static void Log(string message)
        {
            if (!DebugEnabled) return;

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"[{timestamp}] {message}");
        }

        /// <summary>
        /// Logs controller and aim assist output.
        /// </summary>
        public static void LogInputState(float stickX, float stickY, bool aim, bool fire)
        {
            if (!DebugEnabled) return;

            string msg = $"Stick: ({stickX:0.00}, {stickY:0.00}) | Aim: {aim} | Fire: {fire}";
            Log(msg);
        }

        /// <summary>
        /// Logs snap angle or target delta.
        /// </summary>
        public static void LogSnapDelta(float deltaX, float deltaY)
        {
            if (!DebugEnabled) return;

            string msg = $"Snap Delta: ΔX={deltaX:0.00}, ΔY={deltaY:0.00}";
            Log(msg);
        }
    }
}
