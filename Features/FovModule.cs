// Neural v3.1 — FovModule.cs
using System;
using System.Numerics;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Features
{
    public class FovModule
    {
        public float Radius => UserSettings.Instance.Get("FovRadius", 90f);
        public bool IsEnabled => UserSettings.Instance.Get("FovEnabled", true);
        public Vector2? ScreenCenter { get; private set; }

        public void Update(Vector2 resolution)
        {
            ScreenCenter = resolution / 2f;
        }

        public float GetEffectiveRadius()
        {
            return Radius;
        }

        public bool IsInsideFov(Vector2 screenPos)
        {
            if (!ScreenCenter.HasValue || !IsEnabled)
                return false;

            float distance = Vector2.Distance(ScreenCenter.Value, screenPos);
            return distance <= Radius;
        }
    }
}
