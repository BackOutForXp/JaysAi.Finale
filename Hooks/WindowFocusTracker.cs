// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.Hooks
{
    public class WindowFocusTracker
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        private readonly Timer _pollTimer;
        private readonly string _targetProcessName;
        private bool _isFocused;

        public event Action<bool> FocusChanged;

        public WindowFocusTracker(string targetProcessName, double pollIntervalMs = 500)
        {
            _targetProcessName = targetProcessName.ToLower();
            _pollTimer = new Timer(pollIntervalMs);
            _pollTimer.Elapsed += OnPoll;
        }

        public void StartTracking()
        {
            _pollTimer.Start();
            LogSystem.Info("Window focus tracking started.");
        }

        public void StopTracking()
        {
            _pollTimer.Stop();
            LogSystem.Info("Window focus tracking stopped.");
        }

        private void OnPoll(object sender, ElapsedEventArgs e)
        {
            try
            {
                IntPtr foregroundWindow = GetForegroundWindow();
                GetWindowThreadProcessId(foregroundWindow, out int processId);

                Process proc = Process.GetProcessById(processId);
                bool isCurrentlyFocused = proc.ProcessName.Equals(_targetProcessName, StringComparison.OrdinalIgnoreCase);

                if (isCurrentlyFocused != _isFocused)
                {
                    _isFocused = isCurrentlyFocused;
                    FocusChanged?.Invoke(_isFocused);
                    LogSystem.Debug($"Focus changed: {_isFocused}");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn("Window focus tracking failed: " + ex.Message);
            }
        }

        public bool IsGameFocused => _isFocused;
    }
}
