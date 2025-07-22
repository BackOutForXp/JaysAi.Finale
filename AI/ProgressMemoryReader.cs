//heavenly v3.0
using System;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public class ProgressMemoryReader
    {
        private readonly ISystemMemory _memory;

        public ProgressMemoryReader(ISystemMemory memory)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
        }

        public float ReadPredictionConfidence(Guid targetId)
        {
            if (_memory == null)
                return 0f;

            string key = $"prediction_confidence_{targetId}";
            return _memory.TryReadFloat(key, out var value) ? value : 0f;
        }

        public int ReadTargetLockAttempts(Guid targetId)
        {
            if (_memory == null)
                return 0;

            string key = $"lock_attempts_{targetId}";
            return _memory.TryReadInt(key, out var value) ? value : 0;
        }

        public bool HasLineOfSight(Guid targetId)
        {
            string key = $"line_of_sight_{targetId}";
            return _memory.TryReadBool(key, out var value) && value;
        }

        public void UpdateTargetPerformance(Guid targetId, float newConfidence)
        {
            if (_memory == null)
                return;

            string key = $"prediction_confidence_{targetId}";
            _memory.WriteFloat(key, newConfidence);
        }

        public void IncrementLockAttempts(Guid targetId)
        {
            if (_memory == null)
                return;

            string key = $"lock_attempts_{targetId}";
            int current = ReadTargetLockAttempts(targetId);
            _memory.WriteInt(key, current + 1);
        }
    }
}
