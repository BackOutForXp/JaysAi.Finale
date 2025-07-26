// neural v3.0
using JaysAi.Finale.Input.Models;
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public static class InputValidator
    {
        /// <summary>
        /// Checks if a given stick vector is within valid range.
        /// </summary>
        public static bool IsValidStickInput(Vector2 stickInput)
        {
            return Math.Abs(stickInput.X) <= 1.0f && Math.Abs(stickInput.Y) <= 1.0f;
        }

        /// <summary>
        /// Checks if the trigger values are within normalized range [0, 1].
        /// </summary>
        public static bool IsValidTriggerInput(float leftTrigger, float rightTrigger)
        {
            return leftTrigger is >= 0 and <= 1 && rightTrigger is >= 0 and <= 1;
        }

        /// <summary>
        /// Checks if any input values are NaN or Infinity.
        /// </summary>
        public static bool HasInvalidValues(ControllerInputState inputState)
        {
            return float.IsNaN(inputState.LeftTrigger) ||
                   float.IsNaN(inputState.RightTrigger) ||
                   float.IsInfinity(inputState.LeftTrigger) ||
                   float.IsInfinity(inputState.RightTrigger) ||
                   float.IsNaN(inputState.LeftStick.X) ||
                   float.IsNaN(inputState.LeftStick.Y) ||
                   float.IsNaN(inputState.RightStick.X) ||
                   float.IsNaN(inputState.RightStick.Y);
        }
    }
}
