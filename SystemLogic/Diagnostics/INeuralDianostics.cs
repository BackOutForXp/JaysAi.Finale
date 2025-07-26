// Neural v3.0 — INeuralDiagnostic.cs
using System;

namespace JaysAi.Finale.SystemLogic.Diagnostics
{
    public interface INeuralDiagnostic
    {
        string DiagnosticName { get; }
        DateTime LastCheckTime { get; }
        NeuralFeedbackState RunCheck();
    }
}
