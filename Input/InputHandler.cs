//monarch v2.0
using JaysAi.Finale.AI;
using System;

namespace JaysAi.Finale.Input
{
    /// <summary>
    /// Central handler for user input state including keyboard, mouse, and controller.
    /// Delegates input state to internal modules.
    /// </summary>
    public class InputHandler
    {
        public ControllerInputState ControllerState { get; private set; }
        public StickXModule StickAssist { get; private set; }

        public bool IsRunning { get; private set; } = true;

        public InputHandler()
        {
            ControllerState = new ControllerInputState();
            StickAssist = new StickXModule();
        }

        /// <summary>
        /// Updates input state manually.
        /// This should be called once per frame or tick loop.
        /// </summary>
        public void Update()
        {
            if (!IsRunning)
                return;

            // Simulated values (these would be read from real input libraries in production)
            float lx = 0f; // Replace with real analog X input
            float ly = 0f;
            float rx = 0f;
            float ry = 0f;
            bool aiming = false; // Replace with input binding
            bool firing = false;

            // Update internal state
            ControllerState.Update(lx, ly, rx, ry, aiming, firing);

            // Feed input into aim assist module
            StickAssist.UpdateAim(rx, ry);
        }

        /// <summary>
        /// Gets the current smoothed stick aim output.
        /// </summary>
        public (float X, float Y) GetAimAssistDelta()
        {
            return StickAssist.GetStickOutput();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Reset()
        {
            ControllerState.Reset();
            StickAssist.Reset();
        }
    }
}
