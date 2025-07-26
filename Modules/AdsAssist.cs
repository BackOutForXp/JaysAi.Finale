// Neural v3.0 — AdAssist.cs
using DiscordRPC.Logging;
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Input;
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Logging;
using JaysAi.Finale.Modules.Config;
using JaysAi.Finale.SystemLogic;
using System;
using System.Reactive.Subjects;

namespace JaysAi.Finale.Modules
{
    public sealed class AdAssist : IDisposable
    {
        private readonly IDisposable _subscription;
        private readonly PredictionEngine _predictionEngine;
        private readonly ILogger _logger;
        private readonly Subject<AdEvent> _adEvents;
        private bool _isActive;

        public IObservable<AdEvent> AdEvents => _adEvents;

        public AdAssist(IInputSource inputSource, PredictionEngine predictionEngine, ILogger logger)
        {
            _predictionEngine = predictionEngine;
            _logger = logger;
            _adEvents = new Subject<AdEvent>();
            _isActive = true;

            _subscription = inputSource.InputStateStream.Subscribe(OnInputReceived);
        }

        private void OnInputReceived(InputState state)
        {
            if (!_isActive || !AdAssistConfig.IsEnabled) return;

            var prediction = _predictionEngine.PredictAdTarget(state);

            if (prediction.IsValid && prediction.Confidence > AdAssistConfig.ConfidenceThreshold)
            {
                _logger.Debug($"[AdAssist] Target detected: {prediction.TargetLabel} @ {prediction.Confidence:P0}");
                _adEvents.OnNext(new AdEvent
                {
                    Timestamp = DateTime.UtcNow,
                    TargetLabel = prediction.TargetLabel,
                    Confidence = prediction.Confidence,
                    InputSnapshot = state
                });

                if (AdAssistConfig.AutoEngage)
                    InputInjector.Instance.Inject(prediction.SuggestedAction);
            }
        }

        public void Enable() => _isActive = true;

        public void Disable() => _isActive = false;

        public void Dispose()
        {
            _subscription.Dispose();
            _adEvents.OnCompleted();
            _adEvents.Dispose();
        }
    }

    public static class AdAssistConfig
    {
        public static bool IsEnabled { get; set; } = true;
        public static bool AutoEngage { get; set; } = true;
        public static float ConfidenceThreshold { get; set; } = 0.85f;
    }

    public class AdEvent
    {
        public DateTime Timestamp { get; set; }
        public string TargetLabel { get; set; }
        public float Confidence { get; set; }
        public InputState InputSnapshot { get; set; }
    }
}
