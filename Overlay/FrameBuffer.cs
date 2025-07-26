// neural v3.0
using System;
using System.Collections.Generic;
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class FrameBuffer
    {
        private readonly Queue<SKBitmap> _frames;
        private readonly int _maxFrames;
        private readonly object _lock = new();

        public FrameBuffer(int maxFrames = 30)
        {
            _maxFrames = maxFrames;
            _frames = new Queue<SKBitmap>(_maxFrames);
        }

        public void AddFrame(SKBitmap frame)
        {
            if (frame == null) return;

            lock (_lock)
            {
                if (_frames.Count >= _maxFrames)
                {
                    var oldFrame = _frames.Dequeue();
                    oldFrame.Dispose();
                }

                _frames.Enqueue(frame.Copy());
            }
        }

        public SKBitmap? GetLatestFrame()
        {
            lock (_lock)
            {
                if (_frames.Count == 0)
                    return null;

                return _frames.Peek().Copy();
            }
        }

        public SKBitmap[] GetAllFrames()
        {
            lock (_lock)
            {
                return _frames.ToArray();
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                while (_frames.Count > 0)
                {
                    _frames.Dequeue().Dispose();
                }
            }
        }
    }
}
