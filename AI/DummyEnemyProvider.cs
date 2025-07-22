//heavenly v3.0.0 – Offline Testing Dummy Target Provider
using System.Collections.Generic;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.AI
{
    public class DummyEnemyProvider : IEnemyProvider
    {
        public List<DetectedObject> GetEnemies()
        {
            return new List<DetectedObject>
            {
                new DetectedObject
                {
                    X = 400,
                    Y = 300,
                    Width = 50,
                    Height = 100,
                    IsEnemy = true,
                    Label = "Dummy Enemy"
                }
            };
        }
    }
}
