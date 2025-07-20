//monarch v2.1
namespace JaysAi.Finale.Input
{
    public class StickAssistConfig
    {
        /// <summary>
        /// Proportional gain (how much to correct based on current error)
        /// </summary>
        public float Kp { get; set; } = 1.1f;

        /// <summary>
        /// Integral gain (accumulated correction over time)
        /// </summary>
        public float Ki { get; set; } = 0.005f;

        /// <summary>
        /// Derivative gain (rate of change smoothing)
        /// </summary>
        public float Kd { get; set; } = 0.35f;

        /// <summary>
        /// Threshold below which output is ignored (like a real controller)
        /// </summary>
        public float Deadzone { get; set; } = 2.0f;

        /// <summary>
        /// Max allowed stick force [-100 to +100]
        /// </summary>
        public short MaxStrength { get; set; } = 100;

        /// <summary>
        /// If true, disables vertical aim movement
        /// </summary>
        public bool VerticalOnly { get; set; } = false;
    }
}
