// Neural v3.1 — PlayerStateProvider.cs
using System.Numerics;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class PlayerStateProvider
    {
        private readonly PlayerContext _context = new();

        public PlayerContext Context => _context;

        public void UpdateState(Vector3 playerPosition, Vector3 viewDirection, float fieldOfView)
        {
            _context.Position = playerPosition;
            _context.ViewDirection = Vector3.Normalize(viewDirection);
            _context.FieldOfView = fieldOfView;
        }

        public bool IsReady => _context.IsInitialized;
    }
}
