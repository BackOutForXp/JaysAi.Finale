// File: Modules/RecoilCompensator.cs
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using System;
using System.Timers;

namespace JaysAi.Finale.Modules
{
    public class RecoilCompensator
    {
        private readonly AppSettings _settings;
        private readonly Timer _recoilTimer;

        public RecoilCompensator(AppSettings settings)
        {
            _settings = settings;

            _recoilTimer = new Timer
            {
                Interval = _settings.RecoilTickIntervalMs,
                AutoReset = true
            };

            _recoilTimer.Elapsed += (_, _) => ApplyRecoil();
        }

        public void Enable()
        {
            if (_settings.EnableRecoilControl)
                _recoilTimer.Start();
        }

        public void Disable()
        {
            _recoilTimer.Stop();
        }

        private void ApplyRecoil()
        {
            if (InputHandler.IsLeftMouseDown())
            {
                int verticalOffset = _settings.RecoilVerticalOffset;
                int horizontalOffset = _settings.RecoilHorizontalOffset;

                InputEmulator.MoveMouseRelative(horizontalOffset, verticalOffset);
            }
        }
    }
}
