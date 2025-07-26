// Neural v3.0 — NeuralFeedbackState.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.SystemLogic.Telemetry
{
    public class NeuralFeedbackState
    {
        public DateTime Timestamp { get; set; }
        public string Source { get; set; }
        public string Signal { get; set; }
        public Vector3? LastKnownTargetPosition { get; set; }
        public float ConfidenceLevel { get; set; }
        public bool IsCorrectionRequired { get; set; }

        public NeuralFeedbackState(string source, string signal, float confidence, bool correctionNeeded, Vector3? targetPos = null)
        {
            Timestamp = DateTime.UtcNow;
            Source = source;
            Signal = signal;
            ConfidenceLevel = confidence;
            IsCorrectionRequired = correctionNeeded;
            LastKnownTargetPosition = targetPos;
        }
    }
}
