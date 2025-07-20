//monarch v2.1 – Visual ESP Signal Dispatcher
using System.Collections.Generic;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public static class ESPModule
    {
        public static bool IsEnabled = true;
        private static readonly List<OverlaySignal> activeSignals = new();

        public static void Update(List<DetectedEntity> entities)
        {
            if (!IsEnabled || entities == null) return;

            activeSignals.Clear();

            foreach (var entity in entities)
            {
                if (entity == null || !entity.IsEnemy) continue;

                var predictedPos = PredictionEngine.PredictNextPosition(entity.ID, entity.Position);

                var signal = new OverlaySignal
                {
                    ID = entity.ID,
                    ScreenPosition = entity.ScreenPosition,
                    Color = entity.TeamColor,
                    Label = entity.NameTag,
                    PredictedPosition = predictedPos
                };

                activeSignals.Add(signal);
            }

            OverlayDrawer.SetDrawQueue(activeSignals);
        }

        public static void Toggle(bool state)
        {
            IsEnabled = state;
        }

        public static void Clear()
        {
            activeSignals.Clear();
        }
    }

    public class DetectedEntity
    {
        public int ID { get; set; }
        public string NameTag { get; set; }
        public Vec2f Position { get; set; }
        public Vec2f ScreenPosition { get; set; }
        public Vec3f WorldPosition { get; set; }
        public bool IsEnemy { get; set; }
        public string TeamColor { get; set; }
    }
}
