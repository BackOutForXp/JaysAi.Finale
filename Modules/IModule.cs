// neural v3.0
namespace JaysAi.Finale.Modules
{
    public interface IModule
    {
        /// <summary>
        /// Initializes the module, preparing it for operation.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Executes one tick of the module's core logic.
        /// Called on a fixed or variable interval by the loader.
        /// </summary>
        void Tick();

        /// <summary>
        /// Cleans up resources or gracefully shuts down the module.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Optional method to check if the module is currently active.
        /// </summary>
        bool IsActive { get; }
    }
}
