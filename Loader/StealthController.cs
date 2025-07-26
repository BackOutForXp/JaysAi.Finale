// neural v3.0
using System;
using JaysAi.Finale.SystemLogic.Diagnostics;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Signals;

namespace JaysAi.Finale.Integration
{
    public sealed class StealthController : IDisposable
    {
        private readonly IProcessCloaker _cloaker;
        private readonly IObfuscationEngine _obfuscator;
        private readonly SignalBus _signalBus;
        private bool _isStealthEnabled;

        public bool IsStealthed => _isStealthEnabled;

        public StealthController(
            IProcessCloaker cloaker,
            IObfuscationEngine obfuscator,
            SignalBus signalBus)
        {
            _cloaker = cloaker;
            _obfuscator = obfuscator;
            _signalBus = signalBus;
        }

        public void EnableStealth()
        {
            if (_isStealthEnabled) return;

            _cloaker.HideFromTaskManager();
            _obfuscator.ObfuscateProcessName();
            _obfuscator.MorphMemoryLayout();
            _isStealthEnabled = true;

            Logger.Info("🕵️ Stealth mode enabled");
            _signalBus.Broadcast(new StealthSignal(true));
        }

        public void DisableStealth()
        {
            if (!_isStealthEnabled) return;

            _cloaker.RestoreProcessVisibility();
            _obfuscator.RevertAll();
            _isStealthEnabled = false;

            Logger.Warn("🛑 Stealth mode disabled");
            _signalBus.Broadcast(new StealthSignal(false));
        }

        public void Toggle()
        {
            if (_isStealthEnabled)
                DisableStealth();
            else
                EnableStealth();
        }

        public void Dispose()
        {
            if (_isStealthEnabled)
                DisableStealth();
        }
    }
}
