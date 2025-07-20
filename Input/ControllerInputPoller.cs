//monarch v2.1 – Real-time controller input scanner
using System;
using System.Timers;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Input
{
    public static class ControllerInputPoller
    {
        private static ControllerInputState _state = new();
        private static Timer _pollTimer;

        public static ControllerInputState CurrentState => _state;

        public static void StartPolling()
        {
            _pollTimer = new Timer(10); // Poll every 10ms (adjust as needed)
            _pollTimer.Elapsed += PollInputs;
            _pollTimer.AutoReset = true;
            _pollTimer.Start();
        }

        private static void PollInputs(object sender, ElapsedEventArgs e)
        {
            // Example placeholder: Inject controller input detection logic here
            // Replace these lines with real detection using SharpDX, DS4Windows, or custom driver

            _state.A = false;
            _state.B = false;
            _state.X = false;
            _state.Y = false;

            // TODO: Plug in controller SDK to map real values
        }

        public static void StopPolling()
        {
            _pollTimer?.Stop();
            _pollTimer?.Dispose();
        }
    }
}
