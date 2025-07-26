//neural v3.0
using System;
using System.Numerics;
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Helpers;

namespace JaysAi.Finale.Input
{
    public static class ControllerBridge
    {
        public static Vector2 LeftStick { get; private set; }
        public static Vector2 RightStick { get; private set; }
        public static bool IsControllerConnected { get; private set; }

        public static float TriggerL { get; private set; }
        public static float TriggerR { get; private set; }

        public static bool ButtonA { get; private set; }
        public static bool ButtonB { get; private set; }
        public static bool ButtonX { get; private set; }
        public static bool ButtonY { get; private set; }

        public static bool BumperL { get; private set; }
        public static bool BumperR { get; private set; }

        public static bool DPadUp { get; private set; }
        public static bool DPadDown { get; private set; }
        public static bool DPadLeft { get; private set; }
        public static bool DPadRight { get; private set; }

        public static bool StartButton { get; private set; }
        public static bool BackButton { get; private set; }

        public static void Update(ControllerInputState inputState)
        {
            if (inputState == null) return;

            IsControllerConnected = inputState.IsConnected;

            LeftStick = ApplyDeadzone(inputState.LeftStick);
            RightStick = ApplyDeadzone(inputState.RightStick);

            TriggerL = inputState.TriggerL;
            TriggerR = inputState.TriggerR;

            ButtonA = inputState.ButtonA;
            ButtonB = inputState.ButtonB;
            ButtonX = inputState.ButtonX;
            ButtonY = inputState.ButtonY;

            BumperL = inputState.BumperL;
            BumperR = inputState.BumperR;

            DPadUp = inputState.DPadUp;
            DPadDown = inputState.DPadDown;
            DPadLeft = inputState.DPadLeft;
            DPadRight = inputState.DPadRight;

            StartButton = inputState.Start;
            BackButton = inputState.Back;
        }

        private static Vector2 ApplyDeadzone(Vector2 stickInput, float threshold = 0.08f)
        {
            return stickInput.Length() < threshold ? Vector2.Zero : stickInput;
        }

        public static bool IsADS()
        {
            return TriggerL > 0.6f;
        }

        public static bool IsFiring()
        {
            return TriggerR > 0.6f;
        }

        public static bool IsActivatingStickAssist()
        {
            return IsADS() && IsFiring();
        }
    }
}
