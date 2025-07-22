//heavenly v3.0.0 – Interface for Enemy Provider Abstraction
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public interface IEnemyProvider
    {
        List<TrackedTarget> GetEnemies();
    }
}
