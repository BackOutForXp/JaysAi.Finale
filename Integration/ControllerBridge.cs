//neural v3.0
using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using JaysAi.Finale.Input;
using JaysAi.Finale.Models;

namespace JaysAi.Finale.Integration
{
    public sealed class ControllerBridge : IDisposable
    {
        private static readonly Lazy<ControllerBridge> _instance = new(() => new ControllerBridge());
        public static ControllerBridge Instance => _instance.Value;

        private readonly ConcurrentDictionary<int, ControllerState> _controllerStates = new();
        private readonly Subject<ControllerEventArgs> _controllerStateChanges = new();

        public IObservable<ControllerEventArgs> ControllerStateChanges => _controllerStateChanges;

        private ControllerBridge() { }

        public void UpdateControllerState(int controllerId, ControllerInputState newInput)
        {
            var newState = new ControllerState();
            newState.Update(newInput);

            _controllerStates.AddOrUpdate(controllerId, newState, (_, __) => newState);
            _controllerStateChanges.OnNext(new ControllerEventArgs(controllerId, newState));
        }

        public ControllerState? GetControllerState(int controllerId)
        {
            _controllerStates.TryGetValue(controllerId, out var state);
            return state;
        }

        public void SetDeadzone(float threshold)
        {
            foreach (var state in _controllerStates.Values)
                state.DeadzoneThreshold = threshold;
        }

        public void Dispose()
        {
            _controllerStateChanges?.OnCompleted();
            _controllerStateChanges?.Dispose();
            _controllerStates.Clear();
        }
    }
}
