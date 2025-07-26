// neural v3.0
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JaysAi.Finale.Hardware
{
    /// <summary>
    /// Simulates a screen capture by returning a static image or frame buffer.
    /// Useful for testing visual modules without real-time input sources.
    /// </summary>
    public class VirtualCapture : ICaptureInterface
    {
        private BitmapSource? _staticFrame;
        public bool IsRunning { get; private set; }

        public int Width => _staticFrame?.PixelWidth ?? 1920;
        public int Height => _staticFrame?.PixelHeight ?? 1080;

        /// <summary>
        /// Initializes the virtual capture system by loading a test image.
        /// </summary>
        public async Task<bool> InitializeAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Load a static test image from embedded resources or disk
                    string testImagePath = Path.Combine(AppContext.BaseDirectory, "Assets", "test_frame.png");
                    if (File.Exists(testImagePath))
                    {
                        using var stream = File.OpenRead(testImagePath);
                        var decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                        _staticFrame = decoder.Frames[0];
                        IsRunning = true;
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[VirtualCapture] Initialization failed: {ex.Message}");
                    return false;
                }
            });
        }

        /// <summary>
        /// Returns the static test frame.
        /// </summary>
        public Task<BitmapSource?> CaptureFrameAsync()
        {
            return Task.FromResult(_staticFrame);
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Dispose()
        {
            _staticFrame = null;
        }
    }
}
