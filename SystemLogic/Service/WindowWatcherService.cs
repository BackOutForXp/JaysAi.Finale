// neural v3.0
using System;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.SystemLogic.Service
{
    public sealed class WindowWatcherService : IDisposable
    {
        private readonly WindowWatcher _watcher;
        private readonly string _targetWindowTitle;
        private bool _isMonitoring;

        public event EventHandler? OnWindowLostFocus;
        public event EventHandler<(int X, int Y, int Width, int Height)>? OnWindowBoundsChanged;

        public WindowWatcherService(string partialWindowTitle)
        {
            _targetWindowTitle = partialWindowTitle;
            _watcher = new WindowWatcher();

            _watcher.WindowFocusLost += (_, _) => OnWindowLostFocus?.Invoke(this, EventArgs.Empty);
            _watcher.WindowBoundsChanged += (_, bounds) => OnWindowBoundsChanged?.Invoke(this, bounds);
        }

        public void Start()
        {
            if (_isMonitoring) return;

            _watcher.StartMonitoring(_targetWindowTitle);
            _isMonitoring = true;
        }

        public void Stop()
        {
            if (!_isMonitoring) return;

            _watcher.Stop();
            _isMonitoring = false;
        }

        public void Dispose()
        {
            Stop();
            _watcher.Dispose();
        }
    }
}
