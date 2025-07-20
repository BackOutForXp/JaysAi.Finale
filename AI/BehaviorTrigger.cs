//monarch v2.1 – In-Game Behavior Decision Logic
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.AI;
using System;

namespace JaysAi.Finale.AI
{
    public class BehaviorTrigger
    {
        private readonly AIProfileSwitcher _profileSwitcher;
        private readonly ControllerInputState _controller;
        private readonly SnapAssist _snapAssist;
        private readonly ESPModule _esp;
        private readonly AimAssist _aimAssist;

        public BehaviorTrigger(
            AIProfileSwitcher profileSwitcher,
            ControllerInputState controller,
            SnapAssist snapAssist,
            ESPModule esp,
            AimAssist aimAssist)
        {
            _profileSwitcher = profileSwitcher;
            _controller = controller;
            _snapAssist = snapAssist;
            _esp = esp;
            _aimAssist = aimAssist;
        }

        public void Update()
        {
            _profileSwitcher.Update();

            if (_controller.IsADS && _controller.FireHeld)
            {
                _snapAssist.UpdateSnap();
                _aimAssist.ApplyAssist();
            }

            if (_controller.ToggleESP)
            {
                _esp.ToggleVisibility();
            }

            if (_controller.InputChanged)
            {
                Console.WriteLine("[Trigger] Input updated.");
            }
        }
    }
}
