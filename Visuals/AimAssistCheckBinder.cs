// Monarch v1.0 – AimAssistCheckboxBinder.cs
// ✅ Monarch Fix Checklist
// [x] GUI checkbox binds to aim logic
// [x] Starts/stops PredictionEngine thread
// [x] Safe toggling and cleanup

using System;
using System.Threading;
using System.Windows.Controls;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.Visuals
{
    public class AimAssistCheckboxBinder
    {
        private readonly CheckBox _aimCheckbox;
        private Thread _aimThread;
        private PredictionEngine _predictionEngine;

        public AimAssistCheckboxBinder(CheckBox checkbox)
        {
            _aimCheckbox = checkbox;
            _aimCheckbox.Checked += ToggleAimAssist;
            _aimCheckbox.Unchecked += ToggleAimAssist;
        }

        private void ToggleAimAssist(object sender, EventArgs e)
        {
            if (_aimCheckbox.IsChecked == true)
            {
                StartAimAssist();
            }
            else
            {
                StopAimAssist();
            }
        }

        private void StartAimAssist()
        {
            if (_aimThread != null && _aimThread.IsAlive) return;

            _aimThread = new Thread(() =>
            {
                _predictionEngine = new PredictionEngine();
                _predictionEngine.Run(); // Starts internal aim loop
            });

            _aimThread.IsBackground = true;
            _aimThread.Start();
        }

        private void StopAimAssist()
        {
            _predictionEngine?.Stop(); // Gracefully end logic loop
            _predictionEngine = null;

            if (_aimThread?.IsAlive == true)
                _aimThread.Interrupt();

            _aimThread = null;
        }
    }
}
