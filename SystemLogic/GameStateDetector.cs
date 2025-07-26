// neural v3.0
using System;
using System.Timers;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class GameStateDetector : IDisposable
    {
        private readonly Timer _stateCheckTimer;
        private string? _lastKnownState;
        private readonly Func<string> _stateEvaluator;

        public event EventHandler<string>? GameStateChanged;

        public GameStateDetector(Func<string> stateEvaluator, double intervalMs = 500)
        {
            _stateEvaluator = stateEvaluator ?? throw new ArgumentNullException(nameof(stateEvaluator));
            _stateCheckTimer = new Timer(intervalMs);
            _stateCheckTimer.Elapsed += OnTimerElapsed;
            _stateCheckTimer.AutoReset = true;
        }

        public void Start()
        {
            _lastKnownState = _stateEvaluator();
            _stateCheckTimer.Start();
        }

        public void Stop()
        {
            _stateCheckTimer.Stop();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            string currentState = _stateEvaluator();
            if (!string.Equals(_lastKnownState, currentState, StringComparison.Ordinal))
            {
                _lastKnownState = currentState;
                GameStateChanged?.Invoke(this, currentState);
            }
        }

        public void Dispose()
        {
            _stateCheckTimer.Dispose();
        }
    }
}
