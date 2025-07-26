// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public class FrameHistory<T>
    {
        private readonly int _capacity;
        private readonly Queue<T> _frames;

        public FrameHistory(int capacity = 60)
        {
            _capacity = Math.Max(1, capacity);
            _frames = new Queue<T>(_capacity);
        }

        public void Add(T frame)
        {
            if (_frames.Count >= _capacity)
                _frames.Dequeue();

            _frames.Enqueue(frame);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _frames;
        }

        public T? GetLatest()
        {
            return _frames.Count > 0 ? _frames.Peek() : default;
        }

        public int Count => _frames.Count;
        public bool IsFull => _frames.Count == _capacity;

        public void Clear()
        {
            _frames.Clear();
        }
    }
}
