//heavenly v3.0 – Visual Frame Buffer Cache
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class FrameHistory
    {
        private readonly int maxFrames;
        private readonly Queue<FrameSnapshot> snapshots;

        public FrameHistory(int capacity = 30)
        {
            maxFrames = capacity;
            snapshots = new Queue<FrameSnapshot>(capacity);
        }

        public void AddSnapshot(FrameSnapshot snapshot)
        {
            if (snapshots.Count >= maxFrames)
                snapshots.Dequeue();

            snapshots.Enqueue(snapshot);
        }

        public IReadOnlyList<FrameSnapshot> GetRecentSnapshots(int count)
        {
            return snapshots.Reverse().Take(count).ToList();
        }

        public FrameSnapshot? GetLastSnapshot()
        {
            return snapshots.Count > 0 ? snapshots.Last() : null;
        }

        public void Clear()
        {
            snapshots.Clear();
        }

        public bool IsEmpty => snapshots.Count == 0;

        public int Count => snapshots.Count;
    }
}
