//monarch v2.1

//monarch v2.1
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class FrameHistory
    {
        private readonly int capacity;
        private readonly Queue<FrameSnapshot> frames;

        public FrameHistory(int capacity = 60)
        {
            this.capacity = capacity;
            frames = new Queue<FrameSnapshot>(capacity);
        }

        public void Add(FrameSnapshot frame)
        {
            if (frames.Count >= capacity)
                frames.Dequeue();
            frames.Enqueue(frame);
        }

        public FrameSnapshot[] GetAll()
        {
            return frames.ToArray();
        }

        public FrameSnapshot GetMostRecent()
        {
            return frames.Count > 0 ? frames.Last() : null;
        }

        public void Clear()
        {
            frames.Clear();
        }

        public int Count => frames.Count;
    }
}
