// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Aim
{
    public class CompensationGraph
    {
        private readonly SortedDictionary<float, float> compensationMap;

        public CompensationGraph()
        {
            compensationMap = new SortedDictionary<float, float>();
        }

        public void AddCompensationPoint(float distance, float compensationValue)
        {
            if (!compensationMap.ContainsKey(distance))
                compensationMap[distance] = compensationValue;
            else
                compensationMap[distance] = (compensationMap[distance] + compensationValue) / 2f; // smoothing update
        }

        public float GetCompensation(float distance)
        {
            if (compensationMap.Count == 0)
                return 0f;

            float closestDistance = float.MaxValue;
            float closestValue = 0f;

            foreach (var pair in compensationMap)
            {
                float diff = Math.Abs(pair.Key - distance);
                if (diff < closestDistance)
                {
                    closestDistance = diff;
                    closestValue = pair.Value;
                }
            }

            return closestValue;
        }

        public void Clear() => compensationMap.Clear();
        public IReadOnlyDictionary<float, float> GetMap() => compensationMap;
    }
}
