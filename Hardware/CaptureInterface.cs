// neural v3.0
using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JaysAi.Finale.Hardware
{
    /// <summary>
    /// Defines a unified contract for all capture sources used in the loader.
    /// Implementations may include internal screen capture, capture cards, or virtual feeds.
    /// </summary>
    public interface ICaptureInterface : IDisposable
    {
        /// <summary>
        /// Initializes the capture source asynchronously.
        /// </summary>
        Task<bool> InitializeAsync();

        /// <summary>
        /// Captures a frame from the selected input source.
        /// </summary>
        /// <returns>A BitmapSource representing the captured frame, or null if unavailable.</returns>
        Task<BitmapSource?> CaptureFrameAsync();

        /// <summary>
        /// Gets the width of the input source resolution.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the input source resolution.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Indicates whether the capture device is actively running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Stops the capture process and releases resources.
        /// </summary>
        void Stop();
    }
}
