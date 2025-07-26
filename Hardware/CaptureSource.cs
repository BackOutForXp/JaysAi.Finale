// neural v3.0
namespace JaysAi.Finale.Hardware
{
    /// <summary>
    /// Represents the available capture input types for screen grabbing.
    /// Used by CaptureManager and ICaptureInterface implementations.
    /// </summary>
    public enum CaptureSource
    {
        None = 0,

        /// <summary>
        /// Internal screen capture using desktop duplication, BitBlt, or DXGI.
        /// </summary>
        Internal,

        /// <summary>
        /// External capture card (e.g., Elgato, AVerMedia).
        /// </summary>
        CaptureCard,

        /// <summary>
        /// Simulated or virtual feed (used for debugging/testing).
        /// </summary>
        Virtual,

        /// <summary>
        /// Webcam or external camera source.
        /// </summary>
        Webcam
    }
}
