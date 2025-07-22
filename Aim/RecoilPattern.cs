//heavenly v3.0
using System.Collections.Generic;

namespace JaysAi.Finale.Aim
{
    public class RecoilPattern
    {
        private readonly List<Offset> _pattern;
        private readonly bool _loop;

        public RecoilPattern(IEnumerable<Offset> pattern, bool loop = true)
        {
            _pattern = new List<Offset>(pattern);
            _loop = loop;
        }

        public Offset GetOffset(int shotIndex)
        {
            if (_pattern.Count == 0)
                return new Offset(0, 0);

            if (shotIndex < _pattern.Count)
                return _pattern[shotIndex];

            return _loop ? _pattern[shotIndex % _pattern.Count] : new Offset(0, 0);
        }

        public void AddStep(float x, float y)
        {
            _pattern.Add(new Offset(x, y));
        }

        public void Clear()
        {
            _pattern.Clear();
        }
    }

    public struct Offset
    {
        public float X, Y;
        public Offset(float x, float y) => (X, Y) = (x, y);
    }
}
