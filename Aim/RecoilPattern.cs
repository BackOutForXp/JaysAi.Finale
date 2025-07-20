//monarch v2.1
using System.Collections.Generic;

namespace JaysAi.Finale.Aim
{
    public class RecoilPattern
    {
        public List<(float offsetX, float offsetY)> Steps { get; set; }

        public RecoilPattern()
        {
            Steps = new List<(float offsetX, float offsetY)>();
        }

        public void AddStep(float x, float y)
        {
            Steps.Add((x, y));
        }

        public void Clear()
        {
            Steps.Clear();
        }

        public int Count => Steps.Count;
    }
}
