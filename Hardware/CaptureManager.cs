// neural v3.0
using System;
using System.Drawing;
using JaysAi.Finale.Logging;
using JaysAi.Finale.Hardware;

namespace JaysAi.Finale.Hardware
{
    public class CaptureManager
    {
        private static ICaptureInterface _activeCaptureInterface;

        /// <summary>
        /// Registers a capture interface implementation to be used globally.
        /// </summary>
        public static void RegisterCaptureInterface(ICaptureInterface captureInterface)
        {
            _activeCaptureInterface = captureInterface ?? throw new ArgumentNullException(nameof(captureInterface));
            Log.Info("CaptureManager", $"Registered capture interface: {captureInterface.GetType().Name}");
        }

        /// <summary>
        /// Captures the current screen frame using the registered interface.
        /// </summary>
        public static Bitmap Capture()
        {
            if (_activeCaptureInterface == null)
            {
                Log.Error("CaptureManager", "Capture interface not registered!");
                throw new InvalidOperationException("Capture interface is not set.");
            }

            try
            {
                return _activeCaptureInterface.CaptureFrame();
            }
            catch (Exception ex)
            {
                Log.Exception("CaptureManager", "Failed to capture frame", ex);
                return null;
            }
        }

        /// <summary>
        /// Optionally preprocess a frame before analysis.
        /// </summary>
        public static Bitmap Preprocess(Bitmap inputFrame)
        {
            if (_activeCaptureInterface == null)
                return inputFrame;

            try
            {
                return _activeCaptureInterface.PreprocessFrame(inputFrame);
            }
            catch (Exception ex)
            {
                Log.Exception("CaptureManager", "Frame preprocessing failed", ex);
                return inputFrame;
            }
        }

        /// <summary>
        /// Releases any capture resources.
        /// </summary>
        public static void Shutdown()
        {
            try
            {
                _activeCaptureInterface?.Shutdown();
                Log.Info("CaptureManager", "Capture interface shut down successfully.");
            }
            catch (Exception ex)
            {
                Log.Exception("CaptureManager", "Error during capture shutdown", ex);
            }
        }
    }
}
