// neural v3.0
using System;
using System.Timers;
using JaysAi.Finale.Input;
using JaysAi.Finale.AI;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Features
{
    public class TriggerBot
    {
        private readonly Timer _triggerTimer;
        private readonly AiDetectionContext _context;

        public bool IsEnabled { get; set; }

        public TriggerBot()
        {
            _context = AiDetectionContext.Instance;
            _triggerTimer = new Timer
            {
                Interval = 10,
                AutoReset = true
            };

            _triggerTimer.Elapsed += OnTriggerCheck;
        }

        public void Start()
        {
            if (!IsEnabled) return;
            Logger.Log("[TriggerBot] Activated", LogLevel.Info);
            _triggerTimer.Start();
        }

        public void Stop()
        {
            _triggerTimer.Stop();
            Logger.Log("[TriggerBot] Deactivated", LogLevel.Info);
        }

        private void OnTriggerCheck(object sender, ElapsedEventArgs e)
        {
            if (!IsEnabled || !_context.IsEnemyDetected) return;

            if (_context.IsEnemyInFOV && _context.IsWithinDistanceThreshold)
            {
                FireTrigger();
            }
        }

        private void FireTrigger()
        {
            Logger.Log("[TriggerBot] Firing trigger", LogLevel.Debug);
            InputSimulator.PressLeftMouse();
        }
    }
}
