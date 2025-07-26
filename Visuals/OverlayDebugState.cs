// Neural v3.0 — OverlayDebugState.cs
namespace JaysAi.Finale.Visuals
{
    public class OverlayDebugState
    {
        public bool IsRendering { get; set; }
        public bool IsConnected { get; set; }
        public float CurrentFPS { get; set; }
        public int FrameBufferSize { get; set; }
        public string StatusMessage { get; set; }

        public OverlayDebugState()
        {
            IsRendering = false;
            IsConnected = false;
            CurrentFPS = 0f;
            FrameBufferSize = 0;
            StatusMessage = string.Empty;
        }
    }
}
