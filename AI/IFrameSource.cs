// neural v3.0
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public interface IFrameSource
    {
        /// <summary>
        /// Returns the most recent frame snapshot captured from the source.
        /// </summary>
        /// <returns>Current FrameSnapshot</returns>
        FrameSnapshot GetCurrentFrame();

        /// <summary>
        /// Forces a new frame capture (used for polling or manual updates).
        /// </summary>
        void CaptureFrame();

        /// <summary>
        /// The name of this frame source (e.g., "LiveGame", "Replay", "Simulated").
        /// </summary>
        string SourceName { get; }
    }
}
