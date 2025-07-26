// Neural v3.0 — ControllerInputState.cs
using System;
using System.Collections.Generic;
using JaysAi.Finale.Enums;
using JaysAi.Finale.Helpers;

namespace JaysAi.Finale.Input
{
    public class ControllerInputState
    {
        public Vector2 LeftStick { get; set; } = new Vector2(0, 0);
        public Vector2 RightStick { get; set; } = new Vector2(0, 0);
        public float LeftTrigger { get; set; }
        public float RightTrigger { get; set; }

        public Dictionary<ControllerButton, bool> ButtonStates { get; private set; }

        public ControllerInputState()
        {
            ButtonStates = new Dictionary<ControllerButton, bool>();
            InitializeDefaultButtons();
        }

        private void InitializeDefaultButtons()
        {
            foreach (ControllerButton button in Enum.GetValues(typeof(ControllerButton)))
            {
                ButtonStates[button] = false;
            }
        }

        public void SetButtonState(ControllerButton button, bool isPressed)
        {
            if (ButtonStates.ContainsKey(button))
            {
                ButtonStates[button] = isPressed;
            }
        }

        public bool IsButtonPressed(ControllerButton button)
        {
            return ButtonStates.TryGetValue(button, out var isPressed) && isPressed;
        }

        public void Reset()
        {
            LeftStick = new Vector2(0, 0);
            RightStick = new Vector2(0, 0);
            LeftTrigger = 0;
            RightTrigger = 0;

            var keys = new List<ControllerButton>(ButtonStates.Keys);
            foreach (var key in keys)
            {
                ButtonStates[key] = false;
            }
        }

        public override string ToString()
        {
            return $"LS: {LeftStick}, RS: {RightStick}, LT: {LeftTrigger:F2}, RT: {RightTrigger:F2}, Buttons: [{string.Join(", ", ButtonStates)}]";
        }
    }
}
