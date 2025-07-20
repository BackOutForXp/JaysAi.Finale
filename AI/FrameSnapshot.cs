//monarch v2.1
using System;

namespace JaysAi.Finale.AI
{
    public class FrameSnapshot
    {
        public int FrameId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Confidence { get; set; }
        public DateTime Timestamp { get; set; }

        public FrameSnapshot(int frameId, float x, float y, float confidence)
        {
            FrameId = frameId;
            X = x;
            Y = y;
            Confidence = confidence;
            Timestamp = DateTime.UtcNow;
        }
    }
}
