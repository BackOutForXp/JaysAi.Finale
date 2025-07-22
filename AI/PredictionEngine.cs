//heavenly v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public class PredictionEngine
    {
        private readonly int frameWindow = 6;
        private readonly Queue<FrameSnapshot> frameSnapshots = new();
        private readonly object syncLock = new();

        public void AddFrame(FrameSnapshot snapshot)
        {
            lock (syncLock)
            {
                frameSnapshots.Enqueue(snapshot);
                if (frameSnapshots.Count > frameWindow)
                    frameSnapshots.Dequeue();
            }
        }

        public Vector2? PredictDisplacement(int framesAhead = 1)
        {
            lock (syncLock)
            {
                if (frameSnapshots.Count < 2)
                    return null;

                var array = frameSnapshots.ToArray();
                var start = array.First();
                var end = array.Last();

                float dx = (end.Position.X - start.Position.X) / (array.Length - 1) * framesAhead;
                float dy = (end.Position.Y - start.Position.Y) / (array.Length - 1) * framesAhead;

                return new Vector2(dx, dy);
            }
        }

        public Vector2? PredictFuturePosition(Vector2 currentPosition, int framesAhead = 1)
        {
            var displacement = PredictDisplacement(framesAhead);
            if (displacement == null) return null;

            return currentPosition + displacement.Value;
        }

        public void Clear()
        {
            lock (syncLock)
            {
                frameSnapshots.Clear();
            }
        }
    }

    public class FrameSnapshot
    {
        public Vector2 Position { get; set; }
        public DateTime Timestamp { get; set; }

        public FrameSnapshot(Vector2 position)
        {
            Position = position;
            Timestamp = DateTime.UtcNow;
        }
    }

    public struct Vector2
    {
        public float X, Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public override string ToString() => $"({X:F2}, {Y:F2})";
    }
}
