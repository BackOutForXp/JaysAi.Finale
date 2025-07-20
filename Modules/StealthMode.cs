using JaysAi.Finale.Visuals;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace JaysAi.Finale.Modules
{
    public class StealthMode
    {
        private readonly OverlayWindow _overlayWindow;
        private readonly DispatcherTimer _pollTimer;
        private bool _isStealthEnabled;

        public bool IsActive => _isStealthEnabled;

        public StealthMode(OverlayWindow overlayWindow)
        {
            _overlayWindow = overlayWindow;

            _pollTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _pollTimer.Tick += CheckStealthToggleKey;
            _pollTimer.Start();
        }

        private void CheckStealthToggleKey(object sender, EventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0 &&
                Keyboard.IsKeyDown(Key.S)) // Ctrl+S toggles stealth
            {
                ToggleStealth();
            }
        }

        public void ToggleStealth()
        {
            _isStealthEnabled = !_isStealthEnabled;
            _overlayWindow.Visibility = _isStealthEnabled ? Visibility.Hidden : Visibility.Visible;
        }

        public void ForceDisable()
        {
            _isStealthEnabled = false;
            _overlayWindow.Visibility = Visibility.Visible;
        }
    }
}

// ✅ StealthMode.cs Checklist:
// [x] Toggles overlay visibility with Ctrl+S
// [x] Uses DispatcherTimer for polling without blocking UI
// [x] Directly modifies OverlayWindow visibility
// [ ] ☐ Add configurable keybind support
// [ ] ☐ Allow external modules to subscribe to stealth status
// [ ] ☐ Hook into global "panic" mode / stream-safe toggle
// [ ] ☐ Suspend input emulation or ESP drawing while active
