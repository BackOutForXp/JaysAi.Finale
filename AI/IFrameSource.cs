//heavenly v3.0.0 – Unified Frame Source Abstraction Interface
using System;
using System.Drawing;

namespace JaysAi.Finale.AI
{
    public interface IFrameSource : IDisposable
    {
        /// <summary>
        /// Gets the latest video frame from the input source.
        /// </summary>
        /// <returns>A bitmap of the latest frame.</returns>
        Bitmap GetLatestFrame();

        /// <summary>
        /// Indicates whether the source is currently running or available.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Optional metadata or tag for the frame source (e.g., "Webcam", "CaptureCard").
        /// </summary>
        string SourceLabel { get; }
    }
}
