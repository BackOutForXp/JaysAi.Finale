// File: IModule.cs
namespace JaysAi.Finale.Modules
{
    /// <summary>
    /// Base interface for all feature modules in the loader.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Whether this module is currently enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Called once per update tick/frame.
        /// </summary>
        void Update();
    }
}
