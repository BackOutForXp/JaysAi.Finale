//monarch v2.1
using System.Collections.Generic;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class AimDataRecorder
    {
        private readonly List<FrameSnapshot> snapshots = new();
        private readonly List<(float X, float Y)> stickMovements = new();
        private readonly int maxFrames;

        public AimDataRecorder(int maxFrames = 1800) // ~60 seconds at 30 FPS
        {
            this.maxFrames = maxFrames;
        }

        public void RecordFrame(FrameSnapshot snapshot, float stickX, float stickY)
        {
            if (snapshots.Count >= maxFrames)
            {
                snapshots.RemoveAt(0);
                stickMovements.RemoveAt(0);
            }

            snapshots.Add(snapshot);
            stickMovements.Add((stickX, stickY));
        }

        public (List<FrameSnapshot> Snapshots, List<(float X, float Y)> Movements) GetRecording()
        {
            return (snapshots, stickMovements);
        }

        public void Clear()
        {
            snapshots.Clear();
            stickMovements.Clear();
        }

        public bool HasData => snapshots.Count > 0;
    }
}
