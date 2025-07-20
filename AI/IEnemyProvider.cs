// File: AI/IEnemyProvider.cs
using JaysAi.Finale.Data;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public interface IEnemyProvider
    {
        List<Enemy> GetVisibleEnemies();
    }
}
