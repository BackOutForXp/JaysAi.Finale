// neural v3.0
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public class RecoilPattern
    {
        private readonly List<Vector2> _patternPoints = new();
        private int _currentIndex = 0;

        public RecoilPattern(IEnumerable<Vector2> pattern)
        {
            if (pattern != null)
                _patternPoints.AddRange(pattern);
        }

        public Vector2 GetNextOffset()
        {
            if (_patternPoints.Count == 0)
                return Vector2.Zero;

            Vector2 offset = _patternPoints[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _patternPoints.Count;
            return offset;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public bool IsEmpty => _patternPoints.Count == 0;
    }
}
