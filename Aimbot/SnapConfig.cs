//monarch v2.1
namespace JaysAi.Finale.Aimbot
{
    public class SnapConfig
    {
        /// <summary>
        /// How strong the snap should be [0.0f to 1.0f]
        /// </summary>
        public float SnapStrength { get; set; } = 0.85f;

        /// <summary>
        /// Radius from crosshair where aim assist activates (in pixels)
        /// </summary>
        public float MagnetThreshold { get; set; } = 75f;

        /// <summary>
        /// If true, locks only horizontally (no vertical snapping)
        /// </summary>
        public bool VerticalLock { get; set; } = false;

        /// <summary>
        /// If true, smooths transitions between target snaps
        /// </summary>
        public bool EnableSmoothing { get; set; } = true;

        /// <summary>
        /// Controls snap delay or speed curve smoothing [0 = instant]
        /// </summary>
        public float SmoothingFactor { get; set; } = 0.12f;
    }
}
