// Neural v3.0 — AimDataRecorder.cs
using System;
using System.Collections.Generic;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Models;
using JaysAi.Finale.Utility;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Input
{
    public class AimDataRecorder
    {
        private readonly List<AimSnapshot> aimSnapshots;
        private readonly object lockObj = new();

        public AimDataRecorder()
        {
            aimSnapshots = new List<AimSnapshot>();
        }

        public void RecordSnapshot(DateTime timestamp, Vector2 targetPosition, Vector2 aimPosition, float distance, float aimDelta)
        {
            lock (lockObj)
            {
                aimSnapshots.Add(new AimSnapshot
                {
                    Timestamp = timestamp,
                    TargetPosition = targetPosition,
                    AimPosition = aimPosition,
                    DistanceToTarget = distance,
                    AimError = aimDelta
                });

                if (aimSnapshots.Count > 1000)
                    aimSnapshots.RemoveAt(0); // Trim oldest
            }
        }

        public List<AimSnapshot> GetSnapshots()
        {
            lock (lockObj)
            {
                return new List<AimSnapshot>(aimSnapshots);
            }
        }

        public void Clear()
        {
            lock (lockObj)
            {
                aimSnapshots.Clear();
            }
        }
    }

    public class AimSnapshot
    {
        public DateTime Timestamp { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Vector2 AimPosition { get; set; }
        public float DistanceToTarget { get; set; }
        public float AimError { get; set; }
    }
}
