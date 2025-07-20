// File: System\GameMemory.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.SystemLogic
{
    public static class GameMemory
    {
        public static bool IsGameAttached { get; private set; } = false;

        public static void AttachToGameProcess()
        {
            // TODO: Add logic to attach to target game process
            IsGameAttached = true;
        }

        public static void Detach()
        {
            IsGameAttached = false;
        }

        public static Vector3 GetPlayerWorldPosition()
        {
            // TODO: Replace with actual memory read or fallback (mock data for now)
            return new Vector3(100, 0, 50);
        }

        public static List<Enemy> ReadEnemiesFromMemory()
        {
            // TODO: Read from memory, currently mocked
            return new List<Enemy>
            {
                new Enemy
                {
                    ScreenPosition = new Vector2(800, 400),
                    Velocity = new Vector3(1, 0, 0),
                    IsVisible = true,
                    IsPriorityTarget = false
                }
            };
        }
    }
}
