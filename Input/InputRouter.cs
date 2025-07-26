// neural v3.0
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.SystemLogic.Signals;
using System;

namespace JaysAi.Finale.Input
{
    public sealed class InputRouter
    {
        private readonly ControllerSignalBus _signalBus;
        private readonly AiManager _aiManager;
        private readonly AimbotLogic _aimbot;

        public InputRouter(AiManager aiManager, AimbotLogic aimbot)
        {
            _signalBus = ControllerSignalBus.Instance;
            _aiManager = aiManager;
            _aimbot = aimbot;

            _signalBus.OnInputReceived += HandleInput;
        }

        private void HandleInput(ControllerInputState inputState)
        {
            if (inputState == null) return;

            if (inputState.IsFiring)
                _aimbot.TriggerAssist(inputState);

            if (inputState.IsAiming)
                _aiManager.LogTargetingFrame(inputState);
        }

        public void Shutdown()
        {
            _signalBus.OnInputReceived -= HandleInput;
        }
    }
}
