//monarch v2.1 – MainInjection.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System;

namespace JaysAi.Finale.Core
{
    public static class MainInjection
    {
        private static bool initialized = false;
        private static ESPModule esp;
        private static AimbotLogic aimbot;
        private static StickXModule stickX;
        private static PredictionEngine prediction;

        public static void Initialize()
        {
            if (initialized)
                return;

            esp = new ESPModule();
            aimbot = new AimbotLogic();
            stickX = new StickXModule();
            prediction = new PredictionEngine();

            OverlaySignal.OnDraw += () =>
            {
                esp.DrawEnemies();
                esp.DrawSnapLines();
            };

            Logger.Log("MainInjection initialized.");
            initialized = true;
        }

        public static void Update()
        {
            if (!initialized)
                return;

            var enemyData = esp.GetVisibleEnemies();
            if (enemyData != null)
            {
                var predictionData = prediction.Predict(enemyData);
                stickX.ApplyPrediction(predictionData);
                aimbot.ApplyCorrection(stickX.GetStickOutput());
            }
        }
    }
}
