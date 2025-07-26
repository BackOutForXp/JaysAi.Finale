// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using JaysAi.Finale.Input.Models;

namespace JaysAi.Finale.Input
{
    public sealed class InputStateLogger
    {
        private readonly ConcurrentQueue<LoggedInputState> _inputLog = new();
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public event Action<LoggedInputState>? OnInputLogged;

        public InputStateLogger()
        {
        }

        public void LogState(ControllerInputState inputState)
        {
            var timestamp = _stopwatch.Elapsed.TotalMilliseconds;

            var logged = new LoggedInputState
            {
                TimestampMs = timestamp,
                InputState = inputState.Clone()
            };

            _inputLog.Enqueue(logged);
            OnInputLogged?.Invoke(logged);
        }

        public LoggedInputState[] GetSnapshot()
        {
            return _inputLog.ToArray();
        }

        public void Clear()
        {
            while (_inputLog.TryDequeue(out _)) { }
        }
    }

    public class LoggedInputState
    {
        public double TimestampMs { get; set; }
        public ControllerInputState? InputState { get; set; }
    }
}
