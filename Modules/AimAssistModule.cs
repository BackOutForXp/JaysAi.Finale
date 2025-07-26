// Neural v3.0 — AimAssistModule.cs
using JaysAi.Finale.Input;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Modules
{
    /// <summary>
    /// Defines the contract for an aim assist module that can be plugged into the aim system.
    /// </summary>
    public interface IAimAssistModule
    {
        /// <summary>
        /// Applies the aim adjustment logic based on current input and prediction data.
        /// </summary>
        /// <param name="input">The current input state of the player/controller.</param>
        /// <param name="prediction">Predicted target movement or aim vector.</param>
        void Apply(InputState input, FramePrediction prediction);
    }
}
