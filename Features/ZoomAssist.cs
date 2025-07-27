// Neural v3.1 — ZoomAssist.cs
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using System;

namespace JaysAi.Finale.Features
{
    public class ZoomAssist
    {
        public bool IsEnabled => UserSettings.Instance.Get("ZoomAssistEnabled", true);
        public float ZoomMultiplier => UserSettings.Instance.Get("ZoomAssistMultiplier", 1.15f);

        public void ApplyZoom(AimContext context)
        {
            if (!IsEnabled || context?.Target == null)
                return;

            var originalFov = context.OriginalFov;
            var distance = context.TargetDistance;

            // Apply zoom multiplier based on distance (AI-tunable in future)
            float factor = Math.Clamp(ZoomMultiplier / (1f + distance * 0.01f), 0.75f, 2f);
            context.CurrentFov = originalFov * factor;
        }
    }
}
