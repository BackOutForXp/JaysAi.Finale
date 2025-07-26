// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;
using System;

namespace JaysAi.Finale.AI
{
    public enum BehaviorCondition
    {
        Always,
        EnemyInSight,
        CloseRange,
        LowHealth,
        Firing,
        Zoomed
    }

    public class BehaviorTrigger
    {
        public BehaviorCondition Condition { get; set; }
        public float RangeThreshold { get; set; } = 25f;

        public bool Evaluate(TargetInfo target, PlayerState player)
        {
            return Condition switch
            {
                BehaviorCondition.Always => true,
                BehaviorCondition.EnemyInSight => target != null && target.IsVisible,
                BehaviorCondition.CloseRange => target != null && target.Distance <= RangeThreshold,
                BehaviorCondition.LowHealth => player.Health <= 25,
                BehaviorCondition.Firing => player.IsFiring,
                BehaviorCondition.Zoomed => player.IsZoomed,
                _ => false
            };
        }

        public static BehaviorTrigger Create(BehaviorCondition condition, float range = 25f)
        {
            return new BehaviorTrigger
            {
                Condition = condition,
                RangeThreshold = range
            };
        }
    }

    public class PlayerState
    {
        public bool IsFiring { get; set; }
        public bool IsZoomed { get; set; }
        public float Health { get; set; }
    }
}
