using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public class Hotkey
    {
        public Key Key { get; private set; }
        public ModifierKeys Modifiers { get; private set; }
        public Action OnPressed { get; private set; }

        private bool _wasPressed = false;

        public Hotkey(Key key, ModifierKeys modifiers, Action onPressed)
        {
            Key = key;
            Modifiers = modifiers;
            OnPressed = onPressed;
        }

        public void Check()
        {
            bool isModifierDown =
                (Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.IsKeyDown(Key.LeftCtrl)) ||
                (Modifiers.HasFlag(ModifierKeys.Shift) && Keyboard.IsKeyDown(Key.LeftShift)) ||
                (Modifiers.HasFlag(ModifierKeys.Alt) && Keyboard.IsKeyDown(Key.LeftAlt));

            if (Keyboard.IsKeyDown(Key) && isModifierDown)
            {
                if (!_wasPressed)
                {
                    OnPressed?.Invoke();
                    _wasPressed = true;
                }
            }
            else
            {
                _wasPressed = false;
            }
        }
    }
}
