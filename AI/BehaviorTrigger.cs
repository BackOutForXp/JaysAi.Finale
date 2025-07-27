// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class BehaviorTrigger
    {
        private List<Func<TrackedTarget, bool>> _triggers = new();

        public void RegisterTriggers()
        {
            _triggers.Add(target =>
            {
                if (target.Confidence > 0.85 && target.FovDistance < 50f)
                {
                    InputEmulator.TriggerFire();
                    LogManager.Log($"🔥 Fired at high-confidence target {target.Id}");
                    return true;
                }
                return false;
            });

            _triggers.Add(target =>
            {
                if (target.FovDistance < 30f && target.Velocity.Length() > 5f)
                {
                    InputEmulator.SoftAimAssist(target);
                    return true;
                }
                return false;
            });
        }

        public void Evaluate(List<TrackedTarget> targets)
        {
            foreach (var target in targets)
            {
                foreach (var condition in _triggers)
                {
                    if (condition(target))
                        break;
                }
            }
        }
    }
}
