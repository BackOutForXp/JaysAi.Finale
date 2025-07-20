//monarch v2.1 – Real-time input poller
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace JaysAi.Finale.Input
{
    public static class ControllerInputListener
    {
        private static System.Timers.Timer _pollTimer;
        public static ControllerState CurrentState { get; private set; } = new();

        public static event Action<ControllerState> OnStateUpdated;

        public static void StartListening()
        {
            _pollTimer = new Timer(10); // 100Hz
            _pollTimer.Elapsed += PollInputs;
            _pollTimer.Start();
        }

        private static void PollInputs(object sender, ElapsedEventArgs e)
        {
            // TEMP: Fake state for now — replace with real polling from SharpDX/Gamepad API
            var newState = new ControllerState
            {
                A = false,
                B = false,
                X = false,
                Y = false,
                LeftStickX = 0f,
                LeftStickY = 0f,
                RightStickX = 0f,
                RightStickY = 0f,
                IsConnected = true
            };

            CurrentState = newState;
            OnStateUpdated?.Invoke(CurrentState);
        }

        public static void StopListening()
        {
            _pollTimer?.Stop();
            _pollTimer?.Dispose();
        }
    }
}
