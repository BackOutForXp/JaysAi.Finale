// neural v3.0
using System.Collections.Generic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public interface IEnemyProvider
    {
        /// <summary>
        /// Returns the latest list of detected enemies.
        /// </summary>
        /// <returns>List of Enemy entities</returns>
        List<Enemy> GetDetectedEnemies();

        /// <summary>
        /// Optional: updates internal state from external sensors or scans.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Optional: initializes provider-specific resources.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Optional: shuts down and cleans up any hardware or memory pointers.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Indicates if the provider is currently operational.
        /// </summary>
        bool IsActive { get; }
    }
}
