//heavenly v3.0 – InputHandler Core
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Input
{
    public class InputHandler
    {
        private readonly InputStateLogger _stateLogger;
        private readonly InputDispatcher _dispatcher;

        public InputHandler()
        {
            _stateLogger = new InputStateLogger();
            _dispatcher = new InputDispatcher();
        }

        public void ProcessInput(InputState currentState)
        {
            if (currentState == null)
                return;

            _stateLogger.Log(currentState);
            _dispatcher.RouteInput(currentState);

            if (FeatureToggle.IsEnabled("AutoTrigger") && currentState.FirePressed)
                SnapAssist.Instance?.TriggerFire();

            if (FeatureToggle.IsEnabled("RecoilControl"))
                RecoilManager.Instance?.ApplyRecoilCorrection(currentState);

            if (FeatureToggle.IsEnabled("StickAssist"))
                StickXModule.Instance?.AdjustStickMovement(currentState);
        }

        public void Tick()
        {
            var inputState = InputPoller.Poll();
            ProcessInput(inputState);
        }
    }
}
