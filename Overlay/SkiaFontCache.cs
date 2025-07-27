// Neural v3.1 — SkiaFontCache.cs
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public static class SkiaFontCache
    {
        private static readonly Dictionary<string, SKTypeface> _fontCache = new();

        public static SKTypeface GetTypeface(string familyName = "Arial", SKFontStyleWeight weight = SKFontStyleWeight.Normal, SKFontStyleSlant slant = SKFontStyleSlant.Upright)
        {
            string key = $"{familyName}:{weight}:{slant}";

            if (_fontCache.TryGetValue(key, out var typeface))
                return typeface;

            var newTypeface = SKTypeface.FromFamilyName(familyName, weight, SKFontStyleWidth.Normal, slant);
            _fontCache[key] = newTypeface;
            return newTypeface;
        }

        public static void Clear()
        {
            foreach (var tf in _fontCache.Values)
                tf.Dispose();

            _fontCache.Clear();
        }
    }
}
