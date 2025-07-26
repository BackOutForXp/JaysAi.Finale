// neural v3.0
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Models;
using JaysAi.Finale.Core;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.Integration
{
    public class AIIntegrationBridge
    {
        private readonly IInputSource _inputSource;
        private readonly IAITargetResolver _targetResolver;
        private readonly IAIDecisionEngine _decisionEngine;

        public AIIntegrationBridge(IInputSource inputSource, IAITargetResolver targetResolver, IAIDecisionEngine decisionEngine)
        {
            _inputSource = inputSource ?? throw new ArgumentNullException(nameof(inputSource));
            _targetResolver = targetResolver ?? throw new ArgumentNullException(nameof(targetResolver));
            _decisionEngine = decisionEngine ?? throw new ArgumentNullException(nameof(decisionEngine));
        }

        public void Update()
        {
            var currentInput = _inputSource.GetInputState();
            var target = _targetResolver.ResolveTarget(currentInput);
            var action = _decisionEngine.Evaluate(currentInput, target);

            if (action != null)
            {
                action.Execute();
                AppLogger.LogInfo($"[AI Bridge] Executed action: {action.GetType().Name}");
            }
        }
    }
}
