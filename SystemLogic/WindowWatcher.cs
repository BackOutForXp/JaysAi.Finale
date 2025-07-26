// neural v3.0
using System;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using JaysAi.Finale.Helpers;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class WindowWatcher : IDisposable
    {
        private readonly Timer _pollTimer;
        private IntPtr _targetWindow;
        private (int X, int Y, int Width, int Height) _lastBounds;
        private bool _wasFocused;

        public event EventHandler? WindowFocusLost;
        public event EventHandler<(int X, int Y, int Width, int Height)>? WindowBoundsChanged;

        public WindowWatcher()
        {
            _pollTimer = new Timer(100); // poll every 100ms
            _pollTimer.Elapsed += OnPoll;
        }

        public void StartMonitoring(string partialWindowTitle)
        {
            _targetWindow = WindowUtilities.FindGameWindow(partialWindowTitle);
            if (_targetWindow != IntPtr.Zero)
            {
                WindowUtilities.TryGetWindowBounds(_targetWindow, out _lastBounds);
                _wasFocused = WindowUtilities.IsWindowInFocus(_targetWindow);
                _pollTimer.Start();
            }
        }

        private void OnPoll(object? sender, ElapsedEventArgs e)
        {
            if (_targetWindow == IntPtr.Zero) return;

            // Check for bounds change
            if (WindowUtilities.TryGetWindowBounds(_targetWindow, out var currentBounds))
            {
                if (!currentBounds.Equals(_lastBounds))
                {
                    _lastBounds = currentBounds;
                    WindowBoundsChanged?.Invoke(this, currentBounds);
                }
            }

            // Check for focus change
            bool isFocused = WindowUtilities.IsWindowInFocus(_targetWindow);
            if (_wasFocused && !isFocused)
            {
                _wasFocused = false;
                WindowFocusLost?.Invoke(this, EventArgs.Empty);
            }
            else if (!_wasFocused && isFocused)
            {
                _wasFocused = true;
            }
        }

        public void Stop()
        {
            _pollTimer.Stop();
        }

        public void Dispose()
        {
            _pollTimer.Dispose();
        }
    }
}
