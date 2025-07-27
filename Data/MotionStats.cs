// Neural v3.1
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class MotionStats
    {
        private Vector3 _previousPosition = Vector3.Zero;
        private float _previousSpeed = 0f;
        private float _directionChangeCooldown = 0f;

        public float Speed { get; private set; }
        public bool IsStrafing { get; private set; }

        public void Update(Vector3 currentPosition, float deltaTime)
        {
            if (deltaTime <= 0f)
                return;

            Speed = Vector3.Distance(_previousPosition, currentPosition) / deltaTime;

            Vector3 movementDir = currentPosition - _previousPosition;

            if (_directionChangeCooldown <= 0f && movementDir.LengthSquared() > 0.01f)
            {
                Vector3 normalized = Vector3.Normalize(movementDir);
                float dot = Vector3.Dot(normalized, new Vector3(1, 0, 0)); // X axis movement
                IsStrafing = Math.Abs(dot) > 0.75f;
                _directionChangeCooldown = 0.15f;
            }
            else
            {
                _directionChangeCooldown -= deltaTime;
            }

            _previousPosition = currentPosition;
            _previousSpeed = Speed;
        }
    }
}
