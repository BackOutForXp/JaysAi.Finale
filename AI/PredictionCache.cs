//monarch v2.1
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class PredictionCache
    {
        private readonly Queue<FrameSnapshot> frames = new();
        private const int MaxFrames = 6;

        public void AddSnapshot(FrameSnapshot snapshot)
        {
            frames.Enqueue(snapshot);
            while (frames.Count > MaxFrames)
                frames.Dequeue();
        }

        public (float dx, float dy)? GetVelocityEstimate(int framesAhead = 1)
        {
            if (frames.Count < 2)
                return null;

            var frameArray = frames.ToArray();
            var first = frameArray[0];
            var last = frameArray[^1];

            float dx = (last.X - first.X) / frames.Count * framesAhead;
            float dy = (last.Y - first.Y) / frames.Count * framesAhead;

            return (dx, dy);
        }

        public void Clear()
        {
            frames.Clear();
        }
    }
}
