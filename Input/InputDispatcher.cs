//heavenly v3.0 – Input Router Hub
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public class InputDispatcher
    {
        private readonly InputMonitor _inputMonitor;
        private readonly InputEmulator _inputEmulator;
        private readonly ControllerInputListener _controllerListener;
        private readonly TriggerBot _triggerBot;
        private readonly SnapAssist _snapAssist;
        private readonly Logger _logger;

        public InputDispatcher(
            InputMonitor inputMonitor,
            InputEmulator inputEmulator,
            ControllerInputListener controllerListener,
            TriggerBot triggerBot,
            SnapAssist snapAssist,
            Logger logger)
        {
            _inputMonitor = inputMonitor;
            _inputEmulator = inputEmulator;
            _controllerListener = controllerListener;
            _triggerBot = triggerBot;
            _snapAssist = snapAssist;
            _logger = logger;
        }

        public void ProcessInputs()
        {
            _inputMonitor.Update();

            if (_inputMonitor.IsLeftMouseDown || _controllerListener.IsTriggerPressed())
            {
                _logger.Debug("Fire input detected");
                _triggerBot.TryShoot();
            }

            if (_inputMonitor.IsAimKeyHeld || _controllerListener.IsADSActive())
            {
                _snapAssist.UpdateTargeting();
            }

            _inputEmulator.ApplyPendingInputs();
        }

        public void Clear()
        {
            _triggerBot?.Reset();
            _snapAssist?.Reset();
        }
    }
}
