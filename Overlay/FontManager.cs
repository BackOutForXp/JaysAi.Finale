// Neural v3.1 — FontManager.cs
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public static class FontManager
    {
        private static readonly Dictionary<string, SKTypeface> _loadedFonts = new();

        public static SKTypeface LoadFont(string fontPath)
        {
            if (_loadedFonts.ContainsKey(fontPath))
                return _loadedFonts[fontPath];

            var typeface = SKTypeface.FromFile(fontPath);
            _loadedFonts[fontPath] = typeface;
            return typeface;
        }

        public static SKTypeface GetDefault() =>
            SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold);
    }
}
